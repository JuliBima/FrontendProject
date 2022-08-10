namespace FrontendProject.Models
{
    public class StudentEnrollmentCourse
    {
        public int ID { get; set; }
        public string LastName { get; set; }
        public string FirstMidName { get; set; }
        public DateTime EnrollmentDate { get; set; }

        public ICollection<EnrollmentCourse> Enrollments { get; set; }
    }
}
