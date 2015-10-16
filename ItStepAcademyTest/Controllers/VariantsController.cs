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
    public class VariantsController : ApiController
    {
        private itsteptestEntities db = new itsteptestEntities();

        // GET api/Variants
        public IEnumerable<variants> Getvariants()
        {
            return db.variants.AsEnumerable();
        }

        // GET api/Variants/5
        public variants Getvariants(int id)
        {
            variants variants = db.variants.Find(id);
            if (variants == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return variants;
        }

        // PUT api/Variants/5
        public HttpResponseMessage Putvariants(int id, variants variants)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != variants.id)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(variants).State = EntityState.Modified;

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

        // POST api/Variants
        public HttpResponseMessage Postvariants(variants variants)
        {
            if (ModelState.IsValid)
            {
                db.variants.Add(variants);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, variants);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = variants.id }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/Variants/5
        public HttpResponseMessage Deletevariants(int id)
        {
            variants variants = db.variants.Find(id);
            if (variants == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.variants.Remove(variants);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, variants);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}