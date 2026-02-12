using ZakovskaApp.Data;
using ZakovskaApp.Service;
using ZakovskaApp.View;

namespace ZakovskaApp.Controller
{
    public class ZakovskaController
    {
        private readonly MainForm _view;
        private readonly SchoolService _service;

        public ZakovskaController(MainForm view, SchoolService service)
        {
            _view = view;
            _service = service;

          
            _view.SetDataSource(_service.GetAllStudents());

            _view.onAddStudent += OnAddStudent;
            _view.onDeleteStudent += OnDeleteStudent;

            _view.onAddGrade += OnAddGrade;
            _view.onDeleteGrade += OnDeleteGrade; 
            _view.onSave += OnSave;
            _view.onLoad += OnLoad;
            _view.onGenerateData += OnGenerateData;
        }

        public void Show() => Application.Run(_view);

        private void OnAddStudent(object sender, EventArgs e)
        {
           
            var novyStudent = _service.CreateStudent("Nový", "Žák");

            _view.SelectStudent(novyStudent);

            
            _view.FocusNameInput();
        }

        private void OnDeleteStudent(object sender, EventArgs e)
        {
            var student = _view.CurrentStudent;
            if (student == null) return;

            if (_view.ShowConfirmation($"Opravdu chcete smazat studenta {student.FullName}?"))
            {
                _service.DeleteStudent(student);
            }
        }

        private void OnAddGrade(object sender, EventArgs e)
        {
            var student = _view.CurrentStudent;
            if (student == null) return;

           
            _service.GradeStudent(student, "Matematika", 1);
        }

        private void OnDeleteGrade(object sender, EventArgs e)
        {
            var student = _view.CurrentStudent;
            var grade = _view.CurrentGrade;

            if (student != null && grade != null)
            {
                if (_view.ShowConfirmation($"Opravdu chcete smazat známku {grade}?"))
                {
                    _service.DeleteGrade(student, grade);
                }
            }
        }

        

        private void OnGenerateData(object sender, EventArgs e)
        {
            var rand = new Random();
            var s = _service.CreateStudent("Jan", "Novák");
            _service.GradeStudent(s, "Čeština", rand.Next(1, 5));
            _service.GradeStudent(s, "Dějepis", rand.Next(1, 5));
        }

        private void OnSave(object sender, EventArgs e)
        {
            string path = _view.GetSaveFileName();
            if (!string.IsNullOrEmpty(path))
            {
                try
                {
                    _service.SaveData(path);
                    _view.ShowInfo("Data byla úspěšně uložena.");
                }
                catch (Exception ex)
                {
                    _view.ShowError($"Chyba při ukládání: {ex.Message}");
                }
            }
        }

        private void OnLoad(object sender, EventArgs e)
        {
     
            string path = _view.GetOpenFileName();

            
            if (string.IsNullOrEmpty(path)) return;

            try
            {
               
                _service.LoadData(path);

               
                _view.ShowInfo("Data byla úspěšně načtena.");
            }
            catch (Exception ex)
            {
             
                _view.ShowError($"Chyba při načítání souboru:\n{ex.Message}");
            }
        }
    }
}