using System;
using System.Data.Entity.Infrastructure;
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

        #region Special check methods
        private static void IsMessageInTheme(Message message, int themeId)
        {
            if (message.ThemeId == themeId) return;
            var errorMessage = string.Format("Specified theme with id = {0} doesn't match message's theme with id = {1}", themeId, message.ThemeId);
            throw new ActionArgumentException(errorMessage) { ErrorType = DataCheckingErrors.IdsMismatch };
        }

        private static void IsMessageQuoted(Message message, string operationName)
        {
            if (!message.Quotes.Any()) return;
            var errorMessage = string.Format("Specified message with id = {0} is quoted and can't be" + operationName + "ed", message.Id);
            throw new MessageQuotedException(errorMessage);
        }
        #endregion // Special check methods

        public Task<MessageDto> CreateNewMessageAsync(int themeId, MessageDto dto, ApplicationUser user)
        {
            var theme = _unitOfWork.ThemesRepository.GetById(themeId);
            IsEntityExist(theme, theme.Id, "Theme");

            var newMessage = new Message { ThemeId  = themeId, Text = dto.Text, User = user };
            
            _unitOfWork.MessagesRepository.Insert(newMessage);
            _unitOfWork.SaveChanges();

            return Task.FromResult(new MessageDto(newMessage));
        }

        public Task<MessageDto> QuoteMessageAsync(int themeId, int id, MessageDto dto, ApplicationUser user)
        {
            var quotedMessage = _unitOfWork.MessagesRepository.GetById(id);
            IsEntityExist(quotedMessage, id, "Message");
            IsMessageInTheme(quotedMessage, themeId);

            var newMessage = new Message { ThemeId = themeId, Text = dto.Text, User = user, Quote = quotedMessage };
            quotedMessage.Quotes.Add(newMessage);
            
            _unitOfWork.MessagesRepository.Insert(newMessage);
            _unitOfWork.MessagesRepository.Update(quotedMessage);
            _unitOfWork.SaveChanges();

            return Task.FromResult(new MessageDto(newMessage));
        }

        public Task EditMessageAsync(int themeId, int id, MessageDto dto)
        {
            var message = _unitOfWork.MessagesRepository.GetById(id);
            IsEntityExist(message, id, "Message");
            IsMessageInTheme(message, themeId);
            IsMessageQuoted(message, "edit");

            message.Text = dto.Text;
            message.DateTime = DateTime.Now;
            
            _unitOfWork.MessagesRepository.Update(message);
            try
            {
                _unitOfWork.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                IsEntityExist(message, id, "Message");
                throw; 
            }

            return Task.FromResult<object>(null);
        }

        public Task<MessageDto> DeleteMessageAsync(int themeId, int id)
        {
            var message = _unitOfWork.MessagesRepository.GetById(id);
            IsEntityExist(message, id, "Message");
            IsMessageInTheme(message, themeId);
            IsMessageQuoted(message, "remov");

            _unitOfWork.MessagesRepository.Delete(message);
            _unitOfWork.SaveChanges();
            
            return Task.FromResult(new MessageDto(message));
        }
    }
}