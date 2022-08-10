using Microsoft.EntityFrameworkCore;
using MyBackendProject.Data;
using MyBackendProject.Models;

namespace MyBackendProject.DAL
{
    public class StudentDAL : IStudent
    {
        private readonly AppDbContext _context;

        public StudentDAL(AppDbContext context)
        {
            _context = context;
        }
        public async Task Delete(int id)
        {
            try
            {
                var delete = await _context.Students.FirstOrDefaultAsync(s => s.ID == id);
                if (delete == null)
                    throw new Exception($"Data dengan id {id} tidak ditemukan");
                _context.Students.Remove(delete);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<IEnumerable<Student>> GetAll()
        {
            var results = await _context.Students.OrderBy(s => s.ID).ToListAsync();
            return results;
        }

        public async Task<Student> GetById(int id)
        {
            var result = await _context.Students.FirstOrDefaultAsync(s => s.ID == id);
            if (result == null) throw new Exception($"Data dengan id {id} tidak ditemukan");
            return result;
        }

        public async Task<IEnumerable<Student>> GetByName(string fristMidNamme, string lastName)
        {
            var results = await _context.Students.Where(s => s.FirstMidName.Contains(fristMidNamme)).Where(s => s.LastName.Contains(lastName)).ToListAsync();

            return results;
        }

        public async Task<IEnumerable<Student>> GetEnrollmentCourses()
        {
            var results = await _context.Students.Include(s => s.Enrollments)
                .ThenInclude(s => s.Course).ToListAsync();
            return results;
        }

        public async Task<Student> Insert(Student obj)
        {
            try
            {
                _context.Students.Add(obj);
                await _context.SaveChangesAsync();
                return obj;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<IEnumerable<Student>> Pagging(int skip, int take)
        {
            var results = await _context.Students
               .Skip(skip).Take(take).ToArrayAsync();
            return results;
        }

        public async Task<Student> Update(Student obj)
        {
            try
            {
                var update = await _context.Students.FirstOrDefaultAsync(s => s.ID == obj.ID);
                if (update == null)
                    throw new Exception($"Data dengan id {obj.ID} tidak ditemukan");

                update.LastName = obj.LastName;
                update.FirstMidName = obj.FirstMidName;
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
