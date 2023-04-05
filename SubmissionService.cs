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
    public class SubmissionService : ISubmissionService
    {
        private readonly IGenericRepository<Submission> _repository;

        public SubmissionService(IGenericRepository<Submission> repository)
        {
            _repository = repository;
        }

        public async Task<Submission> CreateSubmission(Submission newSubmission)
        {
            try
            {
                return await _repository.CreateSubmission(newSubmission);
            }
            catch
            {
                throw;
            }

        }

        public async Task<Submission> GetSubmissionById(int id)
        {
            return await _repository.GetSubmissionById(id);
        }

        public async Task GradeSubmission(int id, int grade)
        {
            var submission = await _repository.GetSubmissionById(id);

            if (submission == null)
            {
                throw new ArgumentException("Submission not found");
            }

            submission.Grade = grade;

            await _repository.UpdateSubmission(submission);
        }

        public async Task<List<Submission>> GetAllElements(int studentId)
        {
            try
            {
                List<Submission> elements = await _repository.GetAllElements();
                elements = elements.Where(lab => lab.StudentId == studentId).ToList();
                return elements;
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<Submission>> GetAllAssignments(int laboratoryId)
        {
            
            try
            {
                List<Submission> elements = await _repository.GetAllElements();
                Console.WriteLine(elements.Count);
                elements = elements.Where(lab => lab.LaboratoryId == laboratoryId).ToList();
                return elements;
            }
            catch
            {
                throw;
            }
        }

    }
}
