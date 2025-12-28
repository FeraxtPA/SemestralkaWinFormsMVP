using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZakovskaApp.Service;
using ZakovskaApp.Data;
using ZakovskaApp.View;

namespace ZakovskaApp.Controller
{
   
    public class ZakovskaController
    {
        private  MainForm _view;
        private SkolaService _service;

        public ZakovskaController(MainForm view, SkolaService service)
        {
            _view = view;
            _service = service;

            _view.onAddStudent +=  OnAddStudent;
            _view.onAddGrade += OnAddGrade;
            _view.onStudentSelected += OnStudentSelected;

            RefreshGrid();
        }

        public void Show()
        {
            Application.Run(_view);
        }

        private void OnAddStudent(object sender, EventArgs e)
        {
            string first = _view.GetFirstName();
            string last = _view.GetLastName();

            if (string.IsNullOrWhiteSpace(first) || string.IsNullOrWhiteSpace(last))
            { 
                return;
            }

            _service.CreateStudent(first, last);
            _view.ClearInput();
            RefreshGrid();
        }

        private void OnAddGrade(object sender, EventArgs e)
        {
            var student = _view.GetSelectedStudent();
            if (student == null)
            {
           
                return;
            }

            _service.GradeStudent(student.Id, _view.GetSubject(), _view.GetGradeValue());

            // Aktualizujeme tabulku (kvůli průměru) i detail známek
            RefreshGrid();
            _view.ShowStudentDetails(student);
        }

        private void OnStudentSelected(object sender, EventArgs e)
        {
            var student = _view.GetSelectedStudent();
            _view.ShowStudentDetails(student);
        }

        private void RefreshGrid()
        {
            var list = _service.GetAllStudents();
            _view.DisplayStudents(list);
        }


    }
}
