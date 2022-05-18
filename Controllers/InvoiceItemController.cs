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
    public class InvoiceItemController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InvoiceItemController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: InvoiceItem
        public async Task<IActionResult> Index(int? invoice_id)
        {
            if (invoice_id != null)
                return View(await _context.IvoiceItems
                    .Where(i => i.InvoiceId == invoice_id)
                    .Include(i => i.Invoice)
                    .ToListAsync());
            var applicationDbContext = _context.IvoiceItems
                .Include(i => i.Invoice);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: InvoiceItem/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoiceItem = await _context.IvoiceItems
                .Include(i => i.Invoice)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (invoiceItem == null)
            {
                return NotFound();
            }

            return View(invoiceItem);
        }

        // GET: InvoiceItem/Create
        public IActionResult Create()
        {
            ViewData["InvoiceId"] = new SelectList(_context.Invoices, "Id", "Id");
            return View();
        }

        // POST: InvoiceItem/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Price,Amount,VAT,InvoiceId")] InvoiceItem invoiceItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(invoiceItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["InvoiceId"] = new SelectList(_context.Invoices, "Id", "Id", invoiceItem.InvoiceId);
            return View(invoiceItem);
        }

        // GET: InvoiceItem/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoiceItem = await _context.IvoiceItems.FindAsync(id);
            if (invoiceItem == null)
            {
                return NotFound();
            }
            ViewData["InvoiceId"] = new SelectList(_context.Invoices, "Id", "Id", invoiceItem.InvoiceId);
            return View(invoiceItem);
        }

        // POST: InvoiceItem/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,Amount,VAT,InvoiceId")] InvoiceItem invoiceItem)
        {
            if (id != invoiceItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(invoiceItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InvoiceItemExists(invoiceItem.Id))
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
            ViewData["InvoiceId"] = new SelectList(_context.Invoices, "Id", "Id", invoiceItem.InvoiceId);
            return View(invoiceItem);
        }

        // GET: InvoiceItem/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoiceItem = await _context.IvoiceItems
                .Include(i => i.Invoice)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (invoiceItem == null)
            {
                return NotFound();
            }

            return View(invoiceItem);
        }

        // POST: InvoiceItem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var invoiceItem = await _context.IvoiceItems.FindAsync(id);
            _context.IvoiceItems.Remove(invoiceItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InvoiceItemExists(int id)
        {
            return _context.IvoiceItems.Any(e => e.Id == id);
        }
    }
}
