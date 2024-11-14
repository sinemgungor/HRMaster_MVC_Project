using System.ComponentModel.DataAnnotations;

namespace HRMaster_MVC_Project.Models
{
    public class PasswordComplexityAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var password = value as string;

            if (string.IsNullOrEmpty(password))
            {
                return new ValidationResult("Şifre gereklidir.");
            }

            // Şifre en az 6 karakter uzunluğunda olmalı
            if (password.Length < 6)
            {
                return new ValidationResult("Şifre en az 6 karakter uzunluğunda olmalıdır.");
            }

            // Şifre büyük harf içermeli
            if (!password.Any(char.IsUpper))
            {
                return new ValidationResult("Şifre en az bir büyük harf içermelidir.");
            }

            // Şifre küçük harf içermeli
            if (!password.Any(char.IsLower))
            {
                return new ValidationResult("Şifre en az bir küçük harf içermelidir.");
            }

            // Şifre sayı içermeli
            if (!password.Any(char.IsDigit))
            {
                return new ValidationResult("Şifre en az bir rakam içermelidir.");
            }

            // Şifre özel karakter içermeli (noktalama işareti veya özel karakter)
            if (!password.Any(ch => char.IsPunctuation(ch) || char.IsSymbol(ch)))
            {
                return new ValidationResult("Şifre en az bir özel karakter içermelidir.");
            }

            return ValidationResult.Success;
        }
    }
}
