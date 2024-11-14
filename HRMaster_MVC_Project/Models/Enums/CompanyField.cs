using System.ComponentModel.DataAnnotations;

namespace HRMaster_MVC_Project.Models.Enums
{
    public enum CompanyField
    {
        [Display(Name = "Teknoloji")]
        Technology = 1,

        [Display(Name = "Sağlık")]
        Healthcare,

        [Display(Name = "Perakende")]
        Retail,

        [Display(Name = "Üretim")]
        Manufacturing,

        [Display(Name = "Enerji")]
        Energy,

        [Display(Name = "Ulaşım ve Lojistik")]
        TransportationAndLogistics,

        [Display(Name = "Eğitim")]
        Education,

        [Display(Name = "Toptan Satış")]
        Wholesale,

        [Display(Name = "Tarım ve Gıda")]
        AgricultureAndFood,

        [Display(Name = "Hizmetler")]
        Services,

        [Display(Name = "Medya ve Eğlence")]
        MediaAndEntertainment,

        [Display(Name = "Telekomünikasyon")]
        Telecommunications,

        [Display(Name = "Finansal Teknoloji")]
        Fintech,

        [Display(Name = "Gayrimenkul")]
        RealEstate,

        [Display(Name = "Otomotiv")]
        Automotive,

        [Display(Name = "Kimya")]
        Chemicals,

        [Display(Name = "Turizm ve Konaklama")]
        TourismAndHospitality,

        [Display(Name = "Bilişim")]
        IT,

        [Display(Name = "Bankacılık ve Sermaye")]
        BankingAndCapital,

        [Display(Name = "Sigortacılık ve Bireysel Emeklilik")]
        InsuranceAndPension,

        [Display(Name = "Hukuk")]
        Legal,

        [Display(Name = "Emlak Yönetimi")]
        PropertyManagement,

        [Display(Name = "Gıda ve İçecek")]
        FoodAndBeverage,

        [Display(Name = "Lojistik ve Dağıtım")]
        LogisticsAndDistribution,

        [Display(Name = "Yazılım")]
        Software,

        [Display(Name = "E-ticaret")]
        ECommerce,

        [Display(Name = "Elektrik ve Elektronik")]
        ElectricalAndElectronics,

        [Display(Name = "Maden ve Metal")]
        MiningAndMetals,

        [Display(Name = "Sağlık Teknolojisi")]
        HealthTech,

        [Display(Name = "Çevre ve Atık Yönetimi")]
        EnvironmentalAndWasteManagement,

        [Display(Name = "Güvenlik")]
        Security,

        [Display(Name = "Sanat ve Tasarım")]
        ArtAndDesign,

        [Display(Name = "Dijital Pazarlama")]
        DigitalMarketing,

        [Display(Name = "Oyun ve Eğlence")]
        GamingAndEntertainment,

        [Display(Name = "İnşaat ve Altyapı")]
        ConstructionAndInfrastructure,

        [Display(Name = "Yatırım ve Finans")]
        InvestmentAndFinance
    }

   
}