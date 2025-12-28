using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZakovskaApp.Data
{
    public class Znamka
    {
        public int hodnota { get; set; }
        public string predmet { get; set; }

        public DateTime datum { get; set; } = DateTime.Now;

        public override string ToString()
        {
            return $"{predmet}: {hodnota} ({datum.ToShortDateString()})";
        }

    }
}
