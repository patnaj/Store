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
    public class CustomerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CustomerController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Customer
        public async Task<IActionResult> Index()
        {
            return View(await _context.Customer.ToListAsync());
        }

        // GET: Customer/Details/5
public async Task<IActionResult> Details(string id)
{
    if (id == null)
    {
        return NotFound();
    }

    var customer = await _context.Customer
        .FirstOrDefaultAsync(m => m.Id == id);
    if (customer == null)
    {
        return NotFound();
    }

    //return View(customer);
    return RedirectToAction("Index","Invoice", new {CustomerId=customer.Id});
}

        // GET: Customer/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Customer/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FullName,Address,Id,UserName,NormalizedUserName,Email,NormalizedEmail,EmailConfirmed,PasswordHash,SecurityStamp,ConcurrencyStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEnd,LockoutEnabled,AccessFailedCount")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(customer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

// GET: Customer/Edit/5
public async Task<IActionResult> Edit(string id)
{
    if (id == null)
    {
        return NotFound();
    }

    var customer = await _context.Customer.FindAsync(id);
    if (customer == null)
    {
        return NotFound();
    }
    var model = new CustomerVM(){Id = customer.Id, 
        Address = customer.Address, 
        FullName = customer.FullName};
    return View("EditVM", model);
}

// POST: Customer/Edit/5
// To protect from overposting attacks, enable the specific properties you want to bind to.
// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Edit(string id, [Bind("FullName,Address,Id")] CustomerVM customer)
{
    if (id != customer.Id)
    {
        return NotFound();
    }

    if (ModelState.IsValid)
    {
        try
        {
            var model = _context.Customer
                .FirstOrDefault(c=>c.Id == id);
            if (model == null)
                return NotFound();
            model.FullName = customer.FullName;
            model.Address = customer.Address;
            _context.Update(model);
            _context.SaveChanges();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!CustomerExists(customer.Id))
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
    return View("EditVM",customer);
}

        // GET: Customer/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customer
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: Customer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var customer = await _context.Customer.FindAsync(id);
            _context.Customer.Remove(customer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerExists(string id)
        {
            return _context.Customer.Any(e => e.Id == id);
        }
    }
}
