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
    public class SubjectsController : ApiController
    {
        private itsteptestEntities db = new itsteptestEntities();

        // GET api/Subjects
        public IEnumerable<subjects> Getsubjects()
        {
            return db.subjects.AsEnumerable();
        }

        // GET api/Subjects/5
        public subjects Getsubjects(int id)
        {
            subjects subjects = db.subjects.Find(id);
            if (subjects == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return subjects;
        }

        // PUT api/Subjects/5
        public HttpResponseMessage Putsubjects(int id, subjects subjects)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != subjects.id)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(subjects).State = EntityState.Modified;

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

        // POST api/Subjects
        public HttpResponseMessage Postsubjects(subjects subjects)
        {
            if (ModelState.IsValid)
            {
                db.subjects.Add(subjects);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, subjects);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = subjects.id }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/Subjects/5
        public HttpResponseMessage Deletesubjects(int id)
        {
            subjects subjects = db.subjects.Find(id);
            if (subjects == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.subjects.Remove(subjects);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, subjects);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}