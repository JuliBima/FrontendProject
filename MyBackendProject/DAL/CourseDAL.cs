using Microsoft.EntityFrameworkCore;
using MyBackendProject.Data;
using MyBackendProject.Models;

namespace MyBackendProject.DAL
{
    public class CourseDAL : ICourse
    {
        private readonly AppDbContext _context;

        public CourseDAL(AppDbContext context)
        {
            _context = context;
        }
        public async Task Delete(int id)
        {
            try
            {
                var delete = await _context.Courses.FirstOrDefaultAsync(s => s.CourseID == id);
                if (delete == null)
                    throw new Exception($"Data dengan id {id} tidak ditemukan");
                _context.Courses.Remove(delete);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<IEnumerable<Course>> GetAll()
        {
            var results = await _context.Courses.OrderBy(s => s.CourseID).ToListAsync();
            return results;
        }

        public async Task<Course> GetById(int id)
        {
            var result = await _context.Courses.FirstOrDefaultAsync(s => s.CourseID == id);
            if (result == null) throw new Exception($"Data dengan id {id} tidak ditemukan");
            return result;
        }

        public async Task<IEnumerable<Course>> GetByTitle(string title)
        {
            var results = await _context.Courses.Where(s => s.Title.Contains(title)).ToListAsync();

            return results;
        }

        public async Task<IEnumerable<Course>> GetEnrollmentStudent()
        {
            var results = await _context.Courses.Include(s => s.Enrollments)
                .ThenInclude(s => s.Student).ToListAsync();
            return results;
        }

        public async Task<Course> Insert(Course obj)
        {
            try
            {
                //var course = _context.Courses.FirstOrDefault(s => s.CourseID == obj.CourseID);
                int idcourse = _context.Courses.Select(a => a.CourseID).Max();
                obj.CourseID = idcourse + 1;
                _context.Courses.Add(obj);
                await _context.SaveChangesAsync();
                return obj;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<IEnumerable<Course>> Pagging(int skip, int take)
        {
            var results = await _context.Courses
               .Skip(skip).Take(take).ToArrayAsync();
            return results;
        }

        public async Task<Course> Update(Course obj)
        {
            try
            {
                var update = await _context.Courses.FirstOrDefaultAsync(s => s.CourseID == obj.CourseID);
                if (update == null)
                    throw new Exception($"Data dengan id {obj.CourseID} tidak ditemukan");

                update.Title = obj.Title;
                update.Credits = obj.Credits;
                await _context.SaveChangesAsync();
                return obj;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }
    }
}
