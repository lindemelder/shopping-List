using SimpleShoppingList.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SimpleShoppingList.Controllers
{
    public class ShoppingListController : ApiController
    {
        public static List<ShoppingList> shoppingLists = new List<ShoppingList>
        {
            new ShoppingList() {ID = 0, Name = "Groceries", 
                Items =  {
                    new Item { ID = 0, Name = "Linde", ShoppingListID = 0},
                    new Item {ID = 1, Name = "Hermie", ShoppingListID = 0},
                    new Item {ID = 2, Name = "Humphrey", ShoppingListID = 0}
                }
            },
            new ShoppingList() {ID = 1, Name = "Hardware"}
        };
        // GET: api/ShoppingList/5
        public IHttpActionResult Get(int id)
        {
            ShoppingList result = shoppingLists.FirstOrDefault(s => s.ID == id);

            if(result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // POST: api/ShoppingList
        public IEnumerable Post([FromBody]ShoppingList newList)
        {
            newList.ID = shoppingLists.Count;
            shoppingLists.Add(newList);

            return shoppingLists;
        }
    }
}
