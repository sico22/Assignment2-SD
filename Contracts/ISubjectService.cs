using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Assignment2.DAL.Models;

namespace Assignment2.BLL.Services.Contracts
{
    public interface ISubjectService
    {
        Task<Subject> CreateSubject(Subject newSubject);
        Task<List<Subject>> GetAllElements();
        Task<Subject> GetSubjectById(int id);
        Task DeleteSubject(int subjectId);
        Task EditSubject(int id, int teacherId, string titles);
    }
}
