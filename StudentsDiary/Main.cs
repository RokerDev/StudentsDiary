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

        public Main()
        {
            InitializeComponent();
            var students = DeserializeFile();
            dgvTable.DataSource = students;
        }

        public void SerializeToFile(List<Student> students)
        {
            var serializer = new XmlSerializer(typeof(List<Student>));
            using (var streamWriter = new StreamWriter(_filePath))
            {
                serializer.Serialize(streamWriter, students);
                streamWriter.Close();
            }
        }
        
        public List<Student> DeserializeFile()
        {
            if (!File.Exists(_filePath))
            {
                return new List<Student>();
            }
            var serializer = new XmlSerializer(typeof(List<Student>));

            using (var streamReader = new StreamReader(_filePath))
            {
                var students = (List<Student>)serializer.Deserialize(streamReader);
                streamReader.Close();

                return students;
            }

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
                var students = DeserializeFile();
                students.RemoveAll(x => x.Id == Convert.ToInt32(selectedStudent.Cells[0].Value));
                SerializeToFile(students);
                dgvTable.DataSource = students;

            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            var students = DeserializeFile();
            dgvTable.DataSource = students;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // forma dodawania to po prostu klasa wiec trzeba utworzyc obiekt klasy
            var addEditStudents = new AddEditStudents();
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
            var addEditStudent = new AddEditStudents(selectedStudent);
            addEditStudent.ShowDialog();

            


        }

    }
}
