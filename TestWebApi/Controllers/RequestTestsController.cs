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
using TestWebApi.Models;

namespace TestWebApi.Controllers
{
    public class RequestTestsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/RequestTests
        public IQueryable<RequestTest> GetRequestTests()
        {
            return db.RequestTests;
        }

        // GET: api/RequestTests/5
        [ResponseType(typeof(RequestTest))]
        public IHttpActionResult GetRequestTest(int id)
        {
            RequestTest requestTest = db.RequestTests.Find(id);
            if (requestTest == null)
            {
                return NotFound();
            }

            return Ok(requestTest);
        }

        // PUT: api/RequestTests/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutRequestTest(int id, RequestTest requestTest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != requestTest.Id)
            {
                return BadRequest();
            }

            db.Entry(requestTest).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RequestTestExists(id))
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

        // POST: api/RequestTests
        [ResponseType(typeof(RequestTest))]
        public IHttpActionResult PostRequestTest(RequestTest requestTest)
        {
            if(requestTest == null)
            {
                ModelState.AddModelError("Global", "No body posted.");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //db.RequestTests.Add(requestTest);
            //db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = requestTest.Id }, requestTest);
        }

        // DELETE: api/RequestTests/5
        [ResponseType(typeof(RequestTest))]
        public IHttpActionResult DeleteRequestTest(int id)
        {
            RequestTest requestTest = db.RequestTests.Find(id);
            if (requestTest == null)
            {
                return NotFound();
            }

            db.RequestTests.Remove(requestTest);
            db.SaveChanges();

            return Ok(requestTest);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RequestTestExists(int id)
        {
            return db.RequestTests.Count(e => e.Id == id) > 0;
        }
    }
}