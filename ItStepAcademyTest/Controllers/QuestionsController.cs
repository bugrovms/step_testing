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
    public class QuestionsController : ApiController
    {
        private itsteptestEntities db = new itsteptestEntities();

        // GET api/Questions
        public IEnumerable<questions> Getquestions()
        {
            return db.questions.AsEnumerable();
        }

        // GET api/Questions/5
        public questions Getquestions(int id)
        {
            questions questions = db.questions.Find(id);
            if (questions == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return questions;
        }

        // PUT api/Questions/5
        public HttpResponseMessage Putquestions(int id, questions questions)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != questions.id)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(questions).State = EntityState.Modified;

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

        // POST api/Questions
        public HttpResponseMessage Postquestions(questions questions)
        {
            if (ModelState.IsValid)
            {
                db.questions.Add(questions);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, questions);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = questions.id }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/Questions/5
        public HttpResponseMessage Deletequestions(int id)
        {
            questions questions = db.questions.Find(id);
            if (questions == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.questions.Remove(questions);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, questions);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}