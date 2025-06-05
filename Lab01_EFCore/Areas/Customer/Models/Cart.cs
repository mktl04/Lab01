using Lab01_EFCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab01_EFCore.Areas.Customer.Models
{
    public class Cart
    {
        private List<CartItem> _items;
        public Cart()
        {
            _items = new List<CartItem>();
        }
        public List<CartItem> Items { get { return _items; } }
        public void Add(Product p, int quanty)
        {
            var item = _items.FirstOrDefault(x => x.Product.Id == p.Id);
            if (item == null)
            {
                _items.Add(new CartItem { Product = p, Quantity = quanty });
            }
            else
            {
                item.Quantity += quanty;
            }
        }
        public void Update(int productId, int quanty)
        {
            var item = _items.FirstOrDefault(x => x.Product.Id == productId);
            if (item != null)
            {
                if(quanty > 0)
                {
                    item.Quantity = quanty;
                }
            }
            else
            {
                _items.Remove(item);
            }
        }
        public void Remove(int productId)
        {
            var item = _items.FirstOrDefault(x => x.Product.Id == productId);
            if (item != null)
            {
                _items.Remove(item);
            }
        }
        public double Total
        {
            get
            {
                double total = _items.Sum(x => x.Quantity * x.Product.Price);
                return total;
            }
        }
        public double Quantity
        {
            get
            {
                double total = _items.Sum(x => x.Quantity);
                return total;
            }
        }
    }
}
