using System.ComponentModel.DataAnnotations;

namespace University.Core.Attributes
{
    public class EndTimeAttribute : ValidationAttribute
    {
        public string StartTimePropertyName { get; set; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is TimeSpan endTime)
            {
                var propertyInfo = validationContext.ObjectType.GetProperty(StartTimePropertyName);
                if (propertyInfo != null)
                {
                    var startTime = (TimeSpan)propertyInfo.GetValue(validationContext.ObjectInstance);

                    if (endTime > startTime)
                    {
                        return ValidationResult.Success;
                    }
                }

                return new ValidationResult($"{validationContext.DisplayName} must be greater than the start time");
            }

            return new ValidationResult($"Invalid {validationContext.DisplayName}");
        }
    }
}
