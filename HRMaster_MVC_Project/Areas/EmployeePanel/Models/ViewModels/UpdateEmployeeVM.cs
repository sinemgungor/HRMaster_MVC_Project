using System.ComponentModel.DataAnnotations;

namespace HRMaster_MVC_Project.Areas.EmployeePanel.Models.ViewModels
{
    public class UpdateEmployeeVM
    {
        public int? ID { get; set; }
        public string? PicturePath { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public IFormFile? UpdatedPicture { get; set; }

        [Phone(ErrorMessage = "Geçerli bir telefon numarası giriniz.")]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "Telefon numarası yalnızca 11 hane ve rakamlardan oluşmalıdır.")]
        public string? UpdatedPhone { get; set; }

        [StringLength(250, ErrorMessage = "Adres 250 karakterden uzun olamaz.")]
        public string? UpdatedAddress { get; set; }
    }
}
