using System.ComponentModel;
using ZakovskaApp.Data;

namespace ZakovskaApp.View
{
    public partial class MainForm : Form
    {
       
        private BindingSource _bsStudents = new BindingSource();
        private BindingSource _bsGrades = new BindingSource();

        
        public event EventHandler onAddStudent;
        public event EventHandler onAddGrade;
        public event EventHandler onSave;
        public event EventHandler onLoad;
        public event EventHandler onGenerateData;
        public event EventHandler onDeleteStudent;
        public event EventHandler onDeleteGrade;

        public MainForm()
        {
            InitializeComponent();
            SetupBinding();
            AttachEvents();
        }

        private void SetupBinding()
        {
           
            _bsStudents.DataSource = typeof(Student);

           
            gridStudents.DataSource = _bsStudents;

            
            _bsGrades.DataSource = _bsStudents;
            _bsGrades.DataMember = "Grades";

           
            lstDetail.DataSource = _bsGrades;
            lstDetail.DisplayMember = "DisplayInfo"; 

           
            txtJmeno.DataBindings.Add("Text", _bsStudents, "Name", true, DataSourceUpdateMode.OnPropertyChanged);
            txtPrijmeni.DataBindings.Add("Text", _bsStudents, "Surname", true, DataSourceUpdateMode.OnPropertyChanged);

            
            numZnamka.DataBindings.Add("Value", _bsGrades, "value", true, DataSourceUpdateMode.OnPropertyChanged);
            cmbPredmet.DataBindings.Add("Text", _bsGrades, "subject", true, DataSourceUpdateMode.OnPropertyChanged);
        }

        private void AttachEvents()
        {
            
            btnAddStudent.Click += (s, e) => onAddStudent?.Invoke(this, EventArgs.Empty);
            btnDelete.Click += (s, e) => onDeleteStudent?.Invoke(this, EventArgs.Empty);

            btnAddGrade.Click += (s, e) => onAddGrade?.Invoke(this, EventArgs.Empty);
          
            btnDeleteGrade.Click += (s, e) => onDeleteGrade?.Invoke(this, EventArgs.Empty); 

            // Kontextové menu pro mazání známky
            var contextMenu = new ContextMenuStrip();
            contextMenu.Items.Add("Smazat známku", null, (s, e) => onDeleteGrade?.Invoke(this, EventArgs.Empty));
            lstDetail.ContextMenuStrip = contextMenu;

            btnSave.Click += (s, e) => onSave?.Invoke(this, EventArgs.Empty);
            btnLoad.Click += (s, e) => onLoad?.Invoke(this, EventArgs.Empty);
            btnGen.Click += (s, e) => onGenerateData?.Invoke(this, EventArgs.Empty);
        }

        

        
        public void SetDataSource(BindingList<Student> students)
        {
            _bsStudents.DataSource = students;
        }

       
        public Student CurrentStudent => _bsStudents.Current as Student;

        
        public Grade CurrentGrade => _bsGrades.Current as Grade;

        
        public string GetSaveFileName()
        {
            using (var sfd = new SaveFileDialog { Filter = "JSON|*.json" })
                return sfd.ShowDialog() == DialogResult.OK ? sfd.FileName : null;
        }

        public string GetOpenFileName()
        {
            using (var ofd = new OpenFileDialog { Filter = "JSON|*.json" })
                return ofd.ShowDialog() == DialogResult.OK ? ofd.FileName : null;
        }

        
        public void SelectStudent(Student student)
        {
            if (student != null)
            {
                int index = _bsStudents.IndexOf(student);
                if (index >= 0)
                {
                    _bsStudents.Position = index; 
                }
            }
        }

        public void FocusNameInput()
        {
            txtJmeno.Focus();
            txtJmeno.SelectAll(); // Označí text, aby šel rovnou přepsat
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
        }
    }
}