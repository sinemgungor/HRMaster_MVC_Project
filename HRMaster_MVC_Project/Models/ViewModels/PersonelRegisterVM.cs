using HRMaster_MVC_Project.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace HRMaster_MVC_Project.Models.ViewModels
{
    public class PersonelRegisterVM
    {
        // İsim alanı zorunlu ve maksimum uzunluk sınırı var.
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(50, ErrorMessage = "Name cannot be longer than 50 characters.")]
        public string Name { get; set; }

        // İkinci isim isteğe bağlı, maksimum uzunluk sınırı var.
        [StringLength(50, ErrorMessage = "Second name cannot be longer than 50 characters.")]
        public string? SecondName { get; set; }

        // Soyisim alanı zorunlu ve maksimum uzunluk sınırı var.
        [Required(ErrorMessage = "Surname is required.")]
        [StringLength(50, ErrorMessage = "Surname cannot be longer than 50 characters.")]
        public string Surname { get; set; }

        // İkinci soyisim isteğe bağlı, maksimum uzunluk sınırı var.
        [StringLength(50, ErrorMessage = "Second surname cannot be longer than 50 characters.")]
        public string? SecondSurname { get; set; }

        // Personelin resmi, geçerli bir resim dosyası türü olmalı.
        //[FileExtensions(Extensions = "jpg,jpeg,png,gif", ErrorMessage = "Please upload a valid image file (jpg, jpeg, png, gif).")]
        [Required(ErrorMessage = "Lütfen profil resmi ekleyiniz!")]
        public IFormFile PicturePath { get; set; }

        // Doğum tarihi zorunlu.
        [Required(ErrorMessage = "Birth date is required.")]
        public DateOnly BirthDate { get; set; }

        // Doğum yeri zorunlu ve maksimum uzunluk sınırı var.
        [Required(ErrorMessage = "Birth state is required.")]
        [StringLength(100, ErrorMessage = "Birth state cannot be longer than 100 characters.")]
        public string BirthState { get; set; }

        // Kimlik numarası zorunlu, belirli bir uzunlukta olmalı.
        [Required(ErrorMessage = "Identity number is required.")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "Identity number must be exactly 11 characters.")]
        public string IdentityNumber { get; set; }

        // İşe başlama tarihi zorunlu.
        [Required(ErrorMessage = "Hire date is required.")]
        public DateOnly HireDate { get; set; }

        // İş alanı zorunlu ve maksimum uzunluk sınırı var.
        [Required(ErrorMessage = "Job is required.")]
        [StringLength(100, ErrorMessage = "Job cannot be longer than 100 characters.")]
        public string Job { get; set; }

        // Adres zorunlu ve maksimum uzunluk sınırı var.
        [Required(ErrorMessage = "Address is required.")]
        [StringLength(200, ErrorMessage = "Address cannot be longer than 200 characters.")]
        public string Address { get; set; }

        // Maaş zorunlu ve pozitif bir değer olmalı.
        [Required(ErrorMessage = "Salary is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Salary must be a positive number.")]
        public double Salary { get; set; }

        // Unvan zorunlu ve maksimum uzunluk sınırı var.
        [Required(ErrorMessage = "Title is required.")]
        [StringLength(50, ErrorMessage = "Title cannot be longer than 50 characters.")]
        public string Title { get; set; }

        // Kan grubu zorunlu.
        [Required(ErrorMessage = "Blood group is required.")]
        public BloodGroup BloodGroup { get; set; }

        // Cinsiyet zorunlu.
        [Required(ErrorMessage = "Gender is required.")]
        public Gender Gender { get; set; }

        // Medeni durum zorunlu.
        [Required(ErrorMessage = "Marital status is required.")]
        public MarialStatus MarialStatus { get; set; }

        // Şirket ID'si zorunlu ve pozitif bir değer olmalı.

        public int? CompanyID { get; set; }

        // E-posta adresi zorunlu ve geçerli bir formatta olmalı.
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string Email { get; set; }

        // Telefon numarası zorunlu, belirli bir formatta olmalı.
        [Required(ErrorMessage = "Phone number is required.")]
        [Phone(ErrorMessage = "Please enter a valid phone number.")]
        public string PhoneNumber { get; set; }
    }
}
