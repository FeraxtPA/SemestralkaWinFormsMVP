using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZakovskaApp.Data;
namespace ZakovskaApp.View
{
    public partial class MainForm : Form
    {
        public event EventHandler onAddStudent;
        public event EventHandler onAddGrade;
        public event EventHandler onStudentSelected;
        public MainForm()
        {
            InitializeComponent();

            btnAddStudent.Click += (s, e) => onAddStudent?.Invoke(this, EventArgs.Empty);
            btnAddGrade.Click += (s, e) => onAddGrade?.Invoke(this, EventArgs.Empty);
            gridStudents.SelectionChanged += (s, e) => onStudentSelected?.Invoke(this, EventArgs.Empty);
        }

        public string GetFirstName()
        {
            return txtJmeno.Text;
        }

        public string GetLastName()
        {
            return txtPrijmeni.Text;
        }

        public string GetSubject()
        {
            return cmbPredmet.Text;
        }

        public int GetGradeValue()
        {
            return (int)numZnamka.Value;
        }

        public Student GetSelectedStudent()
        {
            if (gridStudents.CurrentRow?.DataBoundItem is Student s) return s;
            return null;
        }

        public void DisplayStudents(List<Student> studenti)
        {

            var bindingSource = new BindingSource();
            bindingSource.DataSource = studenti;
            gridStudents.DataSource = bindingSource;


            if (gridStudents.Columns["Id"] != null) gridStudents.Columns["Id"].Visible = false;
        }

        public void ShowStudentDetails(Student student)
        {
            lstDetail.Items.Clear();
            if (student != null)
            {
                foreach (var g in student.Znamky)
                {
                    lstDetail.Items.Add(g.ToString());
                }
            }
        }

        public void ClearInput()
        {
            txtJmeno.Text = "";
            txtPrijmeni.Text = "";
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }
    }
}
