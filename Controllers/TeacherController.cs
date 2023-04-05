using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Assignment2.PLL.Models;
using System.Diagnostics;

using Assignment2.BLL.Services.Contracts;
using Assignment2.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System.Text;
using Microsoft.AspNetCore.Identity;

using System;
using System.Security.Cryptography;
using System.Text;

namespace Assignment2.PLL.Controllers
{
    public class TeacherController : Controller
    {
        private readonly ILogger<TeacherController> _logger;

        private readonly ITeacherService _teacherService;
        private readonly IStudentService _studentService;
        private readonly ITokenService _tokenService;
        private readonly ISubjectService _subjectService;
        private readonly ILaboratoryService _laboratoryService;
        private readonly IAttendanceService _attendanceService;
        private readonly ISubmissionService _submissionService;
        public List<Teacher> teacherPersistent;
        public List<Student> studentPersistent;
        public List<Token> tokenPersistent;
        public List<Subject> subjectPersistent; 
        public List<Laboratory> laboratoryPersistent;
        public List<Attendance> attendancePersistent;
        public List<Submission> submissionPersistent;

        public TeacherController(ILogger<TeacherController> logger, ITeacherService teacherService, IStudentService studentService, ITokenService tokenService, ISubjectService subjectService, ILaboratoryService laboratoryService, IAttendanceService attendanceService, ISubmissionService submissionService)
        {
            _logger = logger;
            _teacherService = teacherService;  
            _studentService = studentService;
            _tokenService = tokenService;
            _subjectService = subjectService;
            _laboratoryService = laboratoryService;
            _attendanceService = attendanceService;
            _submissionService = submissionService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("TeacherLogin", "Access");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> ShowStudents()
        {
            List<Student> students = await _studentService.GetAllElements();
            studentPersistent = students;
            return View(students);
        }

        public async Task<ActionResult> DeleteStudent(int id)
        {
            var student = await _studentService.GetStudentById(id);

            if (student == null)
            {
                return NotFound();
            }


            await _studentService.DeleteStudent(student.StudentId);

            return RedirectToAction("ShowStudents", "Teacher");
        }

        
        [HttpGet]
        public async Task<ActionResult> EditStudent(int id)
        {
            var student = await _studentService.GetStudentById(id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        [HttpPost]
        public async Task<ActionResult> EditStudent(int studentId, string email, string password, string name, int groupNr, string hobby)
        {
            try
            {
                await _studentService.EditStudent(studentId,
                email, password, name,
                groupNr, hobby);
                return RedirectToAction("ShowStudents", "Teacher");
            }
            catch
            {
                return StatusCode(StatusCodes.Status401Unauthorized, new { message = "Not all fields completed" });
            }
        }

        public string GenerateTokenString()
        {
            byte[] tokenData = new byte[64];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(tokenData);
            }
            ViewData["TokenString"] = Convert.ToBase64String(tokenData);
            return Convert.ToBase64String(tokenData);
        }


        [HttpPost]
        public async Task<ActionResult> GenerateToken()
        {

            await _tokenService.CreateToken(new Token { TokenString = GenerateTokenString()});
            return RedirectToAction("ShowStudents", "Teacher");
        }

        public async Task<IActionResult> ShowTokens()
        {
            List<Token> tokens = await _tokenService.GetAllElements();
            tokenPersistent = tokens;
            return View(tokens);
        }

        public async Task<IActionResult> ShowSubjects()
        {
            List<Subject> subjects = await _subjectService.GetAllElements();
            subjectPersistent = subjects;
            return View(subjects);
        }

        [HttpGet]
        public async Task<ActionResult> CreateSubject()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CreateSubject(string title)
        {
            var teacher = _teacherService.GetTeacherByEmail((string)TempData["Email"]);
            
            try
            {
                Subject newSubject = new Subject
                {
                    TeacherId = teacher.TeacherId,
                    Title = title,
                };
                
                await _subjectService.CreateSubject(newSubject);
                return RedirectToAction("ShowSubjects", "Teacher");
            }
            catch
            {
                return RedirectToAction("CreateSubject", "Teacher");
            }
        }



        public async Task<ActionResult> DeleteSubject(int id)
        {
            var subject = await _subjectService.GetSubjectById(id);

            if (subject == null)
            {
                return NotFound();
            }


            await _subjectService.DeleteSubject(subject.SubjectId);

            return RedirectToAction("ShowSubjects", "Teacher");
        }


        [HttpGet]
        public async Task<ActionResult> EditSubject(int id)
        {
            var subject = await _subjectService.GetSubjectById(id);
            if (subject == null)
            {
                return NotFound();
            }

            return View(subject);
        }

        [HttpPost]
        public async Task<ActionResult> EditSubject(int subjectId, int teacherId, string title)
        {
            try
            {
                await _subjectService.EditSubject(subjectId,
                teacherId, title);
                return RedirectToAction("ShowSubjects", "Teacher");
            }
            catch
            {
                return StatusCode(StatusCodes.Status401Unauthorized, new { message = "Not all fields completed" });
            }
        }

        public async Task<ActionResult> ShowLaboratories(int id)
        {
            List<Laboratory> laboratories = await _laboratoryService.GetAllElements(id);
            TempData["LaboratoryId"] = id;
            laboratoryPersistent = laboratories;
            return View(laboratories);
        }

        [HttpGet]
        public async Task<ActionResult> CreateLaboratory()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CreateLaboratory(int number, DateTime date, string title, string curricula, string description)
        {

            try
            {
                Laboratory newLaboratory = new Laboratory
                {
                    SubjectId = (int)TempData["LaboratoryId"],
                    Number = number,
                    Date = date,
                    Title = title,
                    Curricula = curricula,
                    Description = description
                };

                await _laboratoryService.CreateLaboratory(newLaboratory);
                return RedirectToAction("ShowSubjects", "Teacher");
            }
            catch
            {
                return RedirectToAction("CreateSubject", "Teacher");
            }
        }

        public async Task<ActionResult> DeleteLaboratory(int id)
        {
            var laboratory= await _laboratoryService.GetLaboratoryById(id);

            if (laboratory == null)
            {
                return NotFound();
            }


            await _laboratoryService.DeleteLaboratory(laboratory.LaboratoryId);

            return RedirectToAction("ShowSubjects", "Teacher");
        }


        [HttpGet]
        public async Task<ActionResult> EditLaboratory(int id)
        {
            var laboratory = await _laboratoryService.GetLaboratoryById(id);
            if (laboratory == null)
            {
                return NotFound();
            }

            return View(laboratory);
        }

        [HttpPost]
        public async Task<ActionResult> EditLaboratory(int laboratoryId, int subjectId, int number, DateTime date, string title, string curricula, string description)
        {
            try
            {
                await _laboratoryService.EditLaboratory(laboratoryId, subjectId, number, date, title, curricula, description, null, new DateTime { }, null);
                return RedirectToAction("ShowSubjects", "Teacher");
            }
            catch
            {
                return StatusCode(StatusCodes.Status401Unauthorized, new { message = "Not all fields completed" });
            }
        }

        [HttpGet]
        public async Task<ActionResult> CreateAssignment(int id)
        {
            var laboratory = await _laboratoryService.GetLaboratoryById(id);
            if (laboratory == null)
            {
                return NotFound();
            }

            return View(laboratory);
        }

        [HttpPost]
        public async Task<ActionResult> CreateAssignment(int laboratoryId, int subjectId, int number, DateTime date, string title, string curricula, string description, string assignmentName, DateTime assignmentDl, string assignmentDescription)
        {
            try
            {
                await _laboratoryService.EditLaboratory(laboratoryId, 0, 0, new DateTime { }, null, null, null, assignmentName, assignmentDl, assignmentDescription);
                return RedirectToAction("ShowSubjects", "Teacher");
            }
            catch
            {
                return StatusCode(StatusCodes.Status401Unauthorized, new { message = "Not all fields completed" });
            }
        }

        [HttpGet]
        public async Task<ActionResult> EditAssignment(int id)
        {
            var laboratory = await _laboratoryService.GetLaboratoryById(id);
            if (laboratory == null)
            {
                return NotFound();
            }

            return View(laboratory);
        }

        [HttpPost]
        public async Task<ActionResult> EditAssignment(int laboratoryId, int subjectId, int number, DateTime date, string title, string curricula, string description, string assignmentName, DateTime assignmentDl, string assignmentDescription)
        {
            try
            {
                await _laboratoryService.EditLaboratory(laboratoryId, 0, 0, new DateTime { }, null, null, null, assignmentName, assignmentDl, assignmentDescription);
                return RedirectToAction("ShowSubjects", "Teacher");
            }
            catch
            {
                return StatusCode(StatusCodes.Status401Unauthorized, new { message = "Not all fields completed" });
            }
        }

        public async Task<ActionResult> ShowAttendances(int id)
        {
            List<Attendance> attendances = await _attendanceService.GetAllElements(id);
            TempData["LaboratoryId"] = id;
            attendancePersistent = attendances;
            return View(attendances);
        }

        [HttpGet]
        public async Task<ActionResult> CreateAttendance()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CreateAttendance(int attendanceId, int laboratoryId, int studentId, int attended)
        {
            var student = _studentService.GetStudentById(studentId);
            if(student == null)
            {
                return NotFound();
            }
            try
            {
                Attendance newAttendance = new Attendance
                {
                    LaboratoryId = (int)TempData["LaboratoryId"],
                    StudentId = studentId,
                    Attended = attended
                };

                await _attendanceService.CreateAttendance(newAttendance);
                return RedirectToAction("ShowSubjects", "Teacher");
            }
            catch
            {
                return RedirectToAction("CreateAttendance", "Teacher");
            }
        }

        public async Task<ActionResult> DeleteAttendance(int id)
        {
            var attendance = await _attendanceService.GetAttendanceById(id);

            if (attendance == null)
            {
                return NotFound();
            }


            await _attendanceService.DeleteAttendance(attendance.AttendanceId);

            return RedirectToAction("ShowSubjects", "Teacher");
        }


        [HttpGet]
        public async Task<ActionResult> EditAttendance(int id)
        {
            var attendance = await _attendanceService.GetAttendanceById(id);
            if (attendance == null)
            {
                return NotFound();
            }

            return View(attendance);
        }

        [HttpPost]
        public async Task<ActionResult> EditAttendance(int attendanceId, int laboratoryId, int studentId, int attended)
        {
            var attendance = await _attendanceService.GetAttendanceById(studentId);
            var laboratory = await _laboratoryService.GetLaboratoryById(laboratoryId);
            if(attendance == null || laboratory == null)
            {
                return NotFound();
            }

            try
            {
                await _attendanceService.EditAttendance(attendanceId, laboratoryId, studentId, attended);
                return RedirectToAction("ShowSubjects", "Teacher");
            }
            catch
            {
                return StatusCode(StatusCodes.Status401Unauthorized, new { message = "Not all fields completed" });
            }
        }

        public async Task<ActionResult> ShowSubmissions(int id)
        {
            List<Submission> submissions = await _submissionService.GetAllAssignments(id);
            submissionPersistent = submissions;
            return View(submissions);
        }

        [HttpGet]
        public async Task<ActionResult> GradeSubmission(int id)
        {
            var submission = await _submissionService.GetSubmissionById(id);
            if (submission == null)
            {
                return NotFound();
            }

            return View(submission);
        }

        [HttpPost]
        public async Task<ActionResult> GradeSubmission(int submissionId, int grade)
        {
            try
            {
                await _submissionService.GradeSubmission(submissionId, grade);
                return RedirectToAction("ShowSubjects", "Teacher");
            }
            catch
            {
                return StatusCode(StatusCodes.Status401Unauthorized, new { message = "Not all fields completed" });
            }
        }

    }
}
