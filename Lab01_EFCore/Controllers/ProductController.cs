using Lab01_EFCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Lab01_EFCore.Controllers
{
    public class ProductController : Controller
    {
        private ApplicationDbContext _db;
        private IWebHostEnvironment _hosting;
        public ProductController(ApplicationDbContext db, IWebHostEnvironment host)
        {
            _db = db;
            _hosting = host;
        }
        public IActionResult Index(int page = 1)
        {
            int pageSize = 7;

            var totalItems = _db.Products.Count();
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            var products = _db.Products
                              .Include(x => x.Category)
                              .OrderBy(p => p.CategoryId)
                              .Skip((page - 1) * pageSize)
                              .Take(pageSize)
                              .ToList();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;

            return View(products);
        }
        public IActionResult Add()
        {
            ViewBag.TL = _db.Categories.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name });
            return View();
        }
        [HttpPost]
        public IActionResult Add(Product p, IFormFile ImageUrl)
        {
            if (ModelState.IsValid)
            {
                if (ImageUrl != null)
                {
                    p.ImageUrl = SaveImage(ImageUrl);
                }
                _db.Products.Add(p);
                _db.SaveChanges();
                TempData["success"] = "Thêm sản phẩm thành công";
                return RedirectToAction("Index");
            }
            ViewBag.CategoryList = _db.Categories.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            });
            return View();
        }
        private string SaveImage(IFormFile image)
        {
            var filename = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
            var path = Path.Combine(_hosting.WebRootPath, @"images/products");
            var saveFile = Path.Combine(path, filename);
            using (var filestream = new FileStream(saveFile, FileMode.Create))
            {
                image.CopyTo(filestream);
            }
            return @"images/products/" + filename;
        }
        public IActionResult Update(int id)
        {
            var product = _db.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewBag.CategoryList = _db.Categories.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name });
            return View(product);
        }
        [HttpPost]
        public IActionResult Update(Product p, IFormFile ImageUrl)
        {
            if (ModelState.IsValid)
            {
                var existingProduct = _db.Products.Find(p.Id);
                if (ImageUrl != null)
                {
                    p.ImageUrl = SaveImage(ImageUrl);
                    if (!string.IsNullOrEmpty(existingProduct.ImageUrl))
                    {
                        var oldFilePath = Path.Combine(_hosting.WebRootPath, existingProduct.ImageUrl);
                        if (System.IO.File.Exists(oldFilePath))
                        {
                            System.IO.File.Delete(oldFilePath);
                        }
                    }
                }
                else
                {
                    p.ImageUrl = existingProduct.ImageUrl;
                }
                existingProduct.Name = p.Name;
                existingProduct.Description = p.Description;
                existingProduct.Price = p.Price;
                existingProduct.CategoryId = p.CategoryId;
                existingProduct.ImageUrl = p.ImageUrl;
                _db.SaveChanges();
                TempData["success"] = "Cập nhật sản phẩm thành công";
                return RedirectToAction("Index");
            }
            ViewBag.CategoryList = _db.Categories.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            });
            return View();
        }
        public IActionResult Delete(int id)
        {
            var product = _db.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirming(int id)
        {
            var product = _db.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            if (!String.IsNullOrEmpty(product.ImageUrl))
            {
                var oldFilePath = Path.Combine(_hosting.WebRootPath, product.ImageUrl);
                if (System.IO.File.Exists(oldFilePath))
                {
                    System.IO.File.Delete(oldFilePath);
                }
            }
            _db.Products.Remove(product);
            _db.SaveChanges();
            TempData["success"] = "Xóa sản phẩm thành công";
            return RedirectToAction("Index");
        }
    }
}
