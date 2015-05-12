using System;
using System.Threading.Tasks;
using Core.Models;
using Services.Interfaces;

namespace Services.Implementations
{
    class MessageService : IMessageService
    {
        public Task<Message> CreateNewMessageAsync(int themeId, ApplicationUser user, string messageText)
        {
            throw new NotImplementedException();
        }

        public Task<Message> EditMessageAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Message> DeleteMessageAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Message> QuoteMessageAsync(int id, string messageText)
        {
            throw new NotImplementedException();
        }
    }
}