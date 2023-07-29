namespace University.Core.Entities
{
    public class LectureGroup
    {
        public int GroupId { get; set; }
        public Group Group { get; set; }

        public int LectureId { get; set; }
        public Lecture Lecture { get; set; }
    }
}
