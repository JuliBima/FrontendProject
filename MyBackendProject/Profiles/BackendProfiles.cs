using AutoMapper;
using MyBackendProject.DTO;
using MyBackendProject.Models;

namespace MyBackendProject.Profiles
{
    public class BackendProfiles : Profile
    {
        public BackendProfiles()
        {
            //Student
            CreateMap<StudentCreateDTO, Student>();
            CreateMap<Student, StudentCreateDTO>();
            CreateMap<StudentDTO, Student>();
            CreateMap<Student, StudentDTO>();
            CreateMap<StudentEditDTO, Student>();
            CreateMap<Student, StudentEditDTO>();

            //Course
            CreateMap<CourseDTO, Course>();
            CreateMap<Course, CourseDTO>();
            CreateMap<CourseCreateDTO, Course>();
            CreateMap<Course, CourseCreateDTO>();

            //Enrollment
            CreateMap<EnrollmentDTO, Enrollment>();
            CreateMap<Enrollment, EnrollmentDTO>();
            CreateMap<EnrollmentCreateDTO, Enrollment>();
            CreateMap<Enrollment, EnrollmentCreateDTO>();
            CreateMap<EnrollmentEditDTO, Enrollment>();
            CreateMap<Enrollment, EnrollmentEditDTO>();

            //EnrollmentStudentCourse
            CreateMap<EnrollmentStudentCourseDTO, Enrollment>();
            CreateMap<Enrollment, EnrollmentStudentCourseDTO>();
            CreateMap<EnrollmentCourseDTO, Enrollment>();
            CreateMap<Enrollment, EnrollmentCourseDTO>();
            CreateMap<EnrollmentStudentDTO, Enrollment>();
            CreateMap<Enrollment, EnrollmentStudentDTO>();
            //CreateMap<EnrollmentStudentCourseDTO, Course>();
            //CreateMap<Course, EnrollmentStudentCourseDTO>();

            //CreateMap<CourseDTO, Enrollment>();
            //CreateMap<Enrollment, CourseDTO>();
            //CreateMap<StudentDTO, Enrollment>();
            //CreateMap<Enrollment, StudentDTO>();

            CreateMap<StudentEnrollmentCourseDTO, Student>();
            CreateMap<Student, StudentEnrollmentCourseDTO>();
            CreateMap<CourseEnrollmentStudentDTO, Course>();
            CreateMap<Course, CourseEnrollmentStudentDTO>();
            





        }
    }
}
