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
    public class RolesController : ApiController
    {
        private itsteptestEntities db = new itsteptestEntities();

        // GET api/Roles
        public IEnumerable<roles> Getroles()
        {
            return db.roles.AsEnumerable();
        }

        // GET api/Roles/5
        public roles Getroles(int id)
        {
            roles roles = db.roles.Find(id);
            if (roles == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return roles;
        }

        // PUT api/Roles/5
        public HttpResponseMessage Putroles(int id, roles roles)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != roles.id)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(roles).State = EntityState.Modified;

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

        // POST api/Roles
        public HttpResponseMessage Postroles(roles roles)
        {
            if (ModelState.IsValid)
            {
                db.roles.Add(roles);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, roles);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = roles.id }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/Roles/5
        public HttpResponseMessage Deleteroles(int id)
        {
            roles roles = db.roles.Find(id);
            if (roles == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.roles.Remove(roles);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, roles);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}