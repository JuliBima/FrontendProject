using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyBackendProject.DAL;
using MyBackendProject.DTO;
using MyBackendProject.Models;

namespace MyBackendProject.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudent _studentDAL;
        private readonly IMapper _mapper;

        public StudentController(IStudent studentDAL, IMapper mapper)
        {
            _studentDAL = studentDAL;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<StudentDTO>> Get()
        {
           
            var results = await _studentDAL.GetAll();
            var DTO = _mapper.Map<IEnumerable<StudentDTO>>(results);

            return DTO;
        }

        [HttpGet("{id}")]
        public async Task<StudentDTO> GetById(int id)
        {

            var result = await _studentDAL.GetById(id);
            if (result == null) throw new Exception($"data {id} tidak ditemukan");
            var DTO = _mapper.Map<StudentDTO>(result);

            return DTO;
        }

        [HttpGet("ByName")]
        public async Task<IEnumerable<StudentDTO>> GetByName(string fristMidName, string lastName)
        {
            List<StudentDTO> studentDtos = new List<StudentDTO>();
            var results = await _studentDAL.GetByName(fristMidName, lastName);
            foreach (var result in results)
            {
                studentDtos.Add(new StudentDTO
                {
                    ID = result.ID,
                    FirstMidName = result.FirstMidName,
                    LastName = result.LastName
                });
            }
            return studentDtos;
        }

        [HttpGet("ByFristMidName")]
        public async Task<IEnumerable<StudentDTO>> GetByFristMidName(string fristMidName)
        {
            List<StudentDTO> studentDtos = new List<StudentDTO>();
            var results = await _studentDAL.GetByFristMidName(fristMidName);
            foreach (var result in results)
            {
                studentDtos.Add(new StudentDTO
                {
                    ID = result.ID,
                    FirstMidName = result.FirstMidName,
                    LastName = result.LastName
                });
            }
            return studentDtos;
        }

        [HttpGet("ByLastName")]
        public async Task<IEnumerable<StudentDTO>> GetLastName(string lastName)
        {
            List<StudentDTO> studentDtos = new List<StudentDTO>();
            var results = await _studentDAL.GetByLastName(lastName);
            foreach (var result in results)
            {
                studentDtos.Add(new StudentDTO
                {
                    ID = result.ID,
                    FirstMidName = result.FirstMidName,
                    LastName = result.LastName
                });
            }
            return studentDtos;
        }


        [HttpGet("Pagging/{skip}/{take}")]
        public async Task<IEnumerable<StudentDTO>> Pagging(int skip, int take)
        {

            var results = await _studentDAL.Pagging(skip, take);
            var DTO = _mapper.Map<IEnumerable<StudentDTO>>(results);

            return DTO;
        }

        [HttpGet("WithEnrollmentCourses")]
        public async Task<IEnumerable<StudentEnrollmentCourseDTO>> GetEnrollmentCourses()
        {
            var results = await _studentDAL.GetEnrollmentCourses();
            var DTO = _mapper.Map<IEnumerable<StudentEnrollmentCourseDTO>>(results);
            return DTO;
        }

        [HttpPost]
        public async Task<ActionResult> Post(StudentCreateDTO studentCreateDto)
        {
            try
            {
                var newStudent = _mapper.Map<Student>(studentCreateDto);
                var result = await _studentDAL.Insert(newStudent);
                var Dto = _mapper.Map<StudentDTO>(result);

                return CreatedAtAction("Get", new { id = result.ID }, Dto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult> Put(StudentEditDTO studentDto)
        {
            try
            {
                var update = _mapper.Map<Student>(studentDto);
                var result = await _studentDAL.Update(update);
                var DTO = _mapper.Map<StudentDTO>(result);
                return Ok(studentDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _studentDAL.Delete(id);
                return Ok($"Data dengan id {id} berhasil didelete");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
