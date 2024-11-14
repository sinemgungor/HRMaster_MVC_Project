using HRMaster_MVC_Project.Models.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace HRMaster_MVC_Project.Areas.AdminPanel.Models.AdminViewModels
{
    public class AddCEOVM
    {
        [Required(ErrorMessage = "Ad zorunludur.")]
        [StringLength(50, ErrorMessage = "Ad 50 karakterden uzun olamaz.")]
        public string Name { get; set; }

        [StringLength(50, ErrorMessage = "İkinci ad 50 karakterden uzun olamaz.")]
        public string? SecondName { get; set; }

        [Required(ErrorMessage = "Soyad zorunludur.")]
        [StringLength(50, ErrorMessage = "Soyad 50 karakterden uzun olamaz.")]
        public string Surname { get; set; }

        [StringLength(50, ErrorMessage = "İkinci soyad 50 karakterden uzun olamaz.")]
        public string? SecondSurname { get; set; }

        //[FileExtensions(Extensions = "jpg,jpeg,png,gif", ErrorMessage = "Lütfen uygun dosya tipi seçiniz. (jpg, jpeg, png, gif).")]
        [Required(ErrorMessage = "Lütfen profil fotoğrafı yükleyiniz")]
        public IFormFile PicturePath { get; set; }

        [DateNotMoreHundredYears(ErrorMessage = "Doğum tarihi 100 yıl öncesini geçmemelidir.")]
        [Required(ErrorMessage = "Doğum tarihi zorunludur.")]
        public DateOnly BirthDate { get; set; } 

        [Required(ErrorMessage = "Doğum yeri zorunludur.")]
        [StringLength(100, ErrorMessage = "Doğum yeri 100 karakterden uzun olamaz.")]
        public string BirthState { get; set; }

        [Required(ErrorMessage = "Kimlik numarası zorunludur.")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "Kimlik numarası 11 karakter olmalıdır.")]
        public string IdentityNumber { get; set; }

        [Required(ErrorMessage = "İşe başlama tarihi zorunludur.")]
        [DateNotMoreHundredYears(ErrorMessage = "İşe başlama tarihi 100 yıl öncesini geçmemelidir.")]
        public DateOnly HireDate { get; set; } 

        [Required(ErrorMessage = "Meslek zorunludur.")]
        [StringLength(100, ErrorMessage = "Meslek 100 karakterden uzun olamaz.")]
        public string Job { get; set; }

        [Required(ErrorMessage = "Adres zorunludur.")]
        [StringLength(250, ErrorMessage = "Adres 250 karakterden uzun olamaz.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Maaş zorunludur.")]
        [Range(0, double.MaxValue, ErrorMessage = "Maaş sıfır veya daha büyük bir değer olmalıdır.")]
        public double Salary { get; set; }

        [Required(ErrorMessage = "Ünvan zorunludur.")]
        [StringLength(100, ErrorMessage = "Ünvan 100 karakterden uzun olamaz.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Kan grubu zorunludur.")]
        public BloodGroup BloodGroup { get; set; }

        [Required(ErrorMessage = "Cinsiyet zorunludur.")]
        public Gender Gender { get; set; }

        [Required(ErrorMessage = "Medeni hal zorunludur.")]
        public MarialStatus MarialStatus { get; set; }

        public SelectList? Companies { get; set; }

        [Required(ErrorMessage = "E-posta zorunludur.")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Telefon numarası zorunludur.")]
        [Phone(ErrorMessage = "Geçerli bir telefon numarası giriniz.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Şirket seçimi zorunludur.")]
        public int SelectedCompanyID { get; set; }
    }

    public class DateNotInFutureAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is DateOnly date)
            {
                var today = DateOnly.FromDateTime(DateTime.Now);

                return date <= today;
            }
            return false;
        }
    }

    public class DateNotMoreHundredYearsAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is DateOnly date)
            {
                var today = DateOnly.FromDateTime(DateTime.Now);
                var hundredYearsAgo = today.AddYears(-100);

                // Tarih, bugünden daha eski ve 100 yıl öncesini geçmemelidir.
                return date >= hundredYearsAgo;
            }
            return false;
        }
    }
}


