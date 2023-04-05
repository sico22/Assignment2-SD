using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Assignment2.DAL.Models;

namespace Assignment2.DAL.Repositories.Contracts
{
    public interface IGenericRepository<TModel> where TModel : class
    {
        Task<List<TModel>> GetAllElements();
        Teacher GetTeacherByEmail(string email);
        Student GetStudentByEmail(string email);
        Task<Student> SignUpStudent(Student newStudent);
        Task<Student> GetStudentById(int id);
        Task UpdateStudent(Student student);
        Task DeleteStudent(int studentId);
        Task<Token> CreateToken(Token newToken);
        Token GetTokenByString(string tokenString);
        Task DeleteToken(int tokenId);
        Task<Subject> GetSubjectById(int id);
        Task<Subject> CreateSubject(Subject newSubject);
        Task UpdateSubject(Subject subject);
        Task DeleteSubject(int subjectId);
        Task<Laboratory> GetLaboratoryById(int id);
        Task<Laboratory> CreateLaboratory(Laboratory newLaboratory);
        Task UpdateLaboratory(Laboratory labooratory);
        Task DeleteLaboratory(int laboratoryId);
        Task<Attendance> GetAttendanceById(int id);
        Task<Attendance> CreateAttendance(Attendance newAttendance);
        Task UpdateAttendance(Attendance attendance);
        Task DeleteAttendance(int attendanceId);
        Task<Submission> CreateSubmission(Submission newSubmission);
        Task UpdateSubmission(Submission submission);
        Task<Submission> GetSubmissionById(int id);

    }
}
