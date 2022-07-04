namespace StudentsDiary
{
    public class Student
    {
        //zeby wyswietlic dane w data grid view 
        //nasza klasa student musi zawierac wlasciwosci a nie pola
        //public int Id;
        //public string FirstName;
        //public string LastName;
        //public string Mathematic;      Pola
        //public string Technology;
        //public string PolishLanguage;
        //public string EnglishLanguage;
        //public string Programing;
        //public string Remarks;
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Mathematic { get; set; }
        public string Technology { get; set; } // wlasciwosci
        public string PolishLanguage { get; set; }
        public string EnglishLanguage { get; set; }
        public string Programing { get; set; }
        public string Remarks { get; set; }
    }
}