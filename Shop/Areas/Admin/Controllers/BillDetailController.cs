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
    public class BillDetailController : Controller
    {
        private readonly DPContext _context;

        public BillDetailController(DPContext context)
        {
            _context = context;
        }

        // GET: Admin/BillDetail
        public async Task<IActionResult> Index()
        {
            var dPContext = _context.DetailBill.Include(b => b.Bill).Include(b => b.Product);
            return View(await dPContext.ToListAsync());
        }

        // GET: Admin/BillDetail/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var billDetail = await _context.DetailBill
                .Include(b => b.Bill)
                .Include(b => b.Product)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (billDetail == null)
            {
                return NotFound();
            }

            return View(billDetail);
        }

        // GET: Admin/BillDetail/Create
        public IActionResult Create()
        {
            ViewData["BillId"] = new SelectList(_context.bill, "Id", "Id");
            ViewData["ProductId"] = new SelectList(_context.product, "Id", "Id");
            return View();
        }

        // POST: Admin/BillDetail/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,BillId,ProductId,Amount,Money")] BillDetail billDetail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(billDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BillId"] = new SelectList(_context.bill, "Id", "Id", billDetail.BillId);
            ViewData["ProductId"] = new SelectList(_context.product, "Id", "Id", billDetail.ProductId);
            return View(billDetail);
        }

        // GET: Admin/BillDetail/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var billDetail = await _context.DetailBill.FindAsync(id);
            if (billDetail == null)
            {
                return NotFound();
            }
            ViewData["BillId"] = new SelectList(_context.bill, "Id", "Id", billDetail.BillId);
            ViewData["ProductId"] = new SelectList(_context.product, "Id", "Id", billDetail.ProductId);
            return View(billDetail);
        }

        // POST: Admin/BillDetail/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,BillId,ProductId,Amount,Money")] BillDetail billDetail)
        {
            if (id != billDetail.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(billDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BillDetailExists(billDetail.ID))
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
            ViewData["BillId"] = new SelectList(_context.bill, "Id", "Id", billDetail.BillId);
            ViewData["ProductId"] = new SelectList(_context.product, "Id", "Id", billDetail.ProductId);
            return View(billDetail);
        }

        // GET: Admin/BillDetail/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var billDetail = await _context.DetailBill
                .Include(b => b.Bill)
                .Include(b => b.Product)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (billDetail == null)
            {
                return NotFound();
            }

            return View(billDetail);
        }

        // POST: Admin/BillDetail/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var billDetail = await _context.DetailBill.FindAsync(id);
            _context.DetailBill.Remove(billDetail);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BillDetailExists(int id)
        {
            return _context.DetailBill.Any(e => e.ID == id);
        }
    }
}
