using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace ZakovskaApp.Data
{
    public class Student : INotifyPropertyChanged
    {
        private int _id;
        private string _name;
        private string _surname;

        
        public BindingList<Grade> Grades { get; set; } = new BindingList<Grade>();

        public event PropertyChangedEventHandler PropertyChanged;

        public Student()
        {
            
            Grades.ListChanged += (s, e) => OnPropertyChanged(nameof(Average));
        }

        public int Id
        {
            get => _id;
            set { _id = value; OnPropertyChanged(); }
        }

        public string Name
        {
            get => _name;
            set { _name = value; OnPropertyChanged(); OnPropertyChanged(nameof(FullName)); }
        }

        public string Surname
        {
            get => _surname;
            set { _surname = value; OnPropertyChanged(); OnPropertyChanged(nameof(FullName)); }
        }

        public string FullName => $"{Name} {Surname}";

        public double Average
        {
            get
            {
                if (Grades.Count == 0) return 0;
                return Grades.Average(z => z.value);
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string propName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}