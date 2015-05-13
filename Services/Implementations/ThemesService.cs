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
                    Title = theme.Title, Author = theme.Owner.UserName
                }).ToList();
        }

        public Task<ThemeDetailsDto> GetThemeByIdAsync(int id)
        {
            var theme = _unitOfWork.ThemesRepository.GetById(id);
            if (theme == null) return null;

            var dto = new ThemeDetailsDto
            {
                Title = theme.Title,
                Author = theme.Owner.UserName,
                Messages = _unitOfWork.MessagesRepository.
                    Get(message => message.ThemeId == id).
                    Select(message => new MessageDto
                    {
                        ThemeId = id,
                        Author = message.User.UserName,
                        Text = message.Text
                    }).ToList()
            };

            return Task.FromResult(dto);
        }

        public Task<ThemeDto> CreateNewThemeAsync(ApplicationUser user, string themeTitle)
        {
            // Check theme title uniq
            if (_unitOfWork.ThemesRepository.Get(theme => theme.Title == themeTitle).Any()) return null;

            var newTheme = new Theme { Title  = themeTitle, Owner = user };
            _unitOfWork.ThemesRepository.Insert(newTheme);
            _unitOfWork.SaveChanges();

            var dto = new ThemeDto { Title = themeTitle, Author = user.UserName };
            return Task.FromResult(dto);
        }

        public Task<ThemeDto> DeleteThemeByIdAsync(int id)
        {
            var theme = _unitOfWork.ThemesRepository.GetById(id);
            if (theme == null) return null;

            _unitOfWork.ThemesRepository.Delete(theme);
            _unitOfWork.SaveChanges();

            var dto = new ThemeDto { Title = theme.Title, Author = theme.Owner.UserName };
            return Task.FromResult(dto);
        }
    }
}