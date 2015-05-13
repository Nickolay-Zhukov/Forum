using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Services.Interfaces;
using Services.DTO;
using Web.ControllersBindingModels;
using Web.Core;
using Web.Filters;

namespace Web.Controllers.Api
{
    [ValidateModel]
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
        public async Task<IHttpActionResult> PostMessage(int themeId, MessageBindingModel messageBm)
        {
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            var messageDto = await _messageService.CreateNewMessageAsync(themeId, user, messageBm.MessageText);

            
            
            return CreatedAtRoute("DefaultApi", new { controller = "themes", id = themeId }, messageDto);
        }

        // POST api/themes/5/message/5 - Quote message
        [ResponseType(typeof(MessageDto))]
        public async Task<IHttpActionResult> QuoteMessage(int id, MessageBindingModel messageBm)
        {
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            var messageDto = await _messageService.QuoteMessageAsync(id, user, messageBm.MessageText);
            return CreatedAtRoute("DefaultApi", new { controller = "themes", id = messageDto.ThemeId }, messageDto);
        }

        // PUT api/themes/5/message/5
        public async Task<IHttpActionResult> PutMessage(int id, MessageBindingModel messageBm)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _messageService.EditMessageAsync(id, messageBm.MessageText);
            return StatusCode(HttpStatusCode.NoContent);
        }

        // DELETE api/themes/5/message/5
        [ResponseType(typeof(MessageDto))]
        public async Task<IHttpActionResult> DeleteMessage(int id)
        {
            var messageDto = await _messageService.DeleteMessageAsync(id);
            if (messageDto == null) return NotFound();
            return Ok(messageDto);
        }
        #endregion // Controller actions
    }
}