using System.Threading.Tasks;
using Core.Models;
using Services.DTO;
using Services.Results;

namespace Services.Interfaces
{
    public interface IMessageService
    {
        Task<MessageDto> CreateNewMessageAsync(int themeId, ApplicationUser user, string messageText);
        Task<MessageDto> QuoteMessageAsync(int id, ApplicationUser user, string messageText);
        Task<ServiceResult<MessageDto>> EditMessageAsync(int id, string messageText);
        Task<ServiceResult<MessageDto>> DeleteMessageAsync(int id);
    }
}