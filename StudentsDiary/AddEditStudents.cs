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
    public partial class AddEditStudents : Form
    {
        private string _filePath = Path.Combine(Environment.CurrentDirectory, "students.txt");

        private int _studentId;
        public AddEditStudents(int id = 0)
        {
            _studentId = id;
            InitializeComponent();
            if (id != 0) // zostal wybrany jakis student czyli chcesz edytowac
            {
                var students = DeserializeFromFile();
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

        public void SerializeToFile(List<Student> students)
        {
            //inicjalizacja serializera
            var serializer = new XmlSerializer(typeof(List<Student>));
            // wygodna skladnia ktora zapewnia prawidlowe uzycie dispose
            // ktorego w tym przypadku wymaga obiekt streamwriter
            using (var streamWriter = new StreamWriter(_filePath))
            {
                serializer.Serialize(streamWriter, students);
                streamWriter.Close();
            }
        }

        public List<Student> DeserializeFromFile()
        {
            if (!File.Exists(_filePath))
                return new List<Student>();

            var serializer = new XmlSerializer(typeof(List<Student>));
            
            using (var streamReader = new StreamReader(_filePath))
            {
                var students = (List<Student>)serializer.Deserialize(streamReader);
                streamReader.Close();
                return students;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            var students = DeserializeFromFile();

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

            SerializeToFile(students);
            Close();
        }

    }
}
