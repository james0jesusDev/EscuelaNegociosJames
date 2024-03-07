using EscuelaNegociosJames.DbContext;
using EscuelaNegociosJames.Models;
using EscuelaNegociosJames.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EscuelaNegociosJames.Controllers
{

    // Cambios dia 8
    public class StudentController : Controller
    {
        Context _db;
        public StudentController(Context db)
        {
            _db = db;
        }

        public async Task<IActionResult> AllStudent()
        {
            var students = await _db.Students.ToListAsync();
            return View(students);
        }

        public IActionResult AddStudent()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddStudent(Student student)
        {
            _db.Add(student);
            await _db.SaveChangesAsync();
            return RedirectToAction("AllStudent");
        }



        public async Task<IActionResult> EnrollCourse(int id)
        {
            var studentDisplay = await _db.Students.Select(x => new
            {
                Id = x.StudentID,
                Value = x.StudentName
            }).ToListAsync();
            StudentAddEnrollmentViewModel vm = new StudentAddEnrollmentViewModel();
            vm.StudentList = new SelectList(studentDisplay, "Id", "Value");

            var club = await _db.Clubs.SingleOrDefaultAsync(c => c.ClubID == id);

            ViewBag.Club = club;
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> EnrollCourse(StudentAddEnrollmentViewModel vm)
        {
            if (await Comprueba(vm.Enrollment.Club.ClubID, vm.Enrollment.Student.StudentID))
            {
                return RedirectToAction("Information");
            }
            else
            {
                var student = await _db.Students.SingleOrDefaultAsync(s => s.StudentID == vm.Enrollment.Student.StudentID);
                var club = await _db.Clubs.SingleOrDefaultAsync(c => c.ClubID == vm.Enrollment.Club.ClubID);

                club.NumberOfStudents--;

                Enrollment enrollment = new Enrollment();
                enrollment.Student = student;
                enrollment.Club = club;
                _db.Add(enrollment);
                await _db.SaveChangesAsync();
                return RedirectToAction("AllClub", "Club");
            }
        }




        private async Task<bool> Comprueba(int clubId, int studentId)
        {
            bool encontrado;
            var enrollment = await _db.Enrollments.Where(e => e.Club.ClubID == clubId &&
            e.Student.StudentID == studentId).FirstOrDefaultAsync();

            encontrado = enrollment != null;
            return encontrado;
        }

        public async Task<IActionResult> AllClassmate(int id)
        {
            var enrollClub = await _db.Enrollments.Where(e => e.Club.ClubID == id).Include(e => e.Club).Include(e => e.Student).ToListAsync();
            List<Student> classmate = new List<Student>();
            foreach (var enroll in enrollClub)
            {
                var student = await _db.Students.SingleOrDefaultAsync(s => s.StudentID == enroll.Student.StudentID);
                classmate.Add(student);

            }
            ViewData["club"] = _db.Clubs.Find(id).ClubName;

            return View(classmate);
        }

    }
}
