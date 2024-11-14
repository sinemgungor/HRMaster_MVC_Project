using System.ComponentModel.DataAnnotations;

namespace HRMaster_MVC_Project.Models.Enums
{
    public enum LeaveType
    {
        [Display(Name = "Yıllık İzin")]
        AnnualLeave,

        [Display(Name = "Hastalık İzni")]
        SickLeave,

        [Display(Name = "Ücretsiz İzin")]
        UnpaidLeave,

        [Display(Name = "Doğum İzni")]
        MaternityLeave,

        [Display(Name = "Babalık İzni")]
        PaternityLeave,

        [Display(Name = "Evlilik İzni")]
        MarriageLeave,

        [Display(Name = "Vefat İzni")]
        BereavementLeave,

        [Display(Name = "Yol İzni")]
        TravelLeave
    }
}
