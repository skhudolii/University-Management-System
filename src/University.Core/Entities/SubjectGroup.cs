namespace University.Core.Entities
{
    public class SubjectGroup
    {
        public int GroupId { get; set; }
        public Group Group { get; set; }

        public int SubjectId { get; set; }
        public Subject Subject { get; set; }
    }
}
