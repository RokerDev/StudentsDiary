using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace StudentsDiary
{
    public partial class Main : Form
    {
        private FileHelper<List<Student>> _fileHelper = new FileHelper<List<Student>>(Program.FilePath);
        private List<string> _sortingOptions = Student.ListGroupId;

        public Main()
        {
            InitializeComponent();
            RefreshTable();
            AssingNamesToColumnHeaders();
            SetComboBoxOptions(cbSorting, _sortingOptions);
            AddSortingOptions("All");
            cbSorting.SelectedItem = "All";
        }

        private void AddSortingOptions(string option)
        {
            cbSorting.Items.Add(option);
        }

        public static void SetComboBoxOptions(ComboBox comboBox, List<string> options)
        {
            foreach (var id in options)
            {
                comboBox.Items.Add(id);
            }
        }

        private void AssingNamesToColumnHeaders()
        {
            dgvTable.Columns[1].HeaderText = "First Name";
            dgvTable.Columns[2].HeaderText = "Last Name";
            dgvTable.Columns[3].HeaderText = "Group Id";
            dgvTable.Columns[4].HeaderText = "Mathematic";
            dgvTable.Columns[5].HeaderText = "Technology";
            dgvTable.Columns[6].HeaderText = "Polish Language";
            dgvTable.Columns[7].HeaderText = "English Language";
            dgvTable.Columns[8].HeaderText = "Programing";
            dgvTable.Columns[9].HeaderText = "Have Activities";
            dgvTable.Columns[10].HeaderText = "Remarks";
        }
        
        private void DeleteStudent(int studentId)
        {
            var students = _fileHelper.DeserializeFromFile();
            students.RemoveAll(x => x.Id == studentId);
            _fileHelper.SerializeToFile(students);
        }

        private void RefreshTable()
        {
            //var students = _fileHelper.DeserializeFromFile();
            //dgvTable.DataSource = students;
            var students = _fileHelper.DeserializeFromFile();

            if ((string)cbSorting.SelectedItem != "All")
            {
                var sortedStudents = students.Where(x => x.GroupId == (string)cbSorting.SelectedItem).ToList();
                dgvTable.DataSource = sortedStudents;
            }
            else
                dgvTable.DataSource = students;
        }


        private void btnAdd_Click(object sender, EventArgs e)
        {
            // forma dodawania to po prostu klasa wiec trzeba utworzyc obiekt klasy
            var addEditStudent = new AddEditStudent();
            addEditStudent.Text = "Add Student";
            addEditStudent.ShowDialog();
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
            MessageBox.Show($"Do you really want to remove " +
                $"{selectedStudentFName} {selectedStudentLName} from diary".Trim(),
                "Removing Student", MessageBoxButtons.OKCancel);

            if (confirmDelete == DialogResult.OK)
            {
                DeleteStudent(selectedStudentId);
                RefreshTable();
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshTable();
        }

        private void cbSorting_SelectedIndexChanged(object sender, EventArgs e)
        {
            //var students = _fileHelper.DeserializeFromFile();

            //if ((string)cbSorting.SelectedItem != "All")
            //{
            //    var sortedStudents = students.Where(x => x.GroupId == (string)cbSorting.SelectedItem).ToList();
            //    dgvTable.DataSource = sortedStudents;
            //}
            //else
            //    dgvTable.DataSource = students;
            RefreshTable();
        }
    }
}
