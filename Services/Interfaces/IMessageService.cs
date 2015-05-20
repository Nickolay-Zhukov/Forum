using System.Threading.Tasks;
using Services.DTO;

namespace Services.Interfaces
{
    public interface IMessageService
    {
        Task<MessageDto> CreateNewMessageAsync(int themeId, MessageDto dto, string userId);
        Task<MessageDto> QuoteMessageAsync(int themeId, int id, MessageDto dto, string userId);
        Task EditMessageAsync(int themeId, int id, MessageDto dto, string userId);
        Task<MessageDto> DeleteMessageAsync(int themeId, int id, string userId);
    }
}