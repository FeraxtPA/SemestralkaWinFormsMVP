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
    public class SkolaService
    {
        private List<Student> _studenti;

        public SkolaService()
        {
            _studenti = new List<Student>();
        }

        public List<Student> GetAllStudents()
        {
            return _studenti;
        }

        public void CreateStudent(string jmeno, string prijmeni)
        {

            int newId = _studenti.Any() ? _studenti.Max(s => s.Id) + 1 : 1;

            Student student = new Student
            {
                Id = newId,
                Jmeno = jmeno,
                Prijmeni = prijmeni
                
            };
            _studenti.Add(student);
        }

        public void GradeStudent(int studentId, string predmet, int hodnota)
        {
            Student student = _studenti.FirstOrDefault(s => s.Id == studentId);
            if (student != null)
            {
                Znamka znamka = new Znamka
                {
                    predmet = predmet,
                    hodnota = hodnota
                };
                student.Znamky.Add(znamka);
            }
        }

        public void UpdateStudent(int studentId, string newJmeno, string newPrijmeni)
        {
            Student student = _studenti.FirstOrDefault(s => s.Id == studentId);
            if (student != null)
            {
                student.Jmeno = newJmeno;
                student.Prijmeni = newPrijmeni;
            }
        }

        public void DeleteStudent(int studentId)
        {
            Student student = _studenti.FirstOrDefault(s => s.Id == studentId);
            if (student != null)
            {
                _studenti.Remove(student);
            }
        }

        public void UpdateGrade(int studentId, Znamka originalGrade, string newPredmet, int newHodnota)
        {
            Student student = _studenti.FirstOrDefault(s => s.Id == studentId);
            
            if (student != null)
            {
                Znamka grade = student.Znamky.FirstOrDefault(z => z == originalGrade);
                if (grade != null)
                {
                    grade.predmet = newPredmet;
                    grade.hodnota = newHodnota;
                }
            }
        }

        public void DeleteGrade(int studentId, Znamka gradeToDelete)
        {
            Student student = _studenti.FirstOrDefault(s => s.Id == studentId);
            if (student != null)
            {
                student.Znamky.Remove(gradeToDelete);
            }
        }

        public void SaveData(string filePath)
        {
            string json = JsonSerializer.Serialize(_studenti, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, json);
        }

        public void LoadData(string filePath)
        {
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                _studenti = JsonSerializer.Deserialize<List<Student>>(json) ?? new List<Student>();
            }
        }
    }
}
