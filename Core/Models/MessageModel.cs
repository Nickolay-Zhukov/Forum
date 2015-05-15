using System;
using System.Collections.Generic;

namespace Core.Models
{
    public class Message
    {
        public int Id { get; set; }
        public int ThemeId { get; set; }
        public string UserId { get; set; }
        public int? QuoteId { get; set; }
        public string Text { get; set; }
        public DateTime DateTime { get; set; }

        // Navigation properties
        public virtual Theme Theme { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual Message Quote { get; set; }
        public virtual ICollection<Message> Quotes { get; set; }

        public Message()
        {
            DateTime = DateTime.Now;
        }
    }
}