using System.ComponentModel.DataAnnotations;

namespace HRMaster_MVC_Project.Models.Enums
{
    public enum AdvanceType
    {
        [Display(Name = "Maaş Avansı")]
        SalaryAdvance,

        [Display(Name = "Seyahat Avansı")]
        TravelAdvance,

        [Display(Name = "Eğitim Avansı")]
        EducationAdvance,

        [Display(Name = "Proje Avansı")]
        ProjectAdvance,

        [Display(Name = "Acil Durum Avansı")]
        EmergencyAdvance
    }
}


