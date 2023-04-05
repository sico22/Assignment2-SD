using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Assignment2.DAL.Models;

namespace Assignment2.BLL.Services.Contracts
{
    public interface IStudentService
    {
        bool SignInStudent(string email, string password);
        Task<Student> SignUpStudent(Student newStudent);
        Task<List<Student>> GetAllElements();
        Task<Student> GetStudentById(int id);
        Task DeleteStudent(int studentId);
        Task EditStudent(int id, string email, string password, string name, int groupNr, string hobby);
        Student GetStudentByEmail(string email);
    }
}
