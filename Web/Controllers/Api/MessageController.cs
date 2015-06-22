using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Services.Interfaces;
using Services.DTO;

namespace Web.Controllers.Api
{
    [Authorize]
    public class MessageController : ApiController
    {
        private readonly IMessageService _messageService;

        #region Constructor
        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }
        #endregion

        #region Controller actions
        // GET api/themes/5/message/5
        public async Task<IHttpActionResult> GetMessage(int themeId, int id)
        {
            return Ok(await _messageService.GetMessageByIdAsync(themeId, id));
        }
        
        // POST api/themes/5/message
        public async Task<IHttpActionResult> PostMessage(int themeId, MessageDto requestDto)
        {
            var responseDto = await _messageService.CreateNewMessageAsync(themeId, requestDto, User.Identity.GetUserId());
            return CreatedAtRoute("MessagesApi", new { themeId, controller = ControllerContext.ControllerDescriptor.ControllerName, id = responseDto.Id }, responseDto);
        }

        // POST api/themes/5/message/5 - Quote message
        public async Task<IHttpActionResult> PostMessage(int themeId, int id, MessageDto requestDto)
        {
            var responseDto = await _messageService.QuoteMessageAsync(themeId, id, requestDto, User.Identity.GetUserId());
            return CreatedAtRoute("MessagesApi", new { themeId, controller = ControllerContext.ControllerDescriptor.ControllerName, id = responseDto.Id }, responseDto);
        }

        // PUT api/themes/5/message/5
        public async Task<IHttpActionResult> PutMessage(int themeId, int id, MessageDto requestDto)
        {
            await _messageService.EditMessageAsync(themeId, id, requestDto, User.Identity.GetUserId());
            return StatusCode(HttpStatusCode.NoContent);
        }

        // DELETE api/themes/5/message/5
        public async Task<IHttpActionResult> DeleteMessage(int themeId, int id)
        {
            return Ok(await _messageService.DeleteMessageAsync(themeId, id, User.Identity.GetUserId()));
        }
        #endregion // Controller actions
    }
}