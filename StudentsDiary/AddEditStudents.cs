using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace StudentsDiary
{
    public partial class AddEditStudent : Form
    {
        private FileHelper<List<Student>> _fileHelper = new FileHelper<List<Student>>(Program.FilePath);
        private Student _student;
        private int _studentId;

        public AddEditStudent(int id = 0)
        {
            _studentId = id;
            InitializeComponent();
            if (_studentId != 0) // zostal wybrany jakis student czyli chcesz edytowac
                EditStudentData();

            tbFirstName.Select();            
        }

        private void EditStudentData()
        {
            var students = _fileHelper.DeserializeFromFile();
            _student = students.FirstOrDefault(x => x.Id == _studentId);
            if (_student == null)
                throw new Exception("There is no student with the Id provided.");

            AssignStudentDataToTextBox();
        }

        private void AssignStudentDataToTextBox()
        {
            tbId.Text = _student.Id.ToString();
            tbFirstName.Text = _student.FirstName;
            tbLastName.Text = _student.LastName;
            tbRemarks.Text = _student.Remarks;
            tbMath.Text = _student.Mathematic;
            tbProg.Text = _student.Programing;
            tbPol.Text = _student.PolishLanguage;
            tbEng.Text = _student.EnglishLanguage;
            tbTech.Text = _student.Technology;
        }        

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private Student CreateNewStudent()
        {
            return new Student
            {
                Id = _studentId,
                FirstName = tbFirstName.Text,
                LastName = tbLastName.Text,
                EnglishLanguage = tbEng.Text,
                PolishLanguage = tbPol.Text,
                Remarks = tbRemarks.Text,
                Programing = tbProg.Text,
                Mathematic = tbMath.Text,
                Technology = tbTech.Text
            };
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            var students = _fileHelper.DeserializeFromFile();

            if (_studentId == 0)
            {
                var highestStudentId = students.OrderByDescending(x => x.Id).FirstOrDefault();
                _studentId = highestStudentId == null ? 1 : highestStudentId.Id + 1;
            }
            else
                students.RemoveAll(x => x.Id == _studentId);

            students.Add(CreateNewStudent());

            _fileHelper.SerializeToFile(students);
            Close();
        }
    }
}
