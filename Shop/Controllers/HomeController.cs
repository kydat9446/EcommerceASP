using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shop.Areas.Admin.Data;
using Shop.Areas.Admin.Models;
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
                           orderby products.Id descending
                           select  products
                           ).Take(4).ToList();
            ViewBag.TShirt = tShirts;

            var trousers= (from products in _context.product
                           where products.Catid == 10
                           orderby products.Id descending
                           select products
                           ).Take(4).ToList();
            ViewBag.Trousers = trousers;

            var accessories= (from products in _context.product
                              where products.Catid == 12
                              orderby products.Id descending
                              select products
                           ).Take(4).ToList();
            ViewBag.Accessories = accessories;

            var socks= (from products in _context.product
                        where products.Catid == 11
                        orderby products.Id descending
                        select products
                           ).Take(4).ToList();
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

            var sameProducts = (from p in product
                                join c in _context.product on p.Catid equals c.Catid
                                orderby c.Id descending
                                select c
                                ).Take(4).ToList();
            ViewBag.SameProducts = sameProducts;
            var productsNew = (from p in _context.product
                               orderby p.Id descending
                               select p
                             ).Take(8).ToList();
            ViewBag.NewProducts = productsNew;
            return View();
        }
    }
}
