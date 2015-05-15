﻿using System;
using System.ComponentModel.DataAnnotations;
using Core.Models;

namespace Services.DTO
{
    public class MessageDto
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public string Author { get; set; }

        [Required(ErrorMessage = "Message text is required")]
        [StringLength(5000, MinimumLength = 1, ErrorMessage = "The {0} must be at least {2} characters long.")]
        public string Text { get; set; }

        public string QuoteAuthor { get; set; }
        public string Quote { get; set; }

        #region Constructor
        public MessageDto() { }
        public MessageDto(Message message)
        {
            Id = message.Id;
            DateTime = message.DateTime;
            Author = message.User.UserName;
            Text = message.Text;
            QuoteAuthor = message.Quote.User.UserName;
            Quote = message.Quote.Text;
        }
        #endregion
    }
}