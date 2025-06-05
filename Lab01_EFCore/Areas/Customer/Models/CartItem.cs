using Lab01_EFCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab01_EFCore.Areas.Customer.Models
{
    public class CartItem
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
