using System.Threading.Tasks;
using Core.Models;
using Services.DTO;

namespace Services.Interfaces
{
    public interface IMessageService
    {
        Task<MessageDto> CreateNewMessageAsync(int themeId, ApplicationUser user, string messageText);
        Task<MessageDto> QuoteMessageAsync(int id, ApplicationUser user, string messageText);
        Task EditMessageAsync(int id, string messageText);
        Task<MessageDto> DeleteMessageAsync(int id);
    }
}