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

namespace ItStepAcademyTest.Controllers.Admin
{
    public class GroupsController : ApiController
    {
        private itsteptestEntities db = new itsteptestEntities();

        // GET api/Groups
        public IEnumerable<groups> Getgroups()
        {
            return db.groups.AsEnumerable();
        }

        // GET api/Groups/5
        public groups Getgroups(int id)
        {
            groups groups = db.groups.Find(id);
            if (groups == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return groups;
        }

        // PUT api/Groups/5
        public HttpResponseMessage Putgroups(int id, groups groups)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != groups.id)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(groups).State = EntityState.Modified;

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

        // POST api/Groups
        public HttpResponseMessage Postgroups(groups groups)
        {
            if (ModelState.IsValid)
            {
                db.groups.Add(groups);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, groups);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = groups.id }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/Groups/5
        public HttpResponseMessage Deletegroups(int id)
        {
            groups groups = db.groups.Find(id);
            if (groups == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.groups.Remove(groups);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, groups);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}