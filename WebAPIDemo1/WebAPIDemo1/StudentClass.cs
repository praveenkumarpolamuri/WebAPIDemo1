namespace WebAPIDemo1
{
   
    public class Student
    {
        // Properties of the Student class
        public string Name { get; set; }
        public int Age { get; set; }
        public string Major { get; set; }
        public double GPA { get; set; }
        // Constructor to initialize the Student object
        public Student(string name, int age, string major, double gpa)
        {
            Name = name;
            Age = age;
            Major = major;
            GPA = gpa;
        }

        public Student()
        {
        }
    }

}
