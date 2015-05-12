using System.ComponentModel.DataAnnotations;

namespace Web.BindingModels
{
    public class ThemeBindingModel
    {
        [Required]
        [StringLength(255, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 1)]
        [RegularExpression("^[\\w ]+$", ErrorMessage = "Invalid theme title")]
        public string ThemeTitle { get; set; }
    }
}