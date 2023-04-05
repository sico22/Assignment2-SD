using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Assignment2.DAL.Models;

namespace Assignment2.BLL.Services.Contracts
{
    public interface ILaboratoryService
    {
        Task<Laboratory> CreateLaboratory(Laboratory laboratory);
        Task<List<Laboratory>> GetAllElements(int subjectId);
        Task<Laboratory> GetLaboratoryById(int id);
        Task DeleteLaboratory(int laboratoryId);
        Task EditLaboratory(int laboratoryId, int subjectId, int number, DateTime date, string title, string curricula, string description, string assignmentName, DateTime assignmentDl, string assignmentDescription);
    }
}
