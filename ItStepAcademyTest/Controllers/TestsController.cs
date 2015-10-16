using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using ItStepAcademyTest.Models;

namespace ItStepAcademyTest.Controllers
{
    public class TestsController : ApiController
    {
        private itsteptestEntities db = new itsteptestEntities();

        // GET api/Tests
        public IEnumerable<tests> Gettests()
        {
            return db.tests.AsEnumerable();
        }

        // GET api/Tests/5
        public tests Gettests(int id)
        {
            tests tests = db.tests.Find(id);
            if (tests == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return tests;
        }

        // PUT api/Tests/5
        public HttpResponseMessage Puttests(int id, tests tests)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != tests.id)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(tests).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // POST api/Tests
        public HttpResponseMessage Posttests(tests tests)
        {
            if (ModelState.IsValid)
            {
                db.tests.Add(tests);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, tests);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = tests.id }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/Tests/5
        public HttpResponseMessage Deletetests(int id)
        {
            tests tests = db.tests.Find(id);
            if (tests == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.tests.Remove(tests);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, tests);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}