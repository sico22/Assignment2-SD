using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Assignment2.DAL.Models;
using Assignment2.DAL.Repositories.Contracts;
using Assignment2.BLL.Services.Contracts;

namespace Assignment2.BLL.Services
{
    public class StudentService : IStudentService
    {
        private readonly IGenericRepository<Student> _repository;

        public StudentService(IGenericRepository<Student> repository)
        {
            _repository = repository;
        }

        public bool SignInStudent(string email, string password)
        {
            var user = _repository.GetStudentByEmail(email);

            if (user == null)
            {
                return false;
            }

            Console.WriteLine(EncodeToBase64(password));
            Console.WriteLine(user.Password);

            if (EncodeToBase64(password) != user.Password)
            {
                return false;
            }

            return true;
        }

        public async Task<Student> SignUpStudent(Student newStudent)
        {
            try
            {
                newStudent.Password = EncodeToBase64(newStudent.Password);
                return await _repository.SignUpStudent(newStudent);
            }
            catch
            {
                throw;
            }
        }

        public async Task<Student> GetStudentById(int id)
        {
            return await _repository.GetStudentById(id);
        }

        public async Task DeleteStudent(int studentId)
        {
            await _repository.DeleteStudent(studentId);
        }

        public async Task EditStudent(int id, string email, string password, string name, int groupNr, string hobby)
        {
            var student = await _repository.GetStudentById(id);

            if (student == null)
            {
                throw new ArgumentException("Student not found");
            }

            student.StudentId = id;
            student.Email = email;
            student.Password = password;
            student.GroupNr = groupNr;
            student.Hobby = hobby;

            await _repository.UpdateStudent(student);
        }

        public async Task<List<Student>> GetAllElements()
        {
            try
            {
                return await _repository.GetAllElements();
            }
            catch
            {
                throw;
            }
        }
        public Student GetStudentByEmail(string email)
        {
            return _repository.GetStudentByEmail(email);
        }


        public static string EncodeToBase64(string plainText)
        {
            byte[] plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
    }

}
