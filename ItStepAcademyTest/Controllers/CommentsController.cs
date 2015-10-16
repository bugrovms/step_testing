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
    public class CommentsController : ApiController
    {
        private itsteptestEntities db = new itsteptestEntities();

        // GET api/Comments
        public IEnumerable<comments> Getcomments()
        {
            return db.comments.AsEnumerable();
        }

        // GET api/Comments/5
        public comments Getcomments(int id)
        {
            comments comments = db.comments.Find(id);
            if (comments == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return comments;
        }

        // PUT api/Comments/5
        public HttpResponseMessage Putcomments(int id, comments comments)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != comments.id)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(comments).State = EntityState.Modified;

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

        // POST api/Comments
        public HttpResponseMessage Postcomments(comments comments)
        {
            if (ModelState.IsValid)
            {
                db.comments.Add(comments);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, comments);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = comments.id }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/Comments/5
        public HttpResponseMessage Deletecomments(int id)
        {
            comments comments = db.comments.Find(id);
            if (comments == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.comments.Remove(comments);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, comments);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}