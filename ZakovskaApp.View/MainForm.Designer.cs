namespace ZakovskaApp.View
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            gridStudents = new DataGridView();
            btnAddStudent = new Button();
            btnAddGrade = new Button();
            txtJmeno = new TextBox();
            txtPrijmeni = new TextBox();
            numZnamka = new NumericUpDown();
            cmbPredmet = new ComboBox();
            lstDetail = new ListBox();
            btnSave = new Button();
            btnLoad = new Button();
            btnGen = new Button();
            ((System.ComponentModel.ISupportInitialize)gridStudents).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numZnamka).BeginInit();
            SuspendLayout();
            // 
            // gridStudents
            // 
            gridStudents.AllowUserToAddRows = false;
            gridStudents.AllowUserToDeleteRows = false;
            gridStudents.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            gridStudents.Location = new Point(12, 12);
            gridStudents.Name = "gridStudents";
            gridStudents.ReadOnly = true;
            gridStudents.RowHeadersWidth = 62;
            gridStudents.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gridStudents.Size = new Size(694, 640);
            gridStudents.TabIndex = 0;
            // 
            // btnAddStudent
            // 
            btnAddStudent.Location = new Point(725, 37);
            btnAddStudent.Name = "btnAddStudent";
            btnAddStudent.Size = new Size(142, 34);
            btnAddStudent.TabIndex = 1;
            btnAddStudent.Text = "Přidat žáka";
            btnAddStudent.UseVisualStyleBackColor = true;
            // 
            // btnAddGrade
            // 
            btnAddGrade.Location = new Point(725, 108);
            btnAddGrade.Name = "btnAddGrade";
            btnAddGrade.Size = new Size(142, 34);
            btnAddGrade.TabIndex = 2;
            btnAddGrade.Text = "Přidat známku";
            btnAddGrade.UseVisualStyleBackColor = true;
            // 
            // txtJmeno
            // 
            txtJmeno.Location = new Point(873, 40);
            txtJmeno.Name = "txtJmeno";
            txtJmeno.Size = new Size(150, 31);
            txtJmeno.TabIndex = 3;
            // 
            // txtPrijmeni
            // 
            txtPrijmeni.Location = new Point(1029, 40);
            txtPrijmeni.Name = "txtPrijmeni";
            txtPrijmeni.Size = new Size(150, 31);
            txtPrijmeni.TabIndex = 4;
            // 
            // numZnamka
            // 
            numZnamka.Location = new Point(873, 111);
            numZnamka.Maximum = new decimal(new int[] { 5, 0, 0, 0 });
            numZnamka.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numZnamka.Name = "numZnamka";
            numZnamka.Size = new Size(180, 31);
            numZnamka.TabIndex = 5;
            numZnamka.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // cmbPredmet
            // 
            cmbPredmet.FormattingEnabled = true;
            cmbPredmet.Items.AddRange(new object[] { "Čeština", "Angličtina", "Matematika", "Dějepis", "Zeměpis", "Fyzika", "Chemie" });
            cmbPredmet.Location = new Point(1059, 111);
            cmbPredmet.Name = "cmbPredmet";
            cmbPredmet.Size = new Size(182, 33);
            cmbPredmet.TabIndex = 6;
            // 
            // lstDetail
            // 
            lstDetail.FormattingEnabled = true;
            lstDetail.ItemHeight = 25;
            lstDetail.Location = new Point(725, 203);
            lstDetail.Name = "lstDetail";
            lstDetail.Size = new Size(328, 429);
            lstDetail.TabIndex = 7;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(1111, 264);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(112, 34);
            btnSave.TabIndex = 8;
            btnSave.Text = "Uložit";
            btnSave.UseVisualStyleBackColor = true;
            // 
            // btnLoad
            // 
            btnLoad.Location = new Point(1111, 392);
            btnLoad.Name = "btnLoad";
            btnLoad.Size = new Size(112, 34);
            btnLoad.TabIndex = 9;
            btnLoad.Text = "Načíst";
            btnLoad.UseVisualStyleBackColor = true;
            // 
            // btnGen
            // 
            btnGen.Location = new Point(1111, 520);
            btnGen.Name = "btnGen";
            btnGen.Size = new Size(112, 34);
            btnGen.TabIndex = 10;
            btnGen.Text = "Vygeneruj";
            btnGen.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.WindowFrame;
            ClientSize = new Size(1258, 664);
            Controls.Add(btnGen);
            Controls.Add(btnLoad);
            Controls.Add(btnSave);
            Controls.Add(lstDetail);
            Controls.Add(cmbPredmet);
            Controls.Add(numZnamka);
            Controls.Add(txtPrijmeni);
            Controls.Add(txtJmeno);
            Controls.Add(btnAddGrade);
            Controls.Add(btnAddStudent);
            Controls.Add(gridStudents);
            Name = "MainForm";
            Text = "Zakovska";
            Load += MainForm_Load;
            ((System.ComponentModel.ISupportInitialize)gridStudents).EndInit();
            ((System.ComponentModel.ISupportInitialize)numZnamka).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView gridStudents;
        private Button btnAddStudent;
        private Button btnAddGrade;
        private TextBox txtJmeno;
        private TextBox txtPrijmeni;
        private NumericUpDown numZnamka;
        private ComboBox cmbPredmet;
        private ListBox lstDetail;
        private Button btnSave;
        private Button btnLoad;
        private Button btnGen;
    }
}