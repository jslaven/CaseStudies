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
using CaseStudies.Models;

namespace CaseStudies.api
{
    [Queryable]
    public class ToDoTaskController : ApiController
    {
        private ToDoContext db = new ToDoContext();

        // GET api/ToDoTask
        public IQueryable<ToDoTask> GetTasks()
        {
            return db.Tasks;
        }

        // GET api/ToDoTask/5
        [ResponseType(typeof(ToDoTask))]
        public IHttpActionResult GetToDoTask(int id)
        {
            ToDoTask todotask = db.Tasks.Find(id);
            if (todotask == null)
            {
                return NotFound();
            }

            return Ok(todotask);
        }

        // PUT api/ToDoTask/5
        public IHttpActionResult PutToDoTask(int id, ToDoTask todotask)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != todotask.ID)
            {
                return BadRequest();
            }

            db.Entry(todotask).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ToDoTaskExists(id))
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

        // POST api/ToDoTask
        [ResponseType(typeof(ToDoTask))]
        public IHttpActionResult PostToDoTask(ToDoTask todotask)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Tasks.Add(todotask);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = todotask.ID }, todotask);
        }

        // DELETE api/ToDoTask/5
        [ResponseType(typeof(ToDoTask))]
        public IHttpActionResult DeleteToDoTask(int id)
        {
            ToDoTask todotask = db.Tasks.Find(id);
            if (todotask == null)
            {
                return NotFound();
            }

            db.Tasks.Remove(todotask);
            db.SaveChanges();

            return Ok(todotask);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ToDoTaskExists(int id)
        {
            return db.Tasks.Count(e => e.ID == id) > 0;
        }
    }
}