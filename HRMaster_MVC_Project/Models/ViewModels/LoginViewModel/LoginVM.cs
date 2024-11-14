using System.ComponentModel.DataAnnotations;

namespace HRMaster_MVC_Project.Models.ViewModels.LoginViewModel
{
    public class LoginVM
    {
        [Required(ErrorMessage = "Email alanı gereklidir.")]
        [EmailAddress(ErrorMessage = "Geçerli bir Email adresi giriniz.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Şifre alanı gereklidir.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
