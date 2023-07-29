using System.ComponentModel.DataAnnotations;

namespace University.Core.Attributes
{
    public class StartTimeAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var lectureDateProperty = validationContext.ObjectType.GetProperty("LectureDate");
            if (lectureDateProperty != null)
            {
                var lectureDateValue = lectureDateProperty.GetValue(validationContext.ObjectInstance);
                if (lectureDateValue is DateTime lectureDate && lectureDate.Date == DateTime.Now.Date)
                {
                    if (value is TimeSpan startTime)
                    {
                        var currentTime = DateTime.Now.TimeOfDay;
                        if (startTime >= currentTime)
                        {
                            return ValidationResult.Success;
                        }
                    }

                    return new ValidationResult("Start time cannot be before the current time for today's lecture");
                }
            }

            return ValidationResult.Success;
        }
    }
}
