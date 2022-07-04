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
        public AddEditStudents()
        {
            InitializeComponent();
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

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
