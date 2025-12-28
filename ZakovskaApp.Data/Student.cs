using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZakovskaApp.Data
{
    public class Student
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Jmeno { get; set; }
        public string Prijmeni { get; set; }

        public List<Znamka> Znamky { get; set; } = new List<Znamka>();

        public string CeleJmeno => $"{Jmeno} {Prijmeni}";

        public double Prumer
        {
            get
            {
                if (Znamky.Count == 0) return 0;
                return Znamky.Average(z => z.hodnota);
            }
        }
    }
}
