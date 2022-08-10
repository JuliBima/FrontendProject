namespace MyBackendProject.DTO
{
    public class CourseEnrollmentStudentDTO
    {
        public int CourseID { get; set; }
        public string Title { get; set; }
        public int Credits { get; set; }

        public ICollection<EnrollmentStudentDTO> Enrollments { get; set; }
    }
}
