using System;
using System.Collections.Generic;

namespace Core.Models
{
    public class Theme
    {
        public int Id { get; set; }
        public string OwnerId { get; set; }

        public string Title { get; set; }
        public virtual ApplicationUser Owner { get; set; }
        public DateTime CreationDateTime { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
    }
}