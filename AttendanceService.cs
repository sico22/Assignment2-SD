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
    public class AttendanceService : IAttendanceService
    {
        private readonly IGenericRepository<Attendance> _repository;

        public AttendanceService(IGenericRepository<Attendance> repository)
        {
            _repository = repository;
        }

        public async Task<Attendance> CreateAttendance(Attendance newAttendance)
        {
            try
            {
                return await _repository.CreateAttendance(newAttendance);
            }
            catch
            {
                throw;
            }

        }

        public async Task<List<Attendance>> GetAllElements(int laboratoryId)
        {
            try
            {
                List<Attendance> elements = await _repository.GetAllElements();
                elements = elements.Where(lab => lab.LaboratoryId == laboratoryId).ToList();
                return elements;
            }
            catch
            {
                throw;
            }
        }

        public async Task<Attendance> GetAttendanceById(int id)
        {
            return await _repository.GetAttendanceById(id);
        }

        public async Task DeleteAttendance(int attendanceId)
        {
            await _repository.DeleteAttendance(attendanceId);
        }

        public async Task EditAttendance(int attendanceId, int laboratoryId, int studentId, int attended)
        {
            var attendance = await _repository.GetAttendanceById(attendanceId);

            if (attendance == null)
            {
                throw new ArgumentException("Attendance not found");
            }

            attendance.AttendanceId = attendanceId;
            attendance.LaboratoryId = laboratoryId;
            attendance.StudentId = studentId;
            attendance.Attended = attended;

            await _repository.UpdateAttendance(attendance);
        }

        public async Task<List<Attendance>> GetAllElements()
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
