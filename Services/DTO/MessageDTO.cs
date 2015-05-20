using System;
using System.ComponentModel.DataAnnotations;
using Core.Models;

namespace Services.DTO
{
    public class MessageDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Message text is required")]
        [StringLength(5000, MinimumLength = 1, ErrorMessage = "The {0} must be at least {2} characters long")]
        public string Text { get; set; }
        public string Author { get; set; }
        public DateTime CreationDateTime { get; set; }
        public string Quote { get; set; }
        public string QuoteAuthor { get; set; }

        #region Constructor
        public MessageDto() { }
        public MessageDto(Message message)
        {
            Id = message.Id;
            Text = message.Text;
            if (message.User != null) Author = message.User.UserName;
            CreationDateTime = message.CreationDateTime;
            
            if (message.Quote == null) return;
            Quote = message.Quote.Text;
            QuoteAuthor = message.Quote.User.UserName;
        }
        #endregion
    }
}