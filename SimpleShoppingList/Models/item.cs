using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleShoppingList.Models
{
    public class Item
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public bool Checked { get; set; }
        public int ShoppingListID { get; set; }


        public Item()
        {
            ID = 0;
            Name = string.Empty;
            Checked = false;
            ShoppingListID = -1;
        }
    }
}