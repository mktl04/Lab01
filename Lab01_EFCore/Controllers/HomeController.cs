﻿using Lab01_EFCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Lab01_EFCore.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _db;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }
        public IActionResult Index()
        {
            var pageSize = 4;
            var dsSanPham = _db.Products.Include(x => x.Category).ToList();
            return View(dsSanPham.Skip(0).Take(pageSize).ToList());
        }
        public IActionResult LoadMore(int page = 1)
        {
            var pageSize = 4;
            var dsSanPham = _db.Products.Include(x => x.Category).ToList();
            return PartialView("_ProductPartial", dsSanPham.Skip((page - 1) * pageSize).Take(pageSize).ToList());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
