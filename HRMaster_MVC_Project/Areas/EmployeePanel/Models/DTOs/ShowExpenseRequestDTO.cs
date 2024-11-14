using HRMaster_MVC_Project.Models.Enums;

namespace HRMaster_MVC_Project.Areas.EmployeePanel.Models.DTOs
{
    public class ShowExpenseRequestDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime RequestDate { get; set; }
        public decimal Amount { get; set; }
        public Currency Currency { get; set; }
        public ApprovalStatus ApprovalStatus { get; set; }
        public DateTime? ResponseDate { get; set; }
        public ExpenseType ExpenseType { get; set; }
        public string FilePath { get; set; }
    }
}
