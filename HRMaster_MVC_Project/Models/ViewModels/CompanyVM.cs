using System.ComponentModel.DataAnnotations;
using System.Data;
using HRMaster_MVC_Project.Models.Enums;
namespace HRMaster_MVC_Project.Models.ViewModels
{
    public class CompanyVM
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Şirket Adı gereklidir.")]
        [StringLength(100, ErrorMessage = "Şirket Adı 100 karakterden uzun olmamalıdır.")]
        public string CompanyName { get; set; }

        public string? CompanyTitle { get; set; }

        [Required(ErrorMessage = "Şirket Logosu Ekleyiniz.")]
        public IFormFile LogoPath { get; set; }

        [Required(ErrorMessage = "Mersis Numarası gereklidir.")]
        [StringLength(20, ErrorMessage = "Mersis Numarası 20 karakterden uzun olmamalıdır.")]
        public string MersisNumber { get; set; }

        [Required(ErrorMessage = "Vergi Dairesi Adı gereklidir.")]
        [StringLength(100, ErrorMessage = "Vergi Dairesi Adı 100 karakterden uzun olmamalıdır.")]
        public string TaxOfficeName { get; set; }

        [Required(ErrorMessage = "Vergi Kimlik Numarası gereklidir.")]
        [StringLength(20, ErrorMessage = "Vergi Kimlik Numarası 20 karakterden uzun olmamalıdır.")]
        public string TaxIdentificaitonNumber { get; set; }

        [Required(ErrorMessage = "Şirket Adresi gereklidir.")]
        [StringLength(255, ErrorMessage = "Şirket Adresi 255 karakterden uzun olmamalıdır.")]
        public string CompanyAddress { get; set; }

        [Required(ErrorMessage = "Şirket Telefon Numarası gereklidir.")]
        [Phone(ErrorMessage = "Geçersiz telefon numarası formatı.")]
        public string CompanyPhoneNumber { get; set; }

        [Required(ErrorMessage = "Şirket E-posta Adresi gereklidir.")]
        [EmailAddress(ErrorMessage = "Geçersiz e-posta adresi formatı.")]
        public string CompanyEmail { get; set; }


        public string? CompanyWebsite { get; set; }

        [Required(ErrorMessage = "Şirket Türü gereklidir.")]
        public CompanyType CompanyType { get; set; }

        [Required(ErrorMessage = "Şirket Sektörü gereklidir.")]
        public CompanyField CompanyField { get; set; }

        [Required(ErrorMessage = "Kuruluş Tarihi gereklidir.")]
        [DataType(DataType.Date, ErrorMessage = "Geçersiz tarih formatı.")]
        [DateNotInFuture(ErrorMessage = "Kuruluş Tarihi bugünden ileri bir tarih olmamalıdır.")]
        public DateOnly EstablishedDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);


        public DateOnly? ContractStartingDate { get; set; } 

        [EndDateAfterStartDate(ErrorMessage = "Sözleşme Bitiş Tarihi, Sözleşme Başlangıç Tarihi'nden önce olamaz.")]
        public DateOnly? ContractEndDate { get; set; }

        [Required(ErrorMessage = "Şirket Bilgisi Gereklidir.")]
        [StringLength(1000, ErrorMessage = "Şirket Bilgisi 1000 karakteri geçmemelidir.")]
        public string CompanyInformation { get; set; }
    }

    public class DateNotInFutureAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is DateOnly date)
            {
                var today = DateOnly.FromDateTime(DateTime.Now);
                // Tarih bugünden ileri olmamalıdır
                return date <= today;
            }
            return false;
        }
    }

    public class EndDateAfterStartDateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var companyVM = (CompanyVM)validationContext.ObjectInstance;
            var startDate = companyVM.ContractStartingDate;

            // Eğer endDate null ise, geçerli bir sonuç döndürüyoruz
            if (value == null)
            {
                return ValidationResult.Success;
            }

            var endDate = (DateOnly)value;

            if (startDate.HasValue && endDate < startDate.Value)
            {
                return new ValidationResult("Sözleşme Bitiş Tarihi, Sözleşme Başlangıç Tarihi'nden önce olamaz.");
            }

            return ValidationResult.Success;
        }
    }

}
