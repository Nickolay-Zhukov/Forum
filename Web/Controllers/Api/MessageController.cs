using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Services.Interfaces;
using Services.DTO;
using Services.Results;
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

        private IHttpActionResult ConvertFrom(ServiceResult<MessageDto> serviceResult)
        {
            switch (serviceResult.ErrorType)
            {
                case ErrorType.Ok: return Ok(serviceResult.DtoEntity);
                case ErrorType.OkNoContent: return StatusCode(HttpStatusCode.NoContent);
                case ErrorType.EntityNotFound: return NotFound();
                case ErrorType.ImpossibleOperation: return BadRequest(serviceResult.ErrorMessage);
            }
            return BadRequest();
        }

        #region Controller actions
        // POST api/themes/5/message
        [ResponseType(typeof (MessageDto))]
        public async Task<IHttpActionResult> PostMessage(int themeId, MessageBindingModel model)
        {
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            var messageDto = await _messageService.CreateNewMessageAsync(themeId, user, model.MessageText);
            if (messageDto == null) BadRequest("Specified theme doesn't exist");
            return CreatedAtRoute("DefaultApi", new { controller = "themes", id = themeId }, messageDto);
        }

        // POST api/themes/5/message/5 - Quote message
        [ResponseType(typeof(MessageDto))]
        public async Task<IHttpActionResult> QuoteMessage(int id, MessageBindingModel model)
        {
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            var messageDto = await _messageService.QuoteMessageAsync(id, user, model.MessageText);
            if (messageDto == null) return NotFound();
            return CreatedAtRoute("DefaultApi", new { controller = "themes", id = messageDto.ThemeId }, messageDto);
        }

        // PUT api/themes/5/message/5
        public async Task<IHttpActionResult> PutMessage(int id, MessageBindingModel model)
        {
            var operationResult = await _messageService.EditMessageAsync(id, model.MessageText);
            return ConvertFrom(operationResult);
        }

        // DELETE api/themes/5/message/5
        [ResponseType(typeof(MessageDto))]
        public async Task<IHttpActionResult> DeleteMessage(int id)
        {
            var operationResult = await _messageService.DeleteMessageAsync(id);
            return ConvertFrom(operationResult);
        }
        #endregion // Controller actions
    }
}