using System.ComponentModel.DataAnnotations;

namespace s4t.Erp.Cadastros.Infra.Identity.Model
{
    public class AddPhoneNumberViewModel
    {
        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string Number { get; set; }
    }
}