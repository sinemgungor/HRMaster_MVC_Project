using System.ComponentModel.DataAnnotations;

namespace HRMaster_MVC_Project.Models.Enums
{
    public enum CompanyType
    {
        [Display(Name = "Limited Şirket")]
        LimitedCompany = 1,

        [Display(Name = "Anonim Şirket")]
        JointStockCompany,

        [Display(Name = "Şahıs Şirketi")]
        SoleProprietorship,

        [Display(Name = "Diğer")]
        Other
    }
}
