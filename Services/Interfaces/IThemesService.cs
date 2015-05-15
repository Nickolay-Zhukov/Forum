using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Models;
using Services.DTO;

namespace Services.Interfaces
{
    public interface IThemesService
    {
        IEnumerable<ThemeDto> GetAllThemes();
        Task<ThemeDetailsDto> GetThemeByIdAsync(int id);
        Task<ThemeDto> CreateNewThemeAsync(ThemeDto dto, ApplicationUser user);
        Task<ThemeDto> DeleteThemeByIdAsync(int id);
    }
}