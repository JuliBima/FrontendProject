namespace MyBackendProject.DTO
{
    public class EnrollmentStudentCourseDTO
    {
        public int EnrollmentID { get; set; }
        public int CourseID { get; set; }
        public int StudentID { get; set; }
        public Grade? Grade { get; set; }
        public CourseDTO Course { get; set; }

        public StudentDTO Student { get; set; }

    }
}
