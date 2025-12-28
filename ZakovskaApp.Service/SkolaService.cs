using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZakovskaApp.Data;

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
            Student student = new Student
            {
                Jmeno = jmeno,
                Prijmeni = prijmeni
            };
            _studenti.Add(student);
        }

        public void GradeStudent(Guid studentId, string predmet, int hodnota)
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

        public void CreateSampleData()
        {
            Student s1 = new Student { Jmeno = "Jan", Prijmeni = "Novák" };
            s1.Znamky.Add(new Znamka { hodnota = 1, predmet = "Matematika" });
            s1.Znamky.Add(new Znamka { hodnota = 2, predmet = "Čeština" });

            Student s2 = new Student { Jmeno = "Petr", Prijmeni = "Svoboda" };
            s2.Znamky.Add(new Znamka { hodnota = 3, predmet = "Angličtina" });

            _studenti.Add(s1);
            _studenti.Add(s2);
        }
    }
}
