using System.ComponentModel.DataAnnotations;

namespace HRMaster_MVC_Project.Models.ViewModels
{
    public class ResetPasswordVM
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Token { get; set; }

        [Required]
        [PasswordComplexity(ErrorMessage = "Şifre en az 6 karakter uzunluğunda olmalı, en az bir büyük harf, bir küçük harf, bir rakam ve bir özel karakter içermelidir.")]
        public string NewPassword { get; set; }

        [Required]
        [Compare("NewPassword",ErrorMessage ="Şifreniz ile tekrar şifreniz eşleşmemektedir.")]
        public string ConfirmNewPassword { get; set; }
    }
}
