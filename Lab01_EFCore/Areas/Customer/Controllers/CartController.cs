using Lab01_EFCore.Areas.Customer.Models;
using Lab01_EFCore.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab01_EFCore.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CartController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            Cart cart = HttpContext.Session.GetJSon<Cart>("CART");
            if(cart == null)
            {
                cart = new Cart();
            }
            return View(cart);
        }
        public IActionResult AddToCart(int productId)
        {
            var product = _db.Products.FirstOrDefault(x => x.Id == productId);
            if(product != null)
            {
                Cart cart = HttpContext.Session.GetJSon<Cart>("CART");
                if(cart == null)
                {
                    cart = new Cart();
                }
                cart.Add(product, 1);
                HttpContext.Session.SetJSon("CART", cart);
                return RedirectToAction("Index");
            }
            return Json(new { msg = "error" });
        }
        public IActionResult Update(int productId, int quanty)
        {
            var product = _db.Products.FirstOrDefault(x => x.Id == productId);
            if(product != null)
            {
                Cart cart = HttpContext.Session.GetJSon<Cart>("CART");
                if(cart != null)
                {
                    cart.Update(productId, quanty);
                    HttpContext.Session.SetJSon("CART", cart);
                    return RedirectToAction("Index");
                }
            }
            return Json(new { msg = "error" });
        }
        public IActionResult Remove(int productId)
        {
            var product = _db.Products.FirstOrDefault(x => x.Id == productId);
            if(product != null)
            {
                Cart cart = HttpContext.Session.GetJSon<Cart>("CART");
                if(cart != null)
                {
                    cart.Remove(productId);
                    HttpContext.Session.SetJSon("CART", cart);
                    return RedirectToAction("Index");
                }
            }
            return NotFound();
        }
        public IActionResult AddToCartAPI(int productId)
        {
            var product = _db.Products.FirstOrDefault(x => x.Id == productId);
            if (product != null)
            {
                Cart cart = HttpContext.Session.GetJSon<Cart>("CART");
                if (cart == null)
                {
                    cart = new Cart();
                }
                cart.Add(product, 1);
                HttpContext.Session.SetJSon("CART", cart);
                return Json(new { msg = "Product added to cart", qty = cart.Quantity });
            }
            return Json(new { msg = "error" });
        }
        public IActionResult GetQuantityOfCart()
        {
            Cart cart = HttpContext.Session.GetJSon<Cart>("CART");
            if (cart != null)
            {

                return Json(new { qty = cart.Quantity });
            }
            return Json(new { qty = 0 });
        }
    }
}
