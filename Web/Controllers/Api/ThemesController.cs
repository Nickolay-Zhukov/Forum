using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Services.DTO;
using Services.Interfaces;
using Web.ControllersBindingModels;
using Web.Core;
using Web.Filters;

namespace Web.Controllers.Api
{
    public class ThemesController : ApiController
    {
        private readonly IThemesService _themesService;
        private ApplicationUserManager UserManager
        {
            get { return Request.GetOwinContext().GetUserManager<ApplicationUserManager>(); }
        }

        #region Constructor
        public ThemesController(IThemesService themesService)
        {
            _themesService = themesService;
        }
        #endregion

        #region Controller actions
        // GET api/themes
        public IEnumerable<ThemeDto> GetThemes()
        {
            return _themesService.GetAllThemes();
        }

        // GET api/themes/5
        [ResponseType(typeof(ThemeDetailsDto))]
        public async Task<IHttpActionResult> GetTheme(int id)
        {
            var themeDetailsDto = await _themesService.GetThemeByIdAsync(id);
            if (themeDetailsDto == null) return NotFound();
            return Ok(themeDetailsDto);
        }

        // POST api/themes
        [Authorize]
        [ValidateModel]
        [ResponseType(typeof(ThemeDto))]
        public async Task<IHttpActionResult> PostTheme(ThemeBindingModel model)
        {
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            var themeDto = await _themesService.CreateNewThemeAsync(user, model.ThemeTitle);
            if (themeDto == null) return BadRequest("Theme with same name already exists");
            return CreatedAtRoute("DefaultApi", new {}, themeDto);
        }

        // DELETE api/themes/5
        [Authorize(Roles = "Admin")]
        [ResponseType(typeof(ThemeDto))]
        public async Task<IHttpActionResult> DeleteTheme(int id)
        {
            var themeDto = await _themesService.DeleteThemeByIdAsync(id);
            if (themeDto == null) return NotFound();
            return Ok(themeDto);
        }
        #endregion // Controller actions
    }
}