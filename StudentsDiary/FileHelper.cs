using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace StudentsDiary
{
    class FileHelper<T> where T : new()
    {
        private string _filePath;
        public FileHelper(string FilePath)
        {
            _filePath = FilePath;
        }

        public void SerializeToFile(T students)
        {
            //inicjalizacja serializera
            var serializer = new XmlSerializer(typeof(T));
            // wygodna skladnia ktora zapewnia prawidlowe uzycie dispose
            // ktorego w tym przypadku wymaga obiekt streamwriter
            using (var streamWriter = new StreamWriter(_filePath))
            {
                serializer.Serialize(streamWriter, students);
                streamWriter.Close();
            }
        }

        public T DeserializeFromFile()
        {
            if (!File.Exists(_filePath))
                return new T();

            var serializer = new XmlSerializer(typeof(T));

            using (var streamReader = new StreamReader(_filePath))
            {
                var students = (T)serializer.Deserialize(streamReader);
                streamReader.Close();
                return students;
            }
        }
    }
}
