using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Assignment2.DAL.Models;

namespace Assignment2.BLL.Services.Contracts
{
    public interface ITeacherService
    {
        bool SignInTeacher(string email, string password);
        Teacher GetTeacherByEmail(string email);
    }
}
