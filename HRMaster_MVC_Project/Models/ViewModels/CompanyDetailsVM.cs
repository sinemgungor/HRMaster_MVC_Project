using HRMaster_MVC_Project.Models.Enums;

namespace HRMaster_MVC_Project.Models.ViewModels
{
    public class CompanyDetailsVM
    {
        public string CompanyName { get; set; }
        public string? LogoPath { get; set; }
        public string MersisNumber { get; set; }
        public string TaxIdentificaitonNumber { get; set; }
        public string TaxOfficeName { get; set; }
        public string CompanyAddress { get; set; }
        public string CompanyPhoneNumber { get; set; }
        public string CompanyEmail { get; set; }
        public string? CompanyWebsite { get; set; }
        public DateOnly EstablishedDate { get; set; }
        public int EmployeeCount { get; set; }
        public string CompanyInformation { get; set; }
        //public DateTime ContractStartDate { get; set; }  
        //public DateTime ContractEndDate { get; set; }    
        public Status Status { get; set; }

        public int? CEOId { get; set; } 
    }
}
