using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Models;
using DAL.Interfaces;
using Services.DTO;
using Services.ExceptionsAndErrors;
using Services.Interfaces;

namespace Services.Implementations
{
    public class ThemesService : BaseService, IThemesService
    {
        private readonly IUnitOfWork _unitOfWork;

        #region Constructor
        public ThemesService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region Special check methods
        private void IsThemeUniq(ThemeDto themeDto)
        {
            if (!_unitOfWork.ThemesRepository.Get(theme => theme.Title == themeDto.Title).Any()) return;
            throw new SameThemeExistsException("Theme with specified title already exists");
        }
        #endregion

        public IEnumerable<ThemeDto> GetAllThemes()
        {
            return _unitOfWork.ThemesRepository.Get().Select(theme => new ThemeDto(theme)).ToList();
        }

        public Task<ThemeDetailsDto> GetThemeByIdAsync(int id)
        {
            var theme = _unitOfWork.ThemesRepository.GetById(id);
            IsEntityExist(theme, theme.Id, "Theme");

            return Task.FromResult(new ThemeDetailsDto(theme)
            {
                Messages = _unitOfWork.MessagesRepository.
                    Get(message => message.ThemeId == id).
                    Select(message => new MessageDto(message)).
                    ToList()
            });
        }

        public Task<ThemeDto> CreateNewThemeAsync(ThemeDto dto, ApplicationUser user)
        {
            IsThemeUniq(dto);

            var newTheme = new Theme { Title  = dto.Title, Owner = user };
            _unitOfWork.ThemesRepository.Insert(newTheme);
            _unitOfWork.SaveChanges();
            
            return Task.FromResult(new ThemeDto(newTheme));
        }

        public Task<ThemeDto> DeleteThemeByIdAsync(int id)
        {
            var theme = _unitOfWork.ThemesRepository.GetById(id);
            IsEntityExist(theme, theme.Id, "Theme");

            _unitOfWork.ThemesRepository.Delete(theme);
            _unitOfWork.SaveChanges();

            return Task.FromResult(new ThemeDto(theme));
        }
    }
}