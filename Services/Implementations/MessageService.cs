using System;
using System.Threading.Tasks;
using Core.Models;
using Services.DTO;
using Services.Interfaces;

namespace Services.Implementations
{
    class MessageService : IMessageService
    {
        public Task<MessageDto> CreateNewMessageAsync(int themeId, ApplicationUser user, string messageText)
        {
            // var messageDto = new MessageDto { Text = message.Text, Author = user.UserName };
            throw new NotImplementedException();
        }

        public Task<MessageDto> QuoteMessageAsync(int id, ApplicationUser user, string messageText)
        {
            throw new NotImplementedException();
        }

        public Task EditMessageAsync(int id, string messageText)
        {
            //try
            //{
            //    await _db.SaveChangesAsync();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!MessageExists(id)) return NotFound();
            //    throw;
            //}
            
            throw new NotImplementedException();
        }

        public Task<MessageDto> DeleteMessageAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}