using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shop.Areas.Admin.Data;

namespace Shop.Controllers
{
    public class HomeController : Controller
    {
        private DPContext _context;
        public HomeController(DPContext context)
        {
            this._context = context;
        }
        public IActionResult Index()
        {
            ViewBag.Categories = _context.typeProduct;
            ViewBag.Products = _context.product;
            var tShirts = (from products in _context.product
                           where products.Catid==9
                           select products
                           ).ToList();
            ViewBag.TShirt = tShirts;

            var trousers= (from products in _context.product
                           where products.Catid == 10
                           select products
                           ).ToList();
            ViewBag.Trousers = trousers;

            var accessories= (from products in _context.product
                              where products.Catid == 12
                              select products
                           ).ToList();
            ViewBag.Accessories = accessories;

            var socks= (from products in _context.product
                        where products.Catid == 11
                        select products
                           ).ToList();
            ViewBag.Socks = socks;
            return View();
        }
        public IActionResult About()
        {
            return View();
        }
        public IActionResult Men()
        {
            return View();
        }
        public IActionResult Women()
        {
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }
        public IActionResult Single(int? Id)
        {
            var product = _context.product.Where(item => item.Id == Id).ToList();
            ViewBag.Product = product;
            return View();
        }
    }
}
