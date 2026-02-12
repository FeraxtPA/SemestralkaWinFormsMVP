using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ZakovskaApp.Data
{
    public class Grade : INotifyPropertyChanged
    {
        private int _value;
        private string _subject;
        private DateTime _date = DateTime.Now;

        public event PropertyChangedEventHandler PropertyChanged;

        public int value
        {
            get => _value;
            set { _value = value; OnPropertyChanged(); OnPropertyChanged(nameof(DisplayInfo)); }
        }

        public string subject
        {
            get => _subject;
            set { _subject = value; OnPropertyChanged(); OnPropertyChanged(nameof(DisplayInfo)); }
        }

        public DateTime date
        {
            get => _date;
            set { _date = value; OnPropertyChanged(); OnPropertyChanged(nameof(DisplayInfo)); }
        }

       
        public string DisplayInfo => $"{subject}: {value} ({date.ToShortDateString()})";

        public override string ToString() => DisplayInfo;

        protected void OnPropertyChanged([CallerMemberName] string propName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}