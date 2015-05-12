using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Models;
using Services.DTO;

namespace Services.Interfaces
{
    public interface IThemesService
    {
        IEnumerable<ThemeDto> GetAllThemes();
        Task<ThemeDto> GetThemeByIdAsync(int id);
        Task<Theme> CreateNewThemeAsync(Theme newTheme, string ownerId);
        Task<Theme> DeleteThemeByIdAsync(int id);
    }
}