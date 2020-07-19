using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using SimpleShoppingList.Models;

namespace SimpleShoppingList.Controllers
{
    public class ItemsEFController : ApiController
    {
        private SimpleShoppingListContext db = new SimpleShoppingListContext();

        // GET: api/Items
        public IQueryable<Item> GetItems()
        {
            return db.Items;
        }

        // GET: api/Items/5
        [ResponseType(typeof(Item))]
        public IHttpActionResult GetItem(int id)
        {
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        // PUT: api/Items/5
        [ResponseType(typeof(Item))]
        public IHttpActionResult PutItem(int id, Item item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != item.ID)
            {
                return BadRequest();
            }

            db.Entry(item).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(item);
        }

        // POST: api/Items
        [ResponseType(typeof(ShoppingList))]
        public IHttpActionResult PostItem(Item item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //get shopping list by using the shopping list id
            ShoppingList shoppingList = db.ShoppingLists.Where(x => x.ID == item.ShoppingListID).Include(x => x.Items).FirstOrDefault();
            if(shoppingList == null)
            {
                return NotFound();
            }

            db.Items.Add(item);
            db.SaveChanges();

            return Ok(shoppingList);
        }

        // DELETE: api/Items/5
        [ResponseType(typeof(ShoppingList))]
        public IHttpActionResult DeleteItem(int id)
        {
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return NotFound();
            }

            ShoppingList shoppingList = db.ShoppingLists.Where(x => x.ID == item.ShoppingListID).Include(x => x.Items).FirstOrDefault();

            db.Items.Remove(item);
            db.SaveChanges();

            return Ok(shoppingList);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ItemExists(int id)
        {
            return db.Items.Count(e => e.ID == id) > 0;
        }
    }
}