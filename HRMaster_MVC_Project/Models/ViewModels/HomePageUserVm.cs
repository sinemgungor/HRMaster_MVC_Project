using System.ComponentModel.DataAnnotations;

namespace HRMaster_MVC_Project.Models.ViewModels
{
    public class HomePageUserVm
    {
        // İsim alanının zorunlu olması ve maksimum uzunluk sınırı.
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(50, ErrorMessage = "Name cannot be longer than 50 characters.")]
        public string Name { get; set; }

        // İkinci isim isteğe bağlıdır, ancak bir uzunluk sınırı vardır.
        [StringLength(50, ErrorMessage = "Second name cannot be longer than 50 characters.")]
        public string SecondName { get; set; }

        // Soyisim alanının zorunlu olması ve maksimum uzunluk sınırı.
        [Required(ErrorMessage = "Surname is required.")]
        [StringLength(50, ErrorMessage = "Surname cannot be longer than 50 characters.")]
        public string Surname { get; set; }

        // İkinci soyisim isteğe bağlıdır, ancak bir uzunluk sınırı vardır.
        [StringLength(50, ErrorMessage = "Second surname cannot be longer than 50 characters.")]
        public string SecondSurname { get; set; }

        // Şirket adının zorunlu olması ve maksimum uzunluk sınırı.
        [Required(ErrorMessage = "Company name is required.")]
        [StringLength(100, ErrorMessage = "Company name cannot be longer than 100 characters.")]
        public string CompanyName { get; set; }

        // Şirket logosunun isteğe bağlı olması, ancak bir dosya türü ve boyut sınırı olabilir.
        [FileExtensions(Extensions = "jpg,jpeg,png,gif", ErrorMessage = "Please upload a valid image file (jpg, jpeg, png, gif).")]
        public IFormFile CompanyLogo { get; set; }
    }
}
