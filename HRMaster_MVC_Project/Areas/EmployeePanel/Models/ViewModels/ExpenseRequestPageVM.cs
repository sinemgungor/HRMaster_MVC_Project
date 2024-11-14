using HRMaster_MVC_Project.Areas.EmployeePanel.Models.DTOs;

namespace HRMaster_MVC_Project.Areas.EmployeePanel.Models.ViewModels
{
    public class ExpenseRequestPageVM
    {
        public AddExpenseRequestVM? AddExpenseRequestVM { get; set; }
        public List<ShowExpenseRequestDTO>? ExpenseRequests { get; set; }
    }
}
