using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University.Core.Enums
{
    // TO DO: make AssistantProfessor as Asst. Prof. and etc.
    public class AcademicPositionExtensions
    {
        public static string GetAcademicPositionAbbreviation(AcademicPosition academicPosition)
        {
            switch (academicPosition)
            {
                case AcademicPosition.AssistantProfessor:
                    return "Asst. Prof.";
                case AcademicPosition.SeniorResearcher:
                    return "Sr. Rsch.";
                case AcademicPosition.AssociateProfessor:
                    return "Assoc. Prof.";
                case AcademicPosition.Professor:
                    return "Prof.";
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
