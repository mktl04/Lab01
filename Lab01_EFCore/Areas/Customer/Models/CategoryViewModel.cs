using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab01_EFCore.Areas.Customer.Models
{
    public class CategoryViewModel
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public int TotalProduct { get; set; }
    }
}
