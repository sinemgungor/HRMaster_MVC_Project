using System.ComponentModel.DataAnnotations;

namespace HRMaster_MVC_Project.Areas.CEOPanel.Models.CeoDTOs
{
    public class AddDepartmentDTO
    {
       
        [Required(ErrorMessage = "Departman adı gerekli.")]
        public string DepartmentName { get; set; }
    }
}
