using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Store.Data;
using Store.Models;

namespace Store.Controllers
{
    public class InvoiceController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InvoiceController(ApplicationDbContext context)
        {
            _context = context;
        }

// GET: Invoice
public async Task<IActionResult> Index(string CustomerId)
{
    if (CustomerId != null){
        var customer = await _context.Users
            .FirstOrDefaultAsync(u=>u.Id == CustomerId);
        if(customer == null)
            return NotFound();
        ViewData["customer_id"] = CustomerId;
        ViewData["customer_name"] = customer.UserName;
        return View(await _context.Invoices
            .Where(i => i.IssuedForGuid == CustomerId)
            .Include(i => i.IssuedBy).Include(i => i.IssuedFor)
            .ToListAsync());
    }
    var applicationDbContext = _context.Invoices
        .Include(i => i.IssuedBy)
        .Include(i => i.IssuedFor);
    return View(await applicationDbContext.ToListAsync());
}

        // GET: Invoice/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoice = await _context.Invoices
                .Include(i => i.IssuedBy)
                .Include(i => i.IssuedFor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (invoice == null)
            {
                return NotFound();
            }

            return RedirectToAction("Index", "InvoiceItem",
                new { invoice_id = invoice.Id });
            //return View(invoice);
        }

// GET: Invoice/Create
public IActionResult Create(string customer_id)
{        
    var empl = _context.Set<Employee>()
        .FirstOrDefault();
    var cust = _context.Set<Customer>()
        .FirstOrDefault(u=>u.Id == customer_id);
    if(cust == null || empl == null)
        return NotFound();
    return View(new Invoice(){Date=DateTime.Now, 
        IssuedBy=empl, IssuedFor=cust, 
        IssuedByGuid=empl.Id, IssuedForGuid=cust.Id});
}

// POST: Invoice/Create
// To protect from overposting attacks, enable the specific properties you want to bind to.
// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Create([Bind("Id,Date,IssuedForGuid,IssuedByGuid")] Invoice invoice)
{
    var empl = _context.Set<Employee>()
        .FirstOrDefault();
    var cust = _context.Set<Customer>()
        .FirstOrDefault(u=>u.Id == invoice.IssuedForGuid);
    if(cust == null || empl == null)
        return NotFound();
    invoice.IssuedByGuid = empl.Id;
    invoice.IssuedForGuid = cust.Id;               

    if (ModelState.IsValid)
    {
        _context.Add(invoice);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
    return View(invoice);
}


        // GET: Invoice/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoice = await _context.Invoices.FindAsync(id);
            if (invoice == null)
            {
                return NotFound();
            }
            ViewData["IssuedByGuid"] = new SelectList(_context.Set<Employee>(), "Id", "Id", invoice.IssuedByGuid);
            ViewData["IssuedForGuid"] = new SelectList(_context.Customer, "Id", "Id", invoice.IssuedForGuid);
            return View(invoice);
        }

        // POST: Invoice/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Date,IssuedForGuid,IssuedByGuid")] Invoice invoice)
        {
            if (id != invoice.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(invoice);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InvoiceExists(invoice.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IssuedByGuid"] = new SelectList(_context.Set<Employee>(), "Id", "Id", invoice.IssuedByGuid);
            ViewData["IssuedForGuid"] = new SelectList(_context.Customer, "Id", "Id", invoice.IssuedForGuid);
            return View(invoice);
        }

        // GET: Invoice/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoice = await _context.Invoices
                .Include(i => i.IssuedBy)
                .Include(i => i.IssuedFor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (invoice == null)
            {
                return NotFound();
            }

            return View(invoice);
        }

        // POST: Invoice/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var invoice = await _context.Invoices.FindAsync(id);
            _context.Invoices.Remove(invoice);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InvoiceExists(int id)
        {
            return _context.Invoices.Any(e => e.Id == id);
        }
    }
}
