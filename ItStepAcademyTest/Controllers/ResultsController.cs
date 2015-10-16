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
    public class ResultsController : ApiController
    {
        private itsteptestEntities db = new itsteptestEntities();

        // GET api/Results
        public IEnumerable<results> Getresults()
        {
            return db.results.AsEnumerable();
        }

        // GET api/Results/5
        public results Getresults(int id)
        {
            results results = db.results.Find(id);
            if (results == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return results;
        }

        // PUT api/Results/5
        public HttpResponseMessage Putresults(int id, results results)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != results.id)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(results).State = EntityState.Modified;

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

        // POST api/Results
        public HttpResponseMessage Postresults(results results)
        {
            if (ModelState.IsValid)
            {
                db.results.Add(results);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, results);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = results.id }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/Results/5
        public HttpResponseMessage Deleteresults(int id)
        {
            results results = db.results.Find(id);
            if (results == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.results.Remove(results);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, results);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}