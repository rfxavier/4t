using System.ComponentModel.DataAnnotations;

namespace s4t.Erp.Cadastros.Infra.Identity.Model
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
