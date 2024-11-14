using HRMaster_MVC_Project.Models.Enums;

namespace HRMaster_MVC_Project.Areas.AdminPanel.Models.AdminViewModels
{
    public class ShowCompanyVM
    {
        public int ID { get; set; }
        public string CompanyName { get; set; }

        public string CompanyTitle { get; set; }

        public string? LogoPath { get; set; }

        public string MersisNumber { get; set; }

        public string TaxOfficeName { get; set; }

        public string TaxIdentificaitonNumber { get; set; }

        public string CompanyAddress { get; set; }

        public string CompanyPhoneNumber { get; set; }

        public string CompanyEmail { get; set; }


        public string? CompanyWebsite { get; set; }

        public CompanyType CompanyType { get; set; }

        public CompanyField CompanyField { get; set; }

        public DateOnly EstablishedDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);

        public DateOnly ContractStartingDate { get; set; }
        public DateOnly ContractEndDate { get; set; }

        public string CompanyInformation { get; set; }
    }
}
