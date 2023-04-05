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
    public class TeacherService : ITeacherService
    {
        private readonly IGenericRepository<Teacher> _repository;

        public TeacherService(IGenericRepository<Teacher> repository)
        {
            _repository = repository;
        }


        public bool SignInTeacher(string email, string password)
        {
            var user = _repository.GetTeacherByEmail(email);

            if (user == null)
            {
                return false;
            }

            if (password != user.Password)
            {
                return false;
            }

            return true;
        }

        public Teacher GetTeacherByEmail(string email)
        {
            return _repository.GetTeacherByEmail(email);
        }



    }
}
