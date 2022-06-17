using System.ComponentModel.DataAnnotations;

namespace MyBudget.ViewModels.Account
{
    public class RegsiterVM
    {
        [Required]
        [Display(Name = "Имя")]
        public string? UserName { get; set; }

        [Required]
        [Display(Name = "Пароль")]
        public string? Password { get; set; }

        [Required]
        [Display(Name ="E-Mail аддресс")]
        public string? Email { get; set; }
    }
}
