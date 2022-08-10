namespace FrontendProject.Models
{
    public class CourseElementStudent
    {
        public int CourseID { get; set; }
        public string Title { get; set; }
        public int Credits { get; set; }

        public ICollection<EnrollmentStudent> Enrollments { get; set; }
    }
}
