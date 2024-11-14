using System.ComponentModel.DataAnnotations;

namespace HRMaster_MVC_Project.Models.Enums
{
    public enum Currency
    {
        [Display(Name = "Türk Lirası")]
        TRY,  // Türk Lirası

        [Display(Name = "Amerikan Doları")]
        USD,  // Amerikan Doları

        [Display(Name = "Euro")]
        EUR,  // Euro

        [Display(Name = "İngiliz Sterlini")]
        GBP,  // İngiliz Sterlini

        [Display(Name = "Japon Yeni")]
        JPY,  // Japon Yeni

        [Display(Name = "İsviçre Frangı")]
        CHF,  // İsviçre Frangı

        [Display(Name = "Kanada Doları")]
        CAD,  // Kanada Doları

        [Display(Name = "Avustralya Doları")]
        AUD,  // Avustralya Doları

        [Display(Name = "Çin Yuanı")]
        CNY,  // Çin Yuanı

        [Display(Name = "Rus Rublesi")]
        RUB,  // Rus Rublesi

        [Display(Name = "Hindistan Rupisi")]
        INR,  // Hindistan Rupisi

        [Display(Name = "Brezilya Reali")]
        BRL,  // Brezilya Reali

        [Display(Name = "Güney Afrika Randı")]
        ZAR,  // Güney Afrika Randı

        [Display(Name = "Meksika Pesosu")]
        MXN,  // Meksika Pesosu

        [Display(Name = "Güney Kore Wonu")]
        KRW,  // Güney Kore Wonu

        [Display(Name = "Singapur Doları")]
        SGD,  // Singapur Doları

        [Display(Name = "Hong Kong Doları")]
        HKD,  // Hong Kong Doları

        [Display(Name = "Yeni Zelanda Doları")]
        NZD,  // Yeni Zelanda Doları
    }
}
