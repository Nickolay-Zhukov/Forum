using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.AspNet.Identity;
using Services.DTO;
using Services.Interfaces;

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

        #region Controller actions
        // GET api/Themes
        public IEnumerable<ThemeDto> GetThemes()
        {
            return _themesService.GetAllThemes();
        }

        // GET api/Themes/5
        [ResponseType(typeof(ThemeDetailsDto))]
        public async Task<IHttpActionResult> GetTheme(int id)
        {
            return Ok(await _themesService.GetThemeByIdAsync(id));
        }

        // POST api/Themes
        [Authorize]
        [ResponseType(typeof(ThemeDto))]
        public async Task<IHttpActionResult> PostTheme(ThemeDto requestDto)
        {
            var responseDto = await _themesService.CreateNewThemeAsync(requestDto, User.Identity.GetUserId());
            return CreatedAtRoute("DefaultApi", new { id = responseDto.Id }, responseDto);
        }

        // DELETE api/Themes/5
        [Authorize(Roles = "admin")]
        [ResponseType(typeof(ThemeDto))]
        public async Task<IHttpActionResult> DeleteTheme(int id)
        {
            return Ok(await _themesService.DeleteThemeByIdAsync(id));
        }
        #endregion // Controller actions
    }
}