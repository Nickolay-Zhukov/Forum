using System.Threading.Tasks;
using Core.Models;

namespace Services.Interfaces
{
    public interface IMessageService
    {
        Task<Message> CreateNewMessageAsync(int themeId, ApplicationUser user, string messageText);
        Task<Message> EditMessageAsync(int id);
        Task<Message> DeleteMessageAsync(int id);
        Task<Message> QuoteMessageAsync(int id, string messageText);
    }
}