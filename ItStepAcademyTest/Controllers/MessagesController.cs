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
    public class MessagesController : ApiController
    {
        private itsteptestEntities db = new itsteptestEntities();

        // GET api/Messages
        public IEnumerable<messages> Getmessages()
        {
            return db.messages.AsEnumerable();
        }

        // GET api/Messages/5
        public messages Getmessages(int id)
        {
            messages messages = db.messages.Find(id);
            if (messages == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return messages;
        }

        // PUT api/Messages/5
        public HttpResponseMessage Putmessages(int id, messages messages)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != messages.id)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(messages).State = EntityState.Modified;

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

        // POST api/Messages
        public HttpResponseMessage Postmessages(messages messages)
        {
            if (ModelState.IsValid)
            {
                db.messages.Add(messages);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, messages);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = messages.id }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/Messages/5
        public HttpResponseMessage Deletemessages(int id)
        {
            messages messages = db.messages.Find(id);
            if (messages == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.messages.Remove(messages);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, messages);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}