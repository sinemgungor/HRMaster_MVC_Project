using HRMaster_MVC_Project.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace HRMaster_MVC_Project.Areas.EmployeePanel.Models.DTOs
{
    public class AddExpenseRequestDTO
    {
        public ExpenseType ExpenseType { get; set; }

        [Required(ErrorMessage = "Miktar gereklidir.")]
        public decimal Amount { get; set; }
        public Currency Currency { get; set; }

        [Required(ErrorMessage = "Fiş gereklidir.")]
        public string FilePath { get; set; }

        public int EmployeeID { get; set; }
    }
}
