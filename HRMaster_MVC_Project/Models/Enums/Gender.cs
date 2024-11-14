using System.ComponentModel.DataAnnotations;

namespace HRMaster_MVC_Project.Models.Enums
{
    public enum Gender
    {
        [Display(Name = "Kadın")]
        Female = 1,
        [Display(Name = "Erkek")]
        Male = 2,
        [Display(Name = "Diğer")]
        Other
    }
}
