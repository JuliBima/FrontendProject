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



        }
    }
}
