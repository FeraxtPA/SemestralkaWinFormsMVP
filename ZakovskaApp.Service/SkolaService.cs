using System.ComponentModel;
using System.Text.Json;
using ZakovskaApp.Data;

namespace ZakovskaApp.Service
{
    public class SchoolService
    {
        
        private BindingList<Student> _students;

        public SchoolService()
        {
            _students = new BindingList<Student>();
        }

        public BindingList<Student> GetAllStudents()
        {
            return _students;
        }

        
        public Student CreateStudent(string name, string surname)
        {
            int newId = _students.Any() ? _students.Max(s => s.Id) + 1 : 1;
            Student student = new Student { Id = newId, Name = name, Surname = surname };
            _students.Add(student);
            return student;
        }

        public void GradeStudent(Student student, string subject, int value)
        {
            if (student != null)
            {
                student.Grades.Add(new Grade { subject = subject, value = value });
            }
        }

        public void DeleteStudent(Student student)
        {
            if (student != null) _students.Remove(student);
        }

        public void DeleteGrade(Student student, Grade grade)
        {
            if (student != null && grade != null) student.Grades.Remove(grade);
        }

        public void LoadData(string filePath)
        {
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                var loaded = JsonSerializer.Deserialize<List<Student>>(json);

                _students.Clear();
                if (loaded != null)
                {
                    foreach (var s in loaded) _students.Add(s);
                }
            }
        }

        public void SaveData(string filePath)
        {
            
            string json = JsonSerializer.Serialize(_students, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, json);
        }

       
    }
}