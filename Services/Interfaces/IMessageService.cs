using System.Threading.Tasks;
using Core.Models;
using Services.DTO;

namespace Services.Interfaces
{
    public interface IMessageService
    {
        Task<MessageDto> CreateNewMessageAsync(int themeId, MessageDto dto, ApplicationUser user);
        Task<MessageDto> QuoteMessageAsync(int themeId, int id, MessageDto dto, ApplicationUser user);
        Task EditMessageAsync(int themeId, int id, MessageDto dto);
        Task<MessageDto> DeleteMessageAsync(int themeId, int id);
    }
}