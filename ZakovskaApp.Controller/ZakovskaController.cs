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

            _view.onDeleteStudent += OnDeleteStudent;
            _view.onEditStudent += OnEditStudent;

            _view.onEditGrade += OnEditGrade;
            _view.onDeleteGrade += OnDeleteGrade;

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
                MessageBox.Show("Jméno a příjmení nesmí být prázdné!",
                        "Chyba zadání",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                return;
            }

            _service.CreateStudent(first, last);
            _view.ClearInput();
            RefreshGrid();
        }

        public void OnEditStudent(object sender, EventArgs e)
        {
            var student = _view.GetSelectedStudent();
            if (student == null)
            {
                MessageBox.Show("Nejdříve vyberte studenta k úpravě.", "Upozornění", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            string newFirst = _view.GetFirstName();
            string newLast = _view.GetLastName();
            if (string.IsNullOrWhiteSpace(newFirst) || string.IsNullOrWhiteSpace(newLast))
            {
                MessageBox.Show("Jméno a příjmení nesmí být prázdné!", "Chyba zadání", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            _service.UpdateStudent(student.Id, newFirst, newLast);
            _view.ClearInput();
            RefreshGrid();

            _view.SelectStudent(student.Id);

            Student updatedStudent = _view.GetSelectedStudent();
            _view.ShowStudentDetails(updatedStudent);


        }

        public void OnDeleteGrade(object sender, EventArgs e)
        {
            var student = _view.GetSelectedStudent();
            if (student == null)
            {
                return;
            }
            var selectedGrade = _view.GetSelectedGrade();
            if (selectedGrade == null)
            {
                return;
            }


            var result = MessageBox.Show(
               $"Opravdu chcete smazat známku {selectedGrade.predmet} {selectedGrade.hodnota}?",
               "Potvrzení smazání",
               MessageBoxButtons.YesNo,
               MessageBoxIcon.Warning
           );

            if (result != DialogResult.Yes)
            {
                return;
            }

            _service.DeleteGrade(student.Id, selectedGrade);
            int studentId = student.Id;
            RefreshGrid();
            _view.SelectStudent(studentId);
            Student updatedStudent = _view.GetSelectedStudent();
            _view.ShowStudentDetails(updatedStudent);
        }

        public void OnEditGrade(object sender, EventArgs e)
        {
           
            var student = _view.GetSelectedStudent();
            if (student == null)
            {
                return;
            }
            var selectedGrade = _view.GetSelectedGrade();
            if (selectedGrade == null)
            {
                return;
            }

            string predmet = _view.GetSubject();
            int hodnota = _view.GetGradeValue();

            _service.UpdateGrade(student.Id, selectedGrade, predmet, hodnota);

            int studentId = student.Id;

            RefreshGrid();
            _view.SelectStudent(studentId);

            Student updatedStudent = _view.GetSelectedStudent();
            _view.ShowStudentDetails(updatedStudent);
        }
        public void OnDeleteStudent(object sender, EventArgs e)
        {
            var student = _view.GetSelectedStudent();
            if (student == null)
            {
                return;
            }

            var result = MessageBox.Show(
                $"Opravdu chcete smazat studenta {student.CeleJmeno}?",
                "Potvrzení smazání",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result != DialogResult.Yes)
            {
                return;
            }
            _service.DeleteStudent(student.Id);
            _view.ClearInput();
            RefreshGrid();
            _view.ShowStudentDetails(null);
        }

        private void OnAddGrade(object sender, EventArgs e)
        {
            var student = _view.GetSelectedStudent();
            if (student == null)
            {
           
                return;
            }

            int studentId = student.Id;

            if (string.IsNullOrWhiteSpace(_view.GetSubject()))
            {
                MessageBox.Show("Předmět nesmí být prázdný!",
                        "Chyba zadání",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                return;
            }
            _service.GradeStudent(student.Id, _view.GetSubject(), _view.GetGradeValue());

            // Aktualizujeme tabulku (kvůli průměru) i detail známek
            RefreshGrid();

            _view.SelectStudent(studentId);

            Student updatedStudent = _view.GetSelectedStudent();
            _view.ShowStudentDetails(updatedStudent);

            _view.ResetGradeInputs();
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
