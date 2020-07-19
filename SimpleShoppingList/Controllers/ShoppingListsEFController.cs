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
    public class ShoppingListsEFController : ApiController
    {
        private SimpleShoppingListContext db = new SimpleShoppingListContext();

        // GET: api/ShoppingListsEF
        public IQueryable<ShoppingList> GetShoppingLists()
        {
            return db.ShoppingLists;
        }

        // GET: api/ShoppingListsEF/5
        [ResponseType(typeof(ShoppingList))]
        public IHttpActionResult GetShoppingList(int id)
        {
            ShoppingList shoppingList = db.ShoppingLists.Where(x => x.ID == id).Include(x => x.Items).FirstOrDefault();
            if (shoppingList == null)
            {
                return NotFound();
            }

            return Ok(shoppingList);
        }

        // PUT: api/ShoppingListsEF/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutShoppingList(int id, ShoppingList shoppingList)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != shoppingList.ID)
            {
                return BadRequest();
            }

            db.Entry(shoppingList).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShoppingListExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/ShoppingListsEF
        [ResponseType(typeof(ShoppingList))]
        public IHttpActionResult PostShoppingList(ShoppingList shoppingList)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ShoppingLists.Add(shoppingList);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = shoppingList.ID }, shoppingList);
        }

        // DELETE: api/ShoppingListsEF/5
        [ResponseType(typeof(ShoppingList))]
        public IHttpActionResult DeleteShoppingList(int id)
        {
            ShoppingList shoppingList = db.ShoppingLists.Find(id);
            if (shoppingList == null)
            {
                return NotFound();
            }

            db.ShoppingLists.Remove(shoppingList);
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

        private bool ShoppingListExists(int id)
        {
            return db.ShoppingLists.Count(e => e.ID == id) > 0;
        }
    }
}