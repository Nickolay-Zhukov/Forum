using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Core.Models;
using DAL.DbContext;

namespace Web.Controllers.Api
{
    public class ThemeController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET api/Theme
        public IQueryable<Theme> GetThemes()
        {
            return db.Themes;
        }

        // GET api/Theme/5
        [ResponseType(typeof(Theme))]
        public async Task<IHttpActionResult> GetTheme(int id)
        {
            Theme theme = await db.Themes.FindAsync(id);
            if (theme == null)
            {
                return NotFound();
            }

            return Ok(theme);
        }

        // PUT api/Theme/5
        public async Task<IHttpActionResult> PutTheme(int id, Theme theme)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != theme.Id)
            {
                return BadRequest();
            }

            db.Entry(theme).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ThemeExists(id))
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

        // POST api/Theme
        [ResponseType(typeof(Theme))]
        public async Task<IHttpActionResult> PostTheme(Theme theme)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Themes.Add(theme);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = theme.Id }, theme);
        }

        // DELETE api/Theme/5
        [ResponseType(typeof(Theme))]
        public async Task<IHttpActionResult> DeleteTheme(int id)
        {
            Theme theme = await db.Themes.FindAsync(id);
            if (theme == null)
            {
                return NotFound();
            }

            db.Themes.Remove(theme);
            await db.SaveChangesAsync();

            return Ok(theme);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ThemeExists(int id)
        {
            return db.Themes.Count(e => e.Id == id) > 0;
        }
    }
}