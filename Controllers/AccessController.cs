using Microsoft.AspNetCore.Mvc;

using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Assignment2.PLL.Models;
using Assignment2.BLL.Services.Contracts;
using Assignment2.DAL.Models;

namespace Assignment2.PLL.Controllers
{
    public class AccessController : Controller
    {
        private readonly ITeacherService _teacherService;
        private readonly IStudentService _studentService;
        private readonly ITokenService _tokenService;

        public AccessController(ITeacherService teacherService, IStudentService studentService, ITokenService tokenService)
        {
            _teacherService = teacherService;
            _studentService = studentService;
            _tokenService = tokenService;

        }
        public IActionResult TeacherLogin()
        {
            ClaimsPrincipal claimsUser = HttpContext.User;
            if (claimsUser.Identity.IsAuthenticated)
                return RedirectToAction("ShowStudents", "Teacher");
            return View();
        }

        [HttpPost]
        public async Task <IActionResult> TeacherLogin(VMLogin modelLogin)
        {
            if (_teacherService.SignInTeacher(modelLogin.Email, modelLogin.Password))
            {
                List<Claim> claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier, modelLogin.Email),
                    new Claim("OtherProperties", "Example Role")
                };

                ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                AuthenticationProperties properties = new AuthenticationProperties()
                {
                    AllowRefresh = true,
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(identity), properties);

                TempData["Email"] = modelLogin.Email;
                return RedirectToAction("ShowStudents", "Teacher");
            }

            ViewData["ValidateMessage"] = "user not found";
            return View();
        }

        public IActionResult StudentLogin()
        {
            ClaimsPrincipal claimsUser = HttpContext.User;
            if (claimsUser.Identity.IsAuthenticated)
                return RedirectToAction("ShowSubjects", "StudentController");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> StudentLogin(VMLogin modelLogin)
        {
            if (_studentService.SignInStudent(modelLogin.Email, modelLogin.Password))
            {
                List<Claim> claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier, modelLogin.Email),
                    new Claim("OtherProperties", "Example Role")
                };

                ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                AuthenticationProperties properties = new AuthenticationProperties()
                {
                    AllowRefresh = true,
                    IsPersistent = modelLogin.KeepLoggedIn
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(identity), properties);
                TempData["Email"] = modelLogin.Email;

                return RedirectToAction("ShowSubjects", "Student");
            }

            ViewData["ValidateMessage"] = "user not found";
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> TokenValidation()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> TokenValidation(string tokenString)
        {
            if (_tokenService.UseTokenAsync(tokenString))
            {
                return RedirectToAction("StudentSignUp", "Access");
            }
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> StudentSignUp()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> StudentSignUp(string email, string password, string name, int groupNr, string hobby)
        {
            try
            {
                Student newStudent= new Student
                {
                    Email = email,
                    Password = password,
                    Name = name,
                    GroupNr = groupNr,
                    Hobby = hobby
                };


                await _studentService.SignUpStudent(newStudent);
                StudentLogin(new VMLogin { Email = email, Password = password});
                return RedirectToAction("ShowSubjects", "Student");
                
            }
            catch
            {
                return StatusCode(StatusCodes.Status401Unauthorized, new { message = "Not all fields were completed" });
            }
        }
    }
}
