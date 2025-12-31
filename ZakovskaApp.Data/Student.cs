using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZakovskaApp.Data
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public List<Grade> Grades { get; set; } = new List<Grade>();

        public string FullName => $"{Name} {Surname}";

        public double Average
        {
            get
            {
                if (Grades.Count == 0) return 0;
                return Grades.Average(z => z.value);
            }
        }
    }
}
