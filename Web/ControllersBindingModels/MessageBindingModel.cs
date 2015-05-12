using System.ComponentModel.DataAnnotations;

namespace Web.ControllersBindingModels
{
    public class MessageBindingModel
    {
        [Required(ErrorMessage = "Message text is required")]
        [StringLength(5000, MinimumLength = 1, ErrorMessage = "The {0} must be at least {2} characters long.")]
        public string MessageText { get; set; }
    }
}