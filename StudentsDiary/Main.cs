using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace StudentsDiary
{
    public partial class Main : Form
    {
        private FileHelper<List<Student>> _fileHelper = new FileHelper<List<Student>>(Program.FilePath);

        public Main()
        {
            InitializeComponent();
            RefreshTable();
            AssingNamesToColumnHeaders();
        }

        private void RefreshTable()
        {
            var students = _fileHelper.DeserializeFromFile();
            dgvTable.DataSource = students;
        }

        private void AssingNamesToColumnHeaders()
        {
            dgvTable.Columns[1].HeaderText = "First Name";
            dgvTable.Columns[2].HeaderText = "Last Name";
            dgvTable.Columns[3].HeaderText = "Mathematic";
            dgvTable.Columns[4].HeaderText = "Technology";
            dgvTable.Columns[5].HeaderText = "Polish Language";
            dgvTable.Columns[6].HeaderText = "English Language";
            dgvTable.Columns[7].HeaderText = "Programing";
            dgvTable.Columns[8].HeaderText = "Remarks";
        }

        
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvTable.SelectedRows.Count == 0)
            {
                MessageBox.Show("Highlight the student you want to remove.");
                return;
            }

            var selectedStudent = dgvTable.SelectedRows[0];
            var selectedStudentId = Convert.ToInt32(selectedStudent.Cells[0].Value);
            var selectedStudentFName = selectedStudent.Cells[1].Value.ToString();
            var selectedStudentLName = selectedStudent.Cells[2].Value.ToString();

            var confirmDelete = 
            MessageBox.Show($"Do you really want to remove" +
                $" {selectedStudentFName} " +
                $"{selectedStudentLName} from diary", 
                "Removing Student", MessageBoxButtons.OKCancel);

            if (confirmDelete == DialogResult.OK)
            {
                DeleteStudent(selectedStudentId);
                RefreshTable();
            }
        }

        private void DeleteStudent(int studentId)
        {
            var students = _fileHelper.DeserializeFromFile();
            students.RemoveAll(x => x.Id == studentId);
            _fileHelper.SerializeToFile(students);            
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshTable();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {            
            // forma dodawania to po prostu klasa wiec trzeba utworzyc obiekt klasy
            var addEditStudents = new AddEditStudent();
            addEditStudents.Text = "Add Student";
            addEditStudents.ShowDialog();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvTable.SelectedRows.Count == 0)
            {
                MessageBox.Show("Highlight the student you want to edit.");
                return;
            }

            var selectedStudent = Convert.ToInt32(dgvTable.SelectedRows[0].Cells[0].Value);
            var addEditStudent = new AddEditStudent(selectedStudent);
            addEditStudent.Text = "Edit Student";
            addEditStudent.ShowDialog();
        }

    }
}
