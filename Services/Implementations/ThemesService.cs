using System;
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

        #region Check methods
        private void IsThemeTitleUniq(string themeTitle)
        {
            if (!_unitOfWork.ThemesRepository.Get(theme => theme.Title == themeTitle).Any()) return;
            throw new SameThemeExistsException("Theme with specified title already exists");
        }
        #endregion
        
        public IEnumerable<ThemeDto> GetAllThemes()
        {
            return _unitOfWork.ThemesRepository.Get().Select(theme => new ThemeDto
            {
                Id = theme.Id,
                Title = theme.Title,
                Author = theme.Owner.UserName,
                CreationDateTime = theme.CreationDateTime
            }).ToList();
        }

        public async Task<ThemeDetailsDto> GetThemeByIdAsync(int id)
        {
            var theme = await _unitOfWork.ThemesRepository.GetByIdAsync(id);
            IsEntityExist(theme, id, "Theme");

            return new ThemeDetailsDto(theme) { Messages = theme.Messages.Select(message => new MessageDto(message)) };
        }

        public async Task<ThemeDto> CreateNewThemeAsync(ThemeDto dto, string userId)
        {
            IsDtoNotNull(dto);
            IsThemeTitleUniq(dto.Title);
            var user = await _unitOfWork.UsersRepository.GetByIdAsync(userId);
            IsEntityExist(user, userId, "User");
            
            var newTheme = new Theme { Title  = dto.Title, Owner = user, CreationDateTime = DateTime.Now };
            
            _unitOfWork.ThemesRepository.Insert(newTheme);
            await _unitOfWork.SaveChangesAsync();
            
            return new ThemeDto(newTheme);
        }

        public async Task<ThemeDto> DeleteThemeByIdAsync(int id)
        {
            var theme = await _unitOfWork.ThemesRepository.GetByIdAsync(id);
            IsEntityExist(theme, id, "Theme");

            _unitOfWork.ThemesRepository.Delete(theme);
            await _unitOfWork.SaveChangesAsync();

            return new ThemeDto(theme);
        }
    }
}