using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace StudentsDiary
{
    public partial class Main : Form
    {
        private string _filePath = Path.Combine(Environment.CurrentDirectory, "students.txt");
        private FileHelper<List<Student>> _fileHelper = new FileHelper<List<Student>>(Path.Combine(Environment.CurrentDirectory, "students.txt"));

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

            var confirmDelete = 
            MessageBox.Show($"Do you really want to remove" +
                $" {selectedStudent.Cells[1].Value.ToString()} " +
                $"{selectedStudent.Cells[2].Value.ToString()} from diary", 
                "Removing Student", MessageBoxButtons.OKCancel);

            if (confirmDelete == DialogResult.OK)
            {
                var students = _fileHelper.DeserializeFromFile();
                students.RemoveAll(x => x.Id == Convert.ToInt32(selectedStudent.Cells[0].Value));
                _fileHelper.SerializeToFile(students);
                RefreshTable();

            }
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
