using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Models;
using DAL.Interfaces;
using Services.DTO;
using Services.Interfaces;

namespace Services.Implementations
{
    public class ThemesService : IThemesService
    {
        private readonly IUnitOfWork _unitOfWork;

        #region Constructor
        public ThemesService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion

        public IEnumerable<ThemeDto> GetAllThemes()
        {
            return _unitOfWork.ThemesRepository.Get().Select(
                theme => new ThemeDto
                {
                    Id = theme.Id, Title = theme.Title, Author = theme.Owner.UserName
                }).ToList();
        }

        public async Task<ThemeDto> GetThemeByIdAsync(int id)
        {
            var theme = await _unitOfWork.ThemesRepository.GetByIdAsync(id);
            return new ThemeDto { Id = theme.Id, Title = theme.Title, Author = theme.Owner.UserName };
        }

        public async Task<Theme> CreateNewThemeAsync(Theme newTheme, string ownerId)
        {
            // Check theme title uniqueness
            if (_unitOfWork.ThemesRepository.Get(theme => theme.Title == newTheme.Title).Any()) return null;

            var owner = await _unitOfWork.UsersRepository.GetByIdAsync(ownerId);
            newTheme.Owner = owner;
            
            _unitOfWork.ThemesRepository.Insert(newTheme);
            await _unitOfWork.SaveChangesAsync();

            return newTheme;
        }

        public async Task<Theme> DeleteThemeByIdAsync(int id)
        {
            var theme = await _unitOfWork.ThemesRepository.GetByIdAsync(id);
            if (theme == null) return null;

            _unitOfWork.ThemesRepository.Delete(theme);
            await _unitOfWork.SaveChangesAsync();

            return theme;
        }
    }
}