using HRMaster_MVC_Project.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace HRMaster_MVC_Project.Areas.EmployeePanel.Models.DTOs
{
    public class AddAdvanceRequestDTO
    {
        [Required(ErrorMessage = "Miktar gereklidir.")]
        public decimal Amount { get; set; }
        public Currency Currency { get; set; }
        [Required(ErrorMessage = "Açıklama gereklidir.")]
        public string Description { get; set; }
        public AdvanceType AdvanceType { get; set; }

        public int EmployeeID { get; set; }
    }
}
