using System.ComponentModel.DataAnnotations;

namespace HRMaster_MVC_Project.Models.ViewModels
{
    public class DepartmentVM
    {
        // ID'nin pozitif bir sayı olması sağlanıyor.
        [Required(ErrorMessage = "ID is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "ID must be a positive integer.")]
        public int ID { get; set; }

        // Departman adının zorunlu olması ve maksimum karakter sınırına uyması sağlanıyor.
        
        public string? DepartmentName { get; set; }

        // CompanyID'nin pozitif bir sayı olması sağlanıyor.
        [Required(ErrorMessage = "Company ID is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Company ID must be a positive integer.")]
        public int CompanyID { get; set; }
    }
}
