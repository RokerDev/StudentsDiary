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
    public partial class AddEditStudent : Form
    {
        private string _filePath = Path.Combine(Environment.CurrentDirectory, "students.txt");
        private FileHelper<List<Student>> _fileHelper = new FileHelper<List<Student>>(Path.Combine(Environment.CurrentDirectory, "students.txt"));


        private int _studentId;
        public AddEditStudent(int id = 0)
        {
            _studentId = id;
            InitializeComponent();
            if (id != 0) // zostal wybrany jakis student czyli chcesz edytowac
            {
                var students = _fileHelper.DeserializeFromFile();
                var student = students.FirstOrDefault(x => x.Id == id);
                if (student == null)
                    throw new Exception("There is no student with the Id provided.");
                
                tbId.Text = student.Id.ToString();
                tbFirstName.Text = student.FirstName;
                tbLastName.Text = student.LastName;
                tbRemarks.Text = student.Remarks;
                tbMath.Text = student.Mathematic;
                tbProg.Text = student.Programing;
                tbPol.Text = student.PolishLanguage;
                tbEng.Text = student.EnglishLanguage;
                tbTech.Text = student.Technology;
            }
            tbFirstName.Select();
            
        }

        

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
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
            {
                students.RemoveAll(x => x.Id == _studentId);
            }
            

            var student = new Student
            {
                Id = _studentId,
                FirstName = tbFirstName.Text,
                LastName = tbLastName.Text,
                EnglishLanguage = tbEng.Text,
                PolishLanguage = tbPol.Text,
                Remarks = tbRemarks.Text,
                Programing = tbProg.Text,
                Mathematic = tbMath.Text,
                Technology =tbTech.Text
            };

            students.Add(student);

            _fileHelper.SerializeToFile(students);
            Close();
        }

    }
}
