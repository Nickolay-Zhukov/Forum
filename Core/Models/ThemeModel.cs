﻿using System;
using System.Collections.Generic;

namespace Core.Models
{
    public class Theme
    {
        public int Id { get; set; }
        public string OwnerId { get; set; }
        public string Title { get; set; }
        public DateTime DateTime { get; set; }

        // Navigation properties
        public virtual ApplicationUser Owner { get; set; }
        public virtual ICollection<Message> Messages { get; set; }

        public Theme()
        {
            DateTime = DateTime.Now;
        }
    }
}