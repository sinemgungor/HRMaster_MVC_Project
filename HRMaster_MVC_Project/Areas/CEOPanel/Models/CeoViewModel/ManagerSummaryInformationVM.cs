using System.ComponentModel.DataAnnotations;

namespace HRMaster_MVC_Project.Models.ViewModels
{
    public class ManagerSummaryInformationVM
    {
        // İsim alanı zorunlu ve maksimum uzunluk sınırı var.
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(50, ErrorMessage = "Name cannot be longer than 50 characters.")]
        public string Name { get; set; }

        // İkinci isim isteğe bağlı, maksimum uzunluk sınırı var.
        [StringLength(50, ErrorMessage = "Second name cannot be longer than 50 characters.")]
        public string SecondName { get; set; }

        // Soyisim alanı zorunlu ve maksimum uzunluk sınırı var.
        [Required(ErrorMessage = "Surname is required.")]
        [StringLength(50, ErrorMessage = "Surname cannot be longer than 50 characters.")]
        public string Surname { get; set; }

        // İkinci soyisim isteğe bağlı, maksimum uzunluk sınırı var.
        [StringLength(50, ErrorMessage = "Second surname cannot be longer than 50 characters.")]
        public string SecondSurname { get; set; }

        // Yöneticinin resmi, geçerli bir resim dosyası türü olmalı.
        [FileExtensions(Extensions = "jpg,jpeg,png,gif", ErrorMessage = "Please upload a valid image file (jpg, jpeg, png, gif).")]
        public IFormFile PicturePath { get; set; }

        // E-posta adresi zorunlu ve geçerli bir formatta olmalı.
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string Email { get; set; }

        // Telefon numarası zorunlu, belirli bir formatta olmalı.
        [Required(ErrorMessage = "Phone number is required.")]
        [Phone(ErrorMessage = "Please enter a valid phone number.")]
        public string PhoneNumber { get; set; }

        // Adres alanı zorunlu ve maksimum uzunluk sınırı var.
        [Required(ErrorMessage = "Address is required.")]
        [StringLength(200, ErrorMessage = "Address cannot be longer than 200 characters.")]
        public string Adress { get; set; }

        // İş alanı zorunlu ve maksimum uzunluk sınırı var.
        [Required(ErrorMessage = "Job is required.")]
        [StringLength(100, ErrorMessage = "Job cannot be longer than 100 characters.")]
        public string Job { get; set; }

        // Departman alanı zorunlu ve maksimum uzunluk sınırı var.
        [Required(ErrorMessage = "Department is required.")]
        [StringLength(100, ErrorMessage = "Department cannot be longer than 100 characters.")]
        public string Department { get; set; }
    }
}
