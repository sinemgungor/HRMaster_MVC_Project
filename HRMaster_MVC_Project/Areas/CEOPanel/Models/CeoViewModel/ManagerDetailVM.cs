using HRMaster_MVC_Project.Models.Enums;

namespace HRMaster_MVC_Project.Areas.CEOPanel.Models
{
    public class ManagerDetailVM
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? SecondName { get; set; }
        public string? Surname { get; set; }
        public string? SecondSurname { get; set; }
        public string? Job { get; set; }
        public string? Title { get; set; }
        public string? IdentityNumber { get; set; }
        public string? PicturePath { get; set; }
        public DateOnly? BirthDate { get; set; }
        public string? BirthState { get; set; }
        public DateOnly? HireDate { get; set; }
        public DateOnly? TerminationDate { get; set; }

        public string? Email { get; set; }
        public string? Address { get; set; }
        public double? Salary { get; set; }
        public string? PhoneNumber { get; set; }
        public Gender? Gender { get; set; }
        public MarialStatus? MarialStatus { get; set; }
        public BloodGroup? BloodGroup { get; set; }
    }
}
