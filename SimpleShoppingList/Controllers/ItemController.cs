using SimpleShoppingList.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SimpleShoppingList.Controllers
{
    public class ItemController : ApiController
    {
        // POST: api/Item
        public IHttpActionResult Post([FromBody]Item item)
        {
            ShoppingList shoppingList = ShoppingListController.shoppingLists.Where(x => x.ID == item.ShoppingListID).FirstOrDefault();

            if(shoppingList == null)
            {
                return  NotFound();
            }

            item.ID = shoppingList.Items.Max(i => i.ID) + 1;
            shoppingList.Items.Add(item);

            return Ok(shoppingList);
        }

        // PUT: api/Item/5
        public IHttpActionResult Put(int id, [FromBody]Item item)
        {
            ShoppingList shoppingList = ShoppingListController.shoppingLists.Where(x => x.ID == item.ShoppingListID).FirstOrDefault();
            if(shoppingList == null)
            {
                return NotFound();
            }

            Item changedItem = shoppingList.Items.Where(x => x.ID == id).FirstOrDefault();
            if (changedItem == null)
            {
                return NotFound();
            }

            changedItem.Checked = item.Checked;
            return Ok(shoppingList);
        }

        // DELETE: api/Item/5
        public IHttpActionResult Delete(int id)
        {
            ShoppingList shoppingList = ShoppingListController.shoppingLists[0];

            Item item = shoppingList.Items.FirstOrDefault(x => x.ID == id);
            if(item == null)
            {
                return NotFound();
            }

            shoppingList.Items.Remove(item);

            return Ok(shoppingList);
        }
    }
}
