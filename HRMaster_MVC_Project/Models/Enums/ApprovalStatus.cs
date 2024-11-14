using System.ComponentModel.DataAnnotations;

namespace HRMaster_MVC_Project.Models.Enums
{
    public enum ApprovalStatus
    {
        [Display(Name = "Onay Bekleyen")]
        Pending,

        [Display(Name = "Onaylandı")]
        Approved,

        [Display(Name = "Reddedildi")]
        Rejected,

        [Display(Name = "İptal Edildi")]
        Cancelled
    }
}
