using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Assignment2.DAL.Models;

namespace Assignment2.BLL.Services.Contracts
{
    public interface ISubmissionService
    {
        Task<Submission> CreateSubmission(Submission newSubmission);
        Task<List<Submission>> GetAllElements(int studentId);
        Task<List<Submission>> GetAllAssignments(int laboratoryId);
        Task GradeSubmission(int id, int grade);
        Task<Submission> GetSubmissionById(int id);


    }
}
