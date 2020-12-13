using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Shop.Areas.Admin.Data;
using Shop.Areas.Admin.Models;

namespace Shop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BillController : Controller
    {
        private readonly DPContext _context;

        public BillController(DPContext context)
        {
            _context = context;
        }

        // GET: Admin/Bill
        public async Task<IActionResult> Index()
        {
            var dPContext = _context.bill.Include(b => b.Customer);
            return View(await dPContext.ToListAsync());
        }

        // GET: Admin/Bill/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bill = await _context.bill
                .Include(b => b.Customer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bill == null)
            {
                return NotFound();
            }

            return View(bill);
        }

        // GET: Admin/Bill/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.account, "Id", "Id");
            return View();
        }

        // POST: Admin/Bill/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CustomerId,Total,Date,Status")] Bill bill)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bill);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.account, "Id", "Id", bill.CustomerId);
            return View(bill);
        }

        // GET: Admin/Bill/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bill = await _context.bill.FindAsync(id);
            if (bill == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.account, "Id", "Id", bill.CustomerId);
            return View(bill);
        }

        // POST: Admin/Bill/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CustomerId,Total,Date,Status")] Bill bill)
        {
            if (id != bill.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bill);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BillExists(bill.Id))
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
            ViewData["CustomerId"] = new SelectList(_context.account, "Id", "Id", bill.CustomerId);
            return View(bill);
        }

        // GET: Admin/Bill/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bill = await _context.bill
                .Include(b => b.Customer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bill == null)
            {
                return NotFound();
            }

            return View(bill);
        }

        // POST: Admin/Bill/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bill = await _context.bill.FindAsync(id);
            _context.bill.Remove(bill);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BillExists(int id)
        {
            return _context.bill.Any(e => e.Id == id);
        }
    }
}
