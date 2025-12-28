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

        public event EventHandler onSave;
        public event EventHandler onLoad;

        public event EventHandler onGenerateData;

        public MainForm()
        {
            InitializeComponent();

            btnAddStudent.Click += (s, e) => onAddStudent?.Invoke(this, EventArgs.Empty);
            btnAddGrade.Click += (s, e) => onAddGrade?.Invoke(this, EventArgs.Empty);
            gridStudents.SelectionChanged += (s, e) => onStudentSelected?.Invoke(this, EventArgs.Empty);

            btnSave.Click += (s, e) => onSave?.Invoke(this, EventArgs.Empty);
            btnLoad.Click += (s, e) => onLoad?.Invoke(this, EventArgs.Empty);
            btnGen.Click += (s, e) => onGenerateData?.Invoke(this, EventArgs.Empty);
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

        public string GetSaveFileName()
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "JSON soubory (*.json)|*.json|Všechny soubory (*.*)|*.*";
                sfd.DefaultExt = "json";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    return sfd.FileName;
                }
            }
            return string.Empty;
        }

        public string GetOpenFileName()
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "JSON soubory (*.json)|*.json|Všechny soubory (*.*)|*.*";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    return ofd.FileName;
                }
            }
            return string.Empty;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }
    }
}
