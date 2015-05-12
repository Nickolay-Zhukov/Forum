using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Core.Models;
using Microsoft.AspNet.Identity;
using Services.DTO;
using Services.Interfaces;
using Web.BindingModels;

namespace Web.Controllers.Api
{
    public class ThemesController : ApiController
    {
        private readonly IThemesService _themesService;

        #region Constructor
        public ThemesController(IThemesService themesService)
        {
            _themesService = themesService;
        }
        #endregion

        // GET api/Themes
        public IEnumerable<ThemeDto> GetThemes()
        {
            return _themesService.GetAllThemes();
        }

        // GET api/Themes/5
        [ResponseType(typeof(ThemeDto))]
        public async Task<IHttpActionResult> GetTheme(int id)
        {
            var theme = await _themesService.GetThemeByIdAsync(id);
            if (theme == null) return NotFound();
            return Ok(theme);
        }

        // POST api/Themes
        [Authorize]
        [ResponseType(typeof(Theme))]
        public async Task<IHttpActionResult> PostTheme(ThemeBindingModel themeBindingModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var theme = await _themesService.CreateNewThemeAsync(
                new Theme { Title = themeBindingModel.ThemeTitle },
                User.Identity.GetUserId());

            if (theme == null) return Conflict();

            return CreatedAtRoute("DefaultApi", new { id = theme.Id }, theme);
        }

        // DELETE api/Themes/5
        [Authorize(Roles = "Admin")]
        [ResponseType(typeof(Theme))]
        public async Task<IHttpActionResult> DeleteTheme(int id)
        {
            var theme = await _themesService.DeleteThemeByIdAsync(id);
            if (theme == null) return NotFound();
            return Ok(theme);
        }
    }
}