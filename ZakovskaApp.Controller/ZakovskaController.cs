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

            _view.onSave += OnSave;
            _view.onLoad += OnLoad;

            _view.onGenerateData += GenerateTestData;

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

        private void OnSave(object sender, EventArgs e)
        {
            // 1. Zeptáme se View na cestu k souboru
            string path = _view.GetSaveFileName();

            // 2. Pokud uživatel vybral soubor, uložíme data přes Service
            if (!string.IsNullOrEmpty(path))
            {
                try
                {
                    _service.SaveData(path);
                    MessageBox.Show("Data byla úspěšně uložena.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Chyba při ukládání: {ex.Message}");
                }
            }
        }

        private void OnLoad(object sender, EventArgs e)
        {
            // 1. Zeptáme se View na cestu k souboru
            string path = _view.GetOpenFileName();

            // 2. Pokud uživatel vybral soubor, načteme data přes Service a obnovíme tabulku
            if (!string.IsNullOrEmpty(path))
            {
                try
                {
                    _service.LoadData(path);
                    RefreshGrid(); // Důležité: aktualizovat View novými daty

                    // Vyčistit detaily, protože vybraný student se mohl změnit/zmizet
                    _view.ShowStudentDetails(null);
                    MessageBox.Show("Data byla načtena.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Chyba při načítání: {ex.Message}");
                }
            }
        }

        private void GenerateTestData(object sender, EventArgs e)
        {
            
            List<string> firstNames = new List<string> { "Jan", "Petr", "Lukáš", "Max", "Karel" };
            List<string> lastNames = new List<string> { "Novák", "Svoboda", "Dvořák", "Malý", "Procházka" };
            Random rand = new Random();
            string first = firstNames[rand.Next(firstNames.Count)];
            string last = lastNames[rand.Next(lastNames.Count)];    
            _service.CreateStudent(first, last);
            var student = _service.GetAllStudents().Last();
            var subjects = new List<string> { "Matematika", "Agličtina", "Čeština", "Dějepis", "Chemie", "Fyzika" };
            for (int i = 0; i < rand.Next(2,7); i++)
            {
                string subject = subjects[rand.Next(subjects.Count)];
                int grade = rand.Next(1, 6);
                _service.GradeStudent(student.Id, subject, grade);
            }
            RefreshGrid();

        }


    }
}
