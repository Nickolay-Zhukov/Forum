using System;
using System.Linq;
using System.Threading.Tasks;
using Core.Models;
using DAL.Interfaces;
using Services.DTO;
using Services.ExceptionsAndErrors;
using Services.Interfaces;

namespace Services.Implementations
{
    public class MessageService : BaseService, IMessageService
    {
        private readonly IUnitOfWork _unitOfWork;

        #region Constructor
        public MessageService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region Check methods
        private static void IsMessageInTheme(Message message, int themeId)
        {
            if (message.ThemeId == themeId) return;
            var errorMessage = string.Format("Specified theme with id = {0} doesn't match message's theme with id = {1}", themeId, message.ThemeId);
            throw new ActionArgumentException(errorMessage) { ErrorType = DataCheckingErrors.IdsMismatch };
        }

        private async Task CheckMessageOwner(Message message, string userId)
        {
            var user = await _unitOfWork.UsersRepository.GetByIdAsync(userId);
            if (user.UserName.ToLower() == "admin" || message.UserId == userId) return;
            throw new AccessDeniedException("Current user doesn't have access rights to perform the requested operation");
        }

        private static void IsMessageQuoted(Message message, string operationName)
        {
            if (!message.Quotes.Any()) return;
            var errorMessage = string.Format("Specified message with id = {0} is quoted and can't be " + operationName.TrimEnd('e') + "ed", message.Id);
            throw new MessageQuotedException(errorMessage);
        }
        #endregion // Check methods

        public async Task<MessageDto> CreateNewMessageAsync(int themeId, MessageDto dto, string userId)
        {
            IsDtoNotNull(dto);
            var theme = await _unitOfWork.ThemesRepository.GetByIdAsync(themeId);
            IsEntityExist(theme, themeId, "Theme");
            var user = await _unitOfWork.UsersRepository.GetByIdAsync(userId);
            IsEntityExist(user, userId, "User");

            var newMessage = new Message { Text = dto.Text, Theme  = theme, User = user, CreationDateTime = DateTime.Now };
            
            _unitOfWork.MessagesRepository.Insert(newMessage);
            await _unitOfWork.SaveChangesAsync();

            return new MessageDto(newMessage);
        }

        public async Task<MessageDto> QuoteMessageAsync(int themeId, int id, MessageDto dto, string userId)
        {
            IsDtoNotNull(dto);
            var quotedMessage = await _unitOfWork.MessagesRepository.GetByIdAsync(id);
            IsEntityExist(quotedMessage, id, "Message");
            IsMessageInTheme(quotedMessage, themeId);
            var user = await _unitOfWork.UsersRepository.GetByIdAsync(userId);
            IsEntityExist(user, userId, "User");

            var newMessage = new Message { Text = dto.Text, Theme = quotedMessage.Theme, User = user, CreationDateTime = DateTime.Now, Quote = quotedMessage };
            quotedMessage.Quotes.Add(newMessage);
            
            _unitOfWork.MessagesRepository.Insert(newMessage);
            _unitOfWork.MessagesRepository.Update(quotedMessage);
            await _unitOfWork.SaveChangesAsync();

            return new MessageDto(newMessage);
        }

        public async Task EditMessageAsync(int themeId, int id, MessageDto dto, string userId)
        {
            IsDtoNotNull(dto);
            var message = await _unitOfWork.MessagesRepository.GetByIdAsync(id);
            IsEntityExist(message, id, "Message");
            IsMessageInTheme(message, themeId);
            await CheckMessageOwner(message, userId);
            IsMessageQuoted(message, "edit");

            message.Text = dto.Text;
            message.CreationDateTime = DateTime.Now;
            
            _unitOfWork.MessagesRepository.Update(message);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<MessageDto> DeleteMessageAsync(int themeId, int id, string userId)
        {
            var message = await _unitOfWork.MessagesRepository.GetByIdAsync(id);
            IsEntityExist(message, id, "Message");
            IsMessageInTheme(message, themeId);
            await CheckMessageOwner(message, userId);
            IsMessageQuoted(message, "remov");

            _unitOfWork.MessagesRepository.Delete(message);
            await _unitOfWork.SaveChangesAsync();
            
            return new MessageDto(message);
        }
    }
}