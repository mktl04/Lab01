using Lab01_EFCore.Areas.Customer.Models;
using Lab01_EFCore.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab01_EFCore.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _hosting;
        public ProductController(ApplicationDbContext db, IWebHostEnvironment hosting)
        {
            _db = db;
            _hosting = hosting;
        }
        public IActionResult Index(int? categoryId = 1)
        {
            var categories = _db.Categories.
                             Select(c => new CategoryViewModel
                             { Id = c.Id, Name = c.Name, TotalProduct = _db.Products.Where(p => p.CategoryId == c.Id).Count() }).ToList();
            var products = _db.Products.Where(p => p.CategoryId == categoryId).ToList();

            ViewBag.Categories = categories;
            ViewBag.Products = products;
            ViewBag.Title = _db.Categories.Find(categoryId).Name;
            ViewBag.SelectedCategoryId = categoryId;

            return View();
        }

        // Ajax: Lấy sản phẩm theo category
        [HttpGet]
        public IActionResult Ajax(int categoryId = 1)
        {
            var categories = _db.Categories.
                             Select(c => new CategoryViewModel
                             { Id = c.Id, Name = c.Name, TotalProduct = _db.Products.Where(p => p.CategoryId == c.Id).Count() }).ToList();
            var products = _db.Products.Where(p => p.CategoryId == categoryId).ToList();

            ViewBag.Categories = categories;
            ViewBag.Products = products;
            ViewBag.SelectedCategoryId = categoryId;

            return PartialView("_ProductListPartial", products);
        }
    }
}
