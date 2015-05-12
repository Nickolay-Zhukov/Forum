using System.ComponentModel.DataAnnotations;

namespace Web.ControllersBindingModels
{
    public class ThemeBindingModel
    {
        [Required(ErrorMessage = "Theme title field is required")]
        [StringLength(255, MinimumLength = 1, ErrorMessage = "The {0} must be at least {2} characters long.")]
        [RegularExpression("^[\\w ]+$", ErrorMessage = "Invalid theme title")]
        public string ThemeTitle { get; set; }
    }
}