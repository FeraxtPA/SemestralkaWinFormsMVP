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
    // Hlavní okno aplikace - View v MVP, obsahuje UI prvky a vyvolává události pro Presenter
    public partial class MainForm : Form
    {

        // Definice událostí pro komunikaci s Presenterem
        public event EventHandler onAddStudent;
        public event EventHandler onAddGrade;
        public event EventHandler onStudentSelected;

        public event EventHandler onSave;
        public event EventHandler onLoad;

        public event EventHandler onGenerateData;

        public event EventHandler onDeleteStudent;
        public event EventHandler onEditStudent;

        public event EventHandler onEditGrade;
        public event EventHandler onDeleteGrade;

        

        public MainForm()
        {
            InitializeComponent();

            // ContextMenu pro right cĺick na známku v listu pro smazání známky
            ContextMenuStrip gradeMenu = new ContextMenuStrip();
            ToolStripMenuItem deleteItem = new ToolStripMenuItem("Smazat známku");

            deleteItem.Click += (s, e) => onDeleteGrade?.Invoke(this, EventArgs.Empty);
            gradeMenu.Items.Add(deleteItem);

            lstDetail.ContextMenuStrip = gradeMenu;

            // Zajištění výběru známky při right clicku pro zobrazení context menu
            lstDetail.MouseDown += (s, e) =>
            {
                if (e.Button == MouseButtons.Right)
                {
                    int index = lstDetail.IndexFromPoint(e.Location);
                    if (index != ListBox.NoMatches)
                    {
                        lstDetail.SelectedIndex = index;
                    }
                }
            };

            // Přiřazení událostí tlačítkům a dalším ovládacím prvkům
            btnAddStudent.Click += (s, e) => onAddStudent?.Invoke(this, EventArgs.Empty);
            btnAddGrade.Click += (s, e) => onAddGrade?.Invoke(this, EventArgs.Empty);
            gridStudents.SelectionChanged += (s, e) => onStudentSelected?.Invoke(this, EventArgs.Empty);

            btnSave.Click += (s, e) => onSave?.Invoke(this, EventArgs.Empty);
            btnLoad.Click += (s, e) => onLoad?.Invoke(this, EventArgs.Empty);
            btnGen.Click += (s, e) => onGenerateData?.Invoke(this, EventArgs.Empty);

            btnDelete.Click += (s, e) => onDeleteStudent?.Invoke(this, EventArgs.Empty);
            btnEdit.Click += (s, e) => onEditStudent?.Invoke(this, EventArgs.Empty);

            btnEditGrade.Click += (s, e) => onEditGrade?.Invoke(this, EventArgs.Empty);

            // Synchronizace výběru známky v listu s poli pro úpravu známky
            lstDetail.SelectedIndexChanged += (s, e) =>
            {
                if (lstDetail.SelectedItem is Grade z)
                {
                    cmbPredmet.Text = z.subject;
                    numZnamka.Value = z.value;
                }

            };
        }



        // Metody pro získání vstupních dat z UI prvků

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

        // Metody pro aktualizaci UI prvků
        public void DisplayStudents(List<Student> studenti)
        {

            var bindingSource = new BindingSource();
            bindingSource.DataSource = new List<Student>(studenti);

            gridStudents.DataSource = bindingSource;


        }

        public void SelectStudent(int studentId)
        {
            gridStudents.ClearSelection();  

            foreach (DataGridViewRow row in gridStudents.Rows)
            {
                if (row.DataBoundItem is Student s && s.Id == studentId)
                {
                    row.Selected = true;
                    gridStudents.CurrentCell = row.Cells[0];
                    break;
                }
            }
        }

        public void ShowStudentDetails(Student student)
        {
            lstDetail.Items.Clear();
            if (student != null)
            {
                txtJmeno.Text = student.Name;
                txtPrijmeni.Text = student.Surname;

                foreach (var g in student.Grades)
                {
                    lstDetail.Items.Add(g);
                }
            }
            else
            {
                txtJmeno.Text = "";
                txtPrijmeni.Text = "";
            }
        }

        public Grade GetSelectedGrade()
        {
            if (lstDetail.SelectedItem is Grade z) return z;
            return null;
        }

        public void ResetGradeInputs()
        {
            cmbPredmet.SelectedIndex = -1;
            numZnamka.Value = 1;           
            cmbPredmet.Focus();          
        }

        public void ClearInput()
        {
            txtJmeno.Text = "";
            txtPrijmeni.Text = "";
        }

        // Metody pro zobrazení dialogů pro uložení a načtení souborů
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
