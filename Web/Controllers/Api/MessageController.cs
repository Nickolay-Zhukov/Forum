using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Services.Interfaces;
using Services.DTO;
using Web.Core;

namespace Web.Controllers.Api
{
    [Authorize]
    public class MessageController : ApiController
    {
        private readonly IMessageService _messageService;
        private ApplicationUserManager UserManager
        {
            get { return Request.GetOwinContext().GetUserManager<ApplicationUserManager>(); }
        }

        #region Constructor
        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }
        #endregion

        #region Controller actions
        // POST api/themes/5/message
        [ResponseType(typeof (MessageDto))]
        public async Task<IHttpActionResult> PostMessage(int themeId, MessageDto requestDto)
        {
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            var responseDto = await _messageService.CreateNewMessageAsync(themeId, requestDto, user);
            return CreatedAtRoute("MessagesApi", new { themeId , id = responseDto.Id }, responseDto);
        }

        // POST api/themes/5/message/5 - Quote message
        [ResponseType(typeof(MessageDto))]
        public async Task<IHttpActionResult> PostMessage(int themeId, int id, MessageDto requestDto)
        {
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            var responseDto = await _messageService.QuoteMessageAsync(themeId, id, requestDto, user);
            return CreatedAtRoute("MessagesApi", new { themeId, id = responseDto.Id }, responseDto);
        }

        // PUT api/themes/5/message/5
        public async Task<IHttpActionResult> PutMessage(int themeId, int id, MessageDto requestDto)
        {
            await _messageService.EditMessageAsync(themeId, id, requestDto);
            return StatusCode(HttpStatusCode.NoContent);
        }

        // DELETE api/themes/5/message/5
        [ResponseType(typeof(MessageDto))]
        public async Task<IHttpActionResult> DeleteMessage(int themeId, int id)
        {
            var responseDto = await _messageService.DeleteMessageAsync(themeId, id);
            return Ok(responseDto);
        }
        #endregion // Controller actions
    }
}