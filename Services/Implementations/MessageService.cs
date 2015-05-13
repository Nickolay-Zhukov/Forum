using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using Core.Models;
using DAL.Interfaces;
using Services.DTO;
using Services.Interfaces;
using Services.Results;

namespace Services.Implementations
{
    class MessageService : IMessageService
    {
        private readonly IUnitOfWork _unitOfWork;

        #region Constructor
        public MessageService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion

        public Task<MessageDto> CreateNewMessageAsync(int themeId, ApplicationUser user, string messageText)
        {
            // Check that specified theme exists
            if (!_unitOfWork.MessagesRepository.Get(theme => theme.Id == themeId).Any()) return null;

            var newMessage = new Message { ThemeId  = themeId, Text = messageText, User = user };
            _unitOfWork.MessagesRepository.Insert(newMessage);
            _unitOfWork.SaveChanges();

            var messageDto = new MessageDto { ThemeId = themeId, Author = user.UserName, Text = messageText };
            return Task.FromResult(messageDto);
        }

        public Task<MessageDto> QuoteMessageAsync(int id, ApplicationUser user, string messageText)
        {
            var quotedMessage = _unitOfWork.MessagesRepository.GetById(id);
            if (quotedMessage == null) return null;

            var newMessage = new Message
            {
                ThemeId = quotedMessage.ThemeId, Text = messageText, User = user, Quote = quotedMessage
            };
            quotedMessage.Quotes.Add(newMessage);
            
            _unitOfWork.MessagesRepository.Insert(newMessage);
            _unitOfWork.MessagesRepository.Update(quotedMessage);
            _unitOfWork.SaveChanges();

            var messageDto = new MessageDto { ThemeId = newMessage.ThemeId, Author = user.UserName, Text = messageText };
            return Task.FromResult(messageDto);
        }

        public Task<ServiceResult<MessageDto>> EditMessageAsync(int id, string messageText)
        {
            var message = _unitOfWork.MessagesRepository.GetById(id);
            if (message == null)
            {
                var errorResult = new ServiceResult<MessageDto> { ErrorType = ErrorType.EntityNotFound };
                return Task.FromResult(errorResult);
            }

            if (message.Quotes.Any())
            {
                var errorResult = new ServiceResult<MessageDto>
                {
                    ErrorType = ErrorType.ImpossibleOperation,
                    ErrorMessage = "This message is quoted and can not be updated"
                };
                return Task.FromResult(errorResult);
            }

            message.Text = messageText;
            _unitOfWork.MessagesRepository.Update(message);
            try
            {
                _unitOfWork.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (_unitOfWork.MessagesRepository.GetById(id) != null) throw;
                var errorResult = new ServiceResult<MessageDto> { ErrorType = ErrorType.EntityNotFound };
                return Task.FromResult(errorResult);
            }

            var okResult = new ServiceResult<MessageDto> { ErrorType = ErrorType.OkNoContent };
            return Task.FromResult(okResult);
        }

        public Task<ServiceResult<MessageDto>> DeleteMessageAsync(int id)
        {
            var message = _unitOfWork.MessagesRepository.GetById(id);
            if (message == null)
            {
                var errorResult = new ServiceResult<MessageDto> { ErrorType = ErrorType.EntityNotFound };
                return Task.FromResult(errorResult);
            }

            if (message.Quotes.Any())
            {
                var errorResult = new ServiceResult<MessageDto>
                {
                    ErrorType = ErrorType.ImpossibleOperation,
                    ErrorMessage = "This message is quoted and can not be removed"
                };
                return Task.FromResult(errorResult);
            }

            _unitOfWork.MessagesRepository.Delete(message);
            _unitOfWork.SaveChanges();

            var messageDto = new MessageDto { ThemeId = message.ThemeId, Author = message.User.UserName, Text = message.Text };
            var okResult = new ServiceResult<MessageDto>(messageDto);
            
            return Task.FromResult(okResult);
        }
    }
}