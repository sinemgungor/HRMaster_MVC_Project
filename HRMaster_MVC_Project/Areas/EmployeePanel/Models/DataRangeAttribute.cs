using System;
using System.ComponentModel.DataAnnotations;


namespace HRMaster_MVC_Project.Areas.EmployeePanel.Models
{
    public class DateRangeAttribute : ValidationAttribute
    {
        private readonly string _startDatePropertyName;

        // Constructor - Notice that there is no return type here
        public DateRangeAttribute(string startDatePropertyName)
        {
            _startDatePropertyName = startDatePropertyName;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var endDate = (DateOnly)value;
            var startDateProperty = validationContext.ObjectType.GetProperty(_startDatePropertyName);

            if (startDateProperty == null)
            {
                return new ValidationResult($"Property '{_startDatePropertyName}' is not found.");
            }

            var startDate = (DateOnly)startDateProperty.GetValue(validationContext.ObjectInstance);

            if (endDate < startDate)
            {
                return new ValidationResult("İzin bitiş tarihi başlangıç tarihinden önce olamaz.");
            }

            return ValidationResult.Success;
        }
    }
}
