using System.ComponentModel.DataAnnotations;

namespace University.Core.Attributes
{
    public class LectureDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object value) // Return a boolean value: true == IsValid, false != IsValid
        {
            if (value is DateTime dateTime)
            {
                return dateTime.Date >= DateTime.Now.Date; //Dates greater than or equal to today are valid (true)
            }

            return false;
        }
    }
}
