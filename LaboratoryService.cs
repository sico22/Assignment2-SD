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
    public class LaboratoryService : ILaboratoryService
    {
        private readonly IGenericRepository<Laboratory> _repository;

        public LaboratoryService(IGenericRepository<Laboratory> repository)
        {
            _repository = repository;
        }

        public async Task<Laboratory> CreateLaboratory(Laboratory newLaboratory)
        {
            try
            {
                return await _repository.CreateLaboratory(newLaboratory);
            }
            catch
            {
                throw;
            }

        }

        public async Task<List<Laboratory>> GetAllElements(int subjectId)
        {
            try
            {
                List<Laboratory> elements = await _repository.GetAllElements();
                elements = elements.Where(lab => lab.SubjectId == subjectId).ToList();
                return elements;
            }
            catch
            {
                throw;
            }
        }

        public async Task<Laboratory> GetLaboratoryById(int id)
        {
            return await _repository.GetLaboratoryById(id);
        }

        public async Task DeleteLaboratory(int laboratoryId)
        {
            await _repository.DeleteLaboratory(laboratoryId);
        }

        public async Task EditLaboratory(int laboratoryId, int subjectId, int number = null, DateTime date = null, string title, string curricula, string description, string assignmentName, DateTime assignmentDl, string assignmentDescription)
        {
            var laboratory = await _repository.GetLaboratoryById(laboratoryId);

            if (laboratory == null)
            {
                throw new ArgumentException("Laboratory not found");
            }

            if(laboratoryId != null && subjectId != null && number != null && date != null && title != null && curricula != null && description != null)
            {
                laboratory.LaboratoryId = laboratoryId;
                laboratory.SubjectId = subjectId;
                laboratory.Number = number;
                laboratory.Date = date;
                laboratory.Title = title;
                laboratory.Description = description;
            }
            
            if(assignmentName != null && assignmentDl != null && assignmentDescription != null)
            {
                laboratory.AssignmentName = assignmentName;
                laboratory.AssignmentDl = assignmentDl;
                laboratory.AssignmentDescription = assignmentDescription;
            }

            await _repository.UpdateLaboratory(laboratory);
        }

        public async Task<List<Laboratory>> GetAllElements()
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
