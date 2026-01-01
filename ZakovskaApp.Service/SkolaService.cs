using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZakovskaApp.Data;

using System.Text.Json;
using System.IO;


namespace ZakovskaApp.Service
{
    // Aplikační logika - Service (Model v MVP)
    public class SchoolService
    {
        private List<Student> _students;

        public SchoolService()
        {
            _students = new List<Student>();
        }

        public List<Student> GetAllStudents()
        {
            return _students;
        }

        public void CreateStudent(string name, string surname)
        {

            int newId = _students.Any() ? _students.Max(s => s.Id) + 1 : 1;

            Student student = new Student
            {
                Id = newId,
                Name = name,
                Surname = surname
                
            };
            _students.Add(student);
        }

        public void GradeStudent(int studentId, string subject, int value)
        {
            Student student = _students.FirstOrDefault(s => s.Id == studentId);
            if (student != null)
            {
                Grade grade = new Grade
                {
                    subject = subject,
                    value = value
                };
                student.Grades.Add(grade);
            }
        }

        public void UpdateStudent(int studentId, string newName, string newSurname)
        {
            Student student = _students.FirstOrDefault(s => s.Id == studentId);
            if (student != null)
            {
                student.Name = newName;
                student.Surname = newSurname;
            }
        }

        public void DeleteStudent(int studentId)
        {
            Student student = _students.FirstOrDefault(s => s.Id == studentId);
            if (student != null)
            {
                _students.Remove(student);
            }
        }

        public void UpdateGrade(int studentId, Grade originalGrade, string newSubject, int newValue)
        {
            Student student = _students.FirstOrDefault(s => s.Id == studentId);
            
            if (student != null)
            {
                Grade grade = student.Grades.FirstOrDefault(z => z == originalGrade);
                if (grade != null)
                {
                    grade.subject = newSubject;
                    grade.value = newValue;
                }
            }
        }

        public void DeleteGrade(int studentId, Grade gradeToDelete)
        {
            Student student = _students.FirstOrDefault(s => s.Id == studentId);
            if (student != null)
            {
                student.Grades.Remove(gradeToDelete);
            }
        }

        public void SaveData(string filePath)
        {
            string json = JsonSerializer.Serialize(_students, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, json);
        }

        public void LoadData(string filePath)
        {
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                _students = JsonSerializer.Deserialize<List<Student>>(json) ?? new List<Student>();
            }
        }
    }
}
