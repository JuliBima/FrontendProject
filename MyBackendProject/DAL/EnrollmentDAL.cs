using Microsoft.EntityFrameworkCore;
using MyBackendProject.Data;
using MyBackendProject.Models;

namespace MyBackendProject.DAL
{
    public class EnrollmentDAL : IEnrollment
    {
        private readonly AppDbContext _context;

        public EnrollmentDAL(AppDbContext context)
        {
            _context = context;
        }
        public async Task Delete(int id)
        {
            try
            {
                var delete = await _context.Enrollments.FirstOrDefaultAsync(s => s.EnrollmentID == id);
                if (delete == null)
                    throw new Exception($"Data dengan id {id} tidak ditemukan");
                _context.Enrollments.Remove(delete);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<IEnumerable<Enrollment>> GetAll()
        {
            var results = await _context.Enrollments.OrderBy(s => s.EnrollmentID).ToListAsync();
            return results;
        }


        public async Task<Enrollment> GetById(int id)
        {
            var result = await _context.Enrollments.Include(s => s.Course).Include(s => s.Student).FirstOrDefaultAsync(s => s.EnrollmentID == id);
            if (result == null) throw new Exception($"Data dengan id {id} tidak ditemukan");
            return result;
        }

        public async Task<IEnumerable<Enrollment>> GetEnrollmentStudentCourses()
        {
            var results = await _context.Enrollments.Include(s => s.Course).Include(s => s.Student).ToListAsync();
            return results;
        }


        public async Task<Enrollment> Insert(Enrollment obj)
        {
            try
            {
                //obj.CourseID = obj.Course.CourseID;
                //obj.StudentID = await _context.Students.Select(n => n.value = ID).ToListAsync();
                _context.Enrollments.Add(obj);
                //List<AppContext> students = _context.Students.Select(n =>
                //new AppContext
                //{
                //    Value = n.ID,
                //    Text = n.FirstMidName
                //}).ToList();
                
                await _context.SaveChangesAsync();
                return obj;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }

        }

        public async Task<Enrollment> InsertEnrollment(Enrollment obj, int studentID, int courseID)
        {


            obj.StudentID = studentID;
            obj.CourseID = courseID;

            _context.Enrollments.Add(obj);
            await _context.SaveChangesAsync();
            
            return obj;


        }

        public async Task<IEnumerable<Enrollment>> Pagging(int skip, int take)
        {
            var results = await _context.Enrollments.Include(s => s.Course).Include(s => s.Student)
               .Skip(skip).Take(take).ToArrayAsync();
            return results;
        }

        public async Task<Enrollment> Update(Enrollment obj)
        {
            try
            {
                var update = await _context.Enrollments.FirstOrDefaultAsync(s => s.EnrollmentID == obj.EnrollmentID);
                if (update == null)
                    throw new Exception($"Data dengan id {obj.EnrollmentID} tidak ditemukan");

                update.CourseID = obj.CourseID;
                update.StudentID = obj.StudentID;
                update.Grade = obj.Grade;
               
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
