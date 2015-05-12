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
        // POST api/Message
        [ResponseType(typeof (MessageDto))]
        public async Task<IHttpActionResult> PostMessage(int themeId, MessageBindingModel messageBm)
        {
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            var message = await _messageService.CreateNewMessageAsync(themeId, user, messageBm.MessageText);
            var messageDto = new MessageDto { Text  = message.Text, Author = user.UserName };

            return CreatedAtRoute("DefaultApi", new { controller = "themes", id = themeId }, messageDto);
        }

        // POST api/Message/5 - Quote message
        //[ResponseType(typeof (Message))]
        //public async Task<IHttpActionResult> PostMessage(int id)
        //{
        //    if (!ModelState.IsValid) return BadRequest(ModelState);

        //    // db.Messages.Add(message);
        //    await _db.SaveChangesAsync();
        //    var message = new Message();

        //    return CreatedAtRoute("DefaultApi", new {id = message.Id}, message);
        //}

        // PUT api/Message/5
        //public async Task<IHttpActionResult> PutMessage(int id, Message message)
        //{
        //    if (!ModelState.IsValid) return BadRequest(ModelState);
        //    if (id != message.Id) return BadRequest();

        //    _db.Entry(message).State = EntityState.Modified;

        //    try
        //    {
        //        await _db.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        // if (!MessageExists(id)) return NotFound();
        //        // throw;
        //    }

        //    return StatusCode(HttpStatusCode.NoContent);
        //}

        // DELETE api/Message/5
        //[ResponseType(typeof (Message))]
        //public async Task<IHttpActionResult> DeleteMessage(int id)
        //{
        //    var message = await _db.Messages.FindAsync(id);
        //    if (message == null) return NotFound();

        //    _db.Messages.Remove(message);
        //    await _db.SaveChangesAsync();

        //    return Ok(message);
        //}
        #endregion // Controller actions
    }
}