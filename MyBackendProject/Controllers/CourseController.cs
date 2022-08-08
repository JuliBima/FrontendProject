using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyBackendProject.DAL;
using MyBackendProject.DTO;
using MyBackendProject.Models;

namespace MyBackendProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourse _courseDAL;
        private readonly IMapper _mapper;

        public CourseController(ICourse courseDAL, IMapper mapper)
        {
            _courseDAL = courseDAL;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<CourseDTO>> Get()
        {

            var results = await _courseDAL.GetAll();
            var DTO = _mapper.Map<IEnumerable<CourseDTO>>(results);

            return DTO;
        }

        [HttpGet("ByTitle")]
        public async Task<IEnumerable<CourseDTO>> GetByName(string title)
        {
            List<CourseDTO> courseDtos = new List<CourseDTO>();
            var results = await _courseDAL.GetByTitle(title);
            foreach (var result in results)
            {
                courseDtos.Add(new CourseDTO
                {
                    CourseID = result.CourseID,
                    Title = result.Title,
                    Credits = result.Credits
                });
            }
            return courseDtos;
        }

        [HttpGet("Pagging/{skip}/{take}")]
        public async Task<IEnumerable<CourseDTO>> Pagging(int skip, int take)
        {

            var results = await _courseDAL.Pagging(skip, take);
            var DTO = _mapper.Map<IEnumerable<CourseDTO>>(results);

            return DTO;
        }

        [HttpGet("{id}")]
        public async Task<CourseDTO> GetById(int id)
        {
           
            var result = await _courseDAL.GetById(id);
            if (result == null) throw new Exception($"data {id} tidak ditemukan");
            var DTO = _mapper.Map<CourseDTO>(result);

            return DTO;
        }

        [HttpPost]
        
        public async Task<ActionResult> Post(CourseCreateDTO CreateDto)
        {
            try
            {
                var newcourse = _mapper.Map<Course>(CreateDto);
                var result = await _courseDAL.Insert(newcourse);
                var Dto = _mapper.Map<CourseDTO>(result);



                //return CreatedAtAction(nameof(GetById), new { id = result.CourseID + 1 }, Dto);
                return CreatedAtAction("Get", new { id = result.CourseID }, Dto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult> Put(CourseDTO courseDto)
        {
            try
            {
                var update = _mapper.Map<Course>(courseDto);
                var result = await _courseDAL.Update(update);
                var DTO = _mapper.Map<CourseDTO>(result);
                return Ok(courseDto);
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
                await _courseDAL.Delete(id);
                return Ok($"Data dengan id {id} berhasil didelete");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
