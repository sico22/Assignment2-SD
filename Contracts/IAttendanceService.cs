using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Assignment2.DAL.Models;

namespace Assignment2.BLL.Services.Contracts
{
    public interface IAttendanceService
    {
        Task<Attendance> CreateAttendance(Attendance attendance);
        Task<List<Attendance>> GetAllElements(int laboratoryId);
        Task<Attendance> GetAttendanceById(int id);
        Task DeleteAttendance(int attendanceId);
        Task EditAttendance(int attendanceId, int laboratoryId, int studentId, int attended);
    }
}
