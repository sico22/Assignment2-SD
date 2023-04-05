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
    public class StudentController : Controller
    {
        private readonly ILogger<TeacherController> _logger;

        private readonly ITeacherService _teacherService;
        private readonly IStudentService _studentService;
        private readonly ISubjectService _subjectService;
        private readonly ILaboratoryService _laboratoryService;
        private readonly ISubmissionService _submissionService;
        public List<Teacher> teacherPersistent;
        public List<Student> studentPersistent;
        public List<Subject> subjectPersistent; 
        public List<Laboratory> laboratoryPersistent;
        public List<Submission> submissionPersistent;

        public StudentController(ILogger<TeacherController> logger, ITeacherService teacherService, IStudentService studentService, ISubjectService subjectService, ILaboratoryService laboratoryService, ISubmissionService submissionService)
        {
            _logger = logger;
            _teacherService = teacherService;  
            _studentService = studentService;
            _subjectService = subjectService;
            _laboratoryService = laboratoryService;
            _submissionService = submissionService;
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

        public async Task<IActionResult> ShowSubjects()
        {
            List<Subject> subjects = await _subjectService.GetAllElements();
            subjectPersistent = subjects;
            return View(subjects);
        }

        public async Task<ActionResult> ShowLaboratories(int id)
        {
            List<Laboratory> laboratories = await _laboratoryService.GetAllElements(id);
            TempData["LaboratoryId"] = id;
            laboratoryPersistent = laboratories;
            return View(laboratories);
        }

        public async Task<ActionResult> ShowSubmissions()
        {
            var student = _studentService.GetStudentByEmail((string)TempData["Email"]);
            TempData["Email"] = student.StudentId;
            List<Submission> submissions = await _submissionService.GetAllElements(student.StudentId);
            submissionPersistent = submissions;
            return View(submissions);
        }

        [HttpGet]
        public async Task<ActionResult> CreateSubmission(int id)
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CreateSubmission(string link, string comment)
        {
            var student = _studentService.GetStudentByEmail((string)TempData["Email"]);
   
            try
            {
                Submission newSubmission = new Submission
                {
                    LaboratoryId = (int)TempData["LaboratoryId"],
                    StudentId = student.StudentId,
                    Link = link,
                    Comment = comment
                };

                await _submissionService.CreateSubmission(newSubmission);
                return RedirectToAction("ShowSubjects", "Student");
            }
            catch
            {
                return RedirectToAction("CreateSubmission", "Student");
            }
        }

    }
}
