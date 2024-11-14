using HRMaster_MVC_Project.Areas.EmployeePanel.Models.DTOs;

namespace HRMaster_MVC_Project.Areas.CEOPanel.Models
{
    public class ShowEmployeeDTO
    {
        public int ID { get; set; }

        public string? Name { get; set; }

        public string? Surname { get; set; }

        public string? PicturePath { get; set; }

        public string? Title { get; set; }
        public DepartmentDTO? Department { get; set; }
    }
}
