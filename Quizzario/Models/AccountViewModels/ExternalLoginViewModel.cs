using System.ComponentModel.DataAnnotations;

namespace Quizzario.Models.AccountViewModels
{
    public class ExternalLoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
