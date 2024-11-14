using HRMaster_MVC_Project.Models.Enums;

namespace HRMaster_MVC_Project.Areas.EmployeePanel.Models.ViewModels
{
    public class AddExpenseRequestVM
    {
        public ExpenseType ExpenseType { get; set; }
        public decimal Amount { get; set; }
        public Currency Currency { get; set; }
        public IFormFile FilePath { get; set; }

        public int EmployeeID { get; set; }
    }
}
