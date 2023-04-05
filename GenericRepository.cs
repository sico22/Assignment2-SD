using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Assignment2.DAL.DataContext;
using Assignment2.DAL.Models;
using Assignment2.DAL.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Assignment2.DAL.Repositories
{
    public class GenericRepository<TModel> : IGenericRepository<TModel> where TModel : class
    {
        private readonly Assignment2Context _dbContext;

        public GenericRepository(Assignment2Context dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<TModel>> GetAllElements()
        {
            try
            {
                return await _dbContext.Set<TModel>().ToListAsync();
            }
            catch
            {
                throw;
            }
        }

        public Teacher GetTeacherByEmail(string email)
        {
            return _dbContext.Teachers.FirstOrDefault(u => u.Email == email);
        }

        public Student GetStudentByEmail(string email)
        {
            return _dbContext.Students.FirstOrDefault(u => u.Email == email);
        }

        public async Task<Student> SignUpStudent(Student newStudent)
        {
            await _dbContext.Students.AddAsync(newStudent);
            await _dbContext.SaveChangesAsync();
            return newStudent;
        }

        public async Task UpdateStudent(Student student)
        {
            _dbContext.Students.Update(student);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteStudent(int studentId)
        {
            var student = await _dbContext.Students.FindAsync(studentId);

            if (student == null)
            {
                throw new ArgumentException($"Performance with id {studentId} does not exist.");
            }

            _dbContext.Students.Remove(student);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Student> GetStudentById(int id)
        {
            return await _dbContext.Students.FindAsync(id);
        }

        public async Task<Token> CreateToken(Token newToken)
        {
            await _dbContext.Tokens.AddAsync(newToken);
            await _dbContext.SaveChangesAsync();
            return newToken;
        }

        public Token GetTokenByString(string tokenString)
        {
            return _dbContext.Tokens.FirstOrDefault(u => u.TokenString == tokenString);
        }

        public async Task DeleteToken(int tokenId)
        {
            var token = await _dbContext.Tokens.FindAsync(tokenId);

            if (token == null)
            {
                throw new ArgumentException($"Performance with id {tokenId} does not exist.");
            }

            _dbContext.Tokens.Remove(token);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Subject> CreateSubject(Subject newSubject)
        {
            await _dbContext.Subjects.AddAsync(newSubject);
            await _dbContext.SaveChangesAsync();
            return newSubject;
        }

        public async Task UpdateSubject(Subject subject)
        {
            _dbContext.Subjects.Update(subject);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteSubject(int subjectId)
        {
            var subject = await _dbContext.Subjects.FindAsync(subjectId);

            if (subject == null)
            {
                throw new ArgumentException($"Performance with id {subjectId} does not exist.");
            }

            _dbContext.Subjects.Remove(subject);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Subject> GetSubjectById(int id)
        {
            return await _dbContext.Subjects.FindAsync(id);
        }

        public async Task<Laboratory> CreateLaboratory(Laboratory newLaboratory)
        {
            await _dbContext.Laboratories.AddAsync(newLaboratory);
            await _dbContext.SaveChangesAsync();
            return newLaboratory;
        }

        public async Task UpdateLaboratory(Laboratory laboratory)
        {
            _dbContext.Laboratories.Update(laboratory);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteLaboratory(int laboratoryId)
        {
            var laboratory = await _dbContext.Laboratories.FindAsync(laboratoryId);

            if (laboratory == null)
            {
                throw new ArgumentException($"Performance with id {laboratoryId} does not exist.");
            }

            _dbContext.Laboratories.Remove(laboratory);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Laboratory> GetLaboratoryById(int id)
        {
            return await _dbContext.Laboratories.FindAsync(id);
        }

        public async Task<Attendance> CreateAttendance(Attendance newAttendance)
        {
            await _dbContext.Attendances.AddAsync(newAttendance);
            await _dbContext.SaveChangesAsync();
            return newAttendance;
        }

        public async Task UpdateAttendance(Attendance attendance)
        {
            _dbContext.Attendances.Update(attendance);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAttendance(int attendanceId)
        {
            var attendance = await _dbContext.Attendances.FindAsync(attendanceId);

            if (attendance == null)
            {
                throw new ArgumentException($"Attendance with id {attendanceId} does not exist.");
            }

            _dbContext.Attendances.Remove(attendance);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Attendance> GetAttendanceById(int id)
        {
            return await _dbContext.Attendances.FindAsync(id);
        }

        public async Task<Submission> CreateSubmission(Submission newSubmission)
        {
            await _dbContext.Submissions.AddAsync(newSubmission);
            await _dbContext.SaveChangesAsync();
            return newSubmission;
        }

        public async Task UpdateSubmission(Submission submission)
        {
            _dbContext.Submissions.Update(submission);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Submission> GetSubmissionById(int id)
        {
            return await _dbContext.Submissions.FindAsync(id);
        }
    }
}
