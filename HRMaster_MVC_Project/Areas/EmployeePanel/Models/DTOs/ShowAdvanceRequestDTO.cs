using HRMaster_MVC_Project.Models.Enums;

namespace HRMaster_MVC_Project.Areas.EmployeePanel.Models.DTOs
{
    public class ShowAdvanceRequestDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime RequestDate { get; set; }
        public ApprovalStatus ApprovalStatus { get; set; }
        public DateTime? ResponseDate { get; set; }
        public decimal Amount { get; set; }
        public Currency Currency { get; set; }
        public string Description { get; set; }
        public AdvanceType AdvanceType { get; set; }
    }
}
