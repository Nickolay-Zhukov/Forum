using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Services.DTO;
using Services.Interfaces;
using Web.Core;

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
            return Ok(await _themesService.GetThemeByIdAsync(id));
        }

        // POST api/themes
        [Authorize]
        [ResponseType(typeof(ThemeDto))]
        public async Task<IHttpActionResult> PostTheme(ThemeDto requestDto)
        {
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            var responseDto = await _themesService.CreateNewThemeAsync(requestDto, user);
            return CreatedAtRoute("DefaultApi", new { id = responseDto.Id }, responseDto);
        }

        // DELETE api/themes/5
        [Authorize(Roles = "Admin")]
        [ResponseType(typeof(ThemeDto))]
        public async Task<IHttpActionResult> DeleteTheme(int id)
        {
            return Ok(await _themesService.DeleteThemeByIdAsync(id));
        }
        #endregion // Controller actions
    }
}