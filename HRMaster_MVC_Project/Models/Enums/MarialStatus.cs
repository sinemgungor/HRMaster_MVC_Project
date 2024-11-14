using System.ComponentModel.DataAnnotations;

namespace HRMaster_MVC_Project.Models.Enums
{
    public enum MarialStatus
    {
        [Display(Name = "Bekar")]
        Single = 1,
        [Display(Name = "Evli")]
        Married
    }
}
