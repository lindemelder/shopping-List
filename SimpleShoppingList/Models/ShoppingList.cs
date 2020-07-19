using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleShoppingList.Models
{
    public class ShoppingList
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public List<Item> Items { get; set; }

        //constructor
        public ShoppingList()
        {
            ID = 0;
            Name = string.Empty;
            Items = new List<Item>();
        }
    }
}