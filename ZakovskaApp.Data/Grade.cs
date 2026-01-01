using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZakovskaApp.Data
{
    // Známka studenta, Model v MVP
    public class Grade
    {
        public int value { get; set; }
        public string subject { get; set; }

        public DateTime date { get; set; } = DateTime.Now;


        // Pro zobrazení v ListBoxu
        public override string ToString()
        {
            return $"{subject}: {value} ({date.ToShortDateString()})";
        }

    }
}
