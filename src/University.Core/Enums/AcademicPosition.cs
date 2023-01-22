using System.ComponentModel;

namespace University.Core.Enums
{
    public enum AcademicPosition
    {
        [Description("Assistant Professor")]
        AsstProf = 1,

        [Description("Senior Researcher")]
        SrRsch,

        [Description("Associate Professor")]
        AssocProf,

        [Description("Professor")]
        Prof
    }
}
