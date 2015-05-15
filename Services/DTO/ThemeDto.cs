using System;
using System.ComponentModel.DataAnnotations;
using Core.Models;

namespace Services.DTO
{
    public class ThemeDto
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public string Author { get; set; }

        [Required(ErrorMessage = "Theme title field is required")]
        [StringLength(255, MinimumLength = 1, ErrorMessage = "The {0} must be at least {2} characters long.")]
        [RegularExpression("^[\\w ]+$", ErrorMessage = "Invalid theme title")]
        public string Title { get; set; }


        #region Constructor
        public ThemeDto() { }
        public ThemeDto(Theme theme)
        {
            Id = theme.Id;
            DateTime = theme.DateTime;
            Author = theme.Owner.UserName;
            Title = theme.Title;
        }
        #endregion
    }
}