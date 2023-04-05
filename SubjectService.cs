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
    public class SubjectService : ISubjectService
    {
        private readonly IGenericRepository<Subject> _repository;

        public SubjectService(IGenericRepository<Subject> repository)
        {
            _repository = repository;
        }

        public async Task<Subject> CreateSubject(Subject newSubject)
        {
            try
            {
                return await _repository.CreateSubject(newSubject);
            }
            catch
            {
                throw;
            }

        }

        public async Task<Subject> GetSubjectById(int id)
        {
            return await _repository.GetSubjectById(id);
        }

        public async Task DeleteSubject(int subjectId)
        {
            await _repository.DeleteSubject(subjectId);
        }

        public async Task EditSubject(int id, int teacherId, string title)
        {
            var subject = await _repository.GetSubjectById(id);

            if (subject == null)
            {
                throw new ArgumentException("Student not found");
            }

            subject.SubjectId = id;
            subject.TeacherId = teacherId;
            subject.Title = title;

            await _repository.UpdateSubject(subject);
        }

        public async Task<List<Subject>> GetAllElements()
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
    }

}
