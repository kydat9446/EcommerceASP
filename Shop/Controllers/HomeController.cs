using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
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
            ViewBag.Products = _context.product;
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
        public IActionResult Register()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Cart()
        {
            return View();
        }
        public async Task<IActionResult> Search(string search)
        {
            var movies = from m in _context.product select m;
            if (!String.IsNullOrEmpty(search))
            {
                movies = movies.Where(s => s.Name.Contains(search));

            }
            return View(await movies.ToListAsync());
        }

        //GET ALL PRODUCT
        public List<Product> getAllProduct()
        {
            return _context.product.ToList();
        }



        public Product getDetailProduct(int id)
        {
            var product = _context.product.Find(id);
            return product;
        }

        //ADD CART
        public IActionResult addCart(int id)
        {
            var cart = HttpContext.Session.GetString("cart");//get key cart
            if (cart == null)
            {
                var product = getDetailProduct(id);
                List<Cart> listCart = new List<Cart>()
               {
                   new Cart
                   {
                       Product = product,
                       Quantity = 1
                   }
               };
                HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(listCart));

            }
            else
            {
                List<Cart> dataCart = JsonConvert.DeserializeObject<List<Cart>>(cart);
                bool check = true;
                for (int i = 0; i < dataCart.Count; i++)
                {
                    if (dataCart[i].Product.Id == id)
                    {
                        dataCart[i].Quantity++;
                        check = false;
                    }
                }
                if (check)
                {
                    dataCart.Add(new Cart
                    {
                        Product = getDetailProduct(id),
                        Quantity = 1
                    });
                }
                HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(dataCart));
                // var cart2 = HttpContext.Session.GetString("cart");//get key cart
                //  return Json(cart2);
            }

            return RedirectToAction(nameof(ListCart));

        }


        public IActionResult ListCart()
        {
            var cart = HttpContext.Session.GetString("cart");//get key cart
            if (cart != null)
            {
                List<Cart> dataCart = JsonConvert.DeserializeObject<List<Cart>>(cart);
                if (dataCart.Count > 0)
                {
                    ViewBag.carts = dataCart;
                    return View();
                }
            }
            // xu ly khong co san pham trong gio hang
            
            return RedirectToAction(nameof(Cart));
        }

        //update Cart
        [HttpPost]
        public IActionResult updateCart(int id, int quantity)
        {
            var cart = HttpContext.Session.GetString("cart");
            if (cart != null)
            {
                List<Cart> dataCart = JsonConvert.DeserializeObject<List<Cart>>(cart);
                if (quantity > 0)
                {
                    for (int i = 0; i < dataCart.Count; i++)
                    {
                        if (dataCart[i].Product.Id == id)
                        {
                            dataCart[i].Quantity = quantity;
                        }
                    }


                    HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(dataCart));
                }
                var cart2 = HttpContext.Session.GetString("cart");
                return Ok(quantity);
            }
            return BadRequest();

        }

        //Delete
        public IActionResult deleteCart(int id)
        {
            var cart = HttpContext.Session.GetString("cart");
            if (cart != null)
            {
                List<Cart> dataCart = JsonConvert.DeserializeObject<List<Cart>>(cart);

                for (int i = 0; i < dataCart.Count; i++)
                {
                    if (dataCart[i].Product.Id == id)
                    {
                        dataCart.RemoveAt(i);
                    }
                }
                HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(dataCart));
                return RedirectToAction(nameof(ListCart));
            }
            return RedirectToAction(nameof(Index));
        }

    }
}
