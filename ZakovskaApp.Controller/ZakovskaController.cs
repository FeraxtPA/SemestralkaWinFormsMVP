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

    // Prezentacni vrstva - Controller obsluhujici udalosti z View a vola sluzby ze Service(Model)
    public class ZakovskaController
    {
        private  MainForm _view;
        private SchoolService _service;

        public ZakovskaController(MainForm view, SchoolService service)
        {
            _view = view;
            _service = service;


            // Observer Pattern - sleduje udalosti z View
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


        // Handlery udalosti
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
            Student student = _view.GetSelectedStudent();
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

            UpdateSelect(student.Id);


        }


        // Pomocna metoda pro aktualizaci vybraneho studenta v gridu
        public void UpdateSelect(int id)
        {
            _view.SelectStudent(id);
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

            // Potvrzeni smazani
            var result = MessageBox.Show(
               $"Opravdu chcete smazat známku {selectedGrade.subject} {selectedGrade.value}?",
               "Potvrzení smazání",
               MessageBoxButtons.YesNo,
               MessageBoxIcon.Warning
           );

            if (result != DialogResult.Yes)
            {
                return;
            }

            _service.DeleteGrade(student.Id, selectedGrade);
           
            RefreshGrid();
            
            UpdateSelect(student.Id);
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

         

            RefreshGrid();
            
            UpdateSelect(student.Id);
        }
        public void OnDeleteStudent(object sender, EventArgs e)
        {
            var student = _view.GetSelectedStudent();
            if (student == null)
            {
                return;
            }


            // Potvrzeni smazani
            var result = MessageBox.Show(
                $"Opravdu chcete smazat studenta {student.FullName}?",
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

          

            if (string.IsNullOrWhiteSpace(_view.GetSubject()))
            {
                MessageBox.Show("Předmět nesmí být prázdný!",
                        "Chyba zadání",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                return;
            }
            _service.GradeStudent(student.Id, _view.GetSubject(), _view.GetGradeValue());

         
            RefreshGrid();

            UpdateSelect(student.Id);

            _view.ResetGradeInputs();
        }

        private void OnStudentSelected(object sender, EventArgs e)
        {
            Student student = _view.GetSelectedStudent();
            _view.ShowStudentDetails(student);
        }


        // Pomocna metoda pro obnovu dat v gridu po každé změně
        private void RefreshGrid()
        {
            List<Student> list = _service.GetAllStudents();
            _view.DisplayStudents(list);
        }

        // Ulozeni a nacteni dat do souboru json
        private void OnSave(object sender, EventArgs e)
        {
          
            string path = _view.GetSaveFileName();

           
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
           
            string path = _view.GetOpenFileName();

          
            if (!string.IsNullOrEmpty(path))
            {
                try
                {
                    _service.LoadData(path);
                    RefreshGrid(); 

                
                    _view.ShowStudentDetails(null);
                    MessageBox.Show("Data byla načtena.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Chyba při načítání: {ex.Message}");
                }
            }
        }

        // Vygeneruje náhodného studenta se známkami
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
