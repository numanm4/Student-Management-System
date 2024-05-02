# OOP.Finalp4

** This project is built on 3 part previous from this final one. each part builds on the one previous of it. **

part 1 of this project (basic system design): 

// adding using system as needed
class Student
{
    public string StudentId { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public List<string> CoursesEnrolled { get; set; }

    public Student(string studentId, string name, int age)
    {
        StudentId = studentId;
        Name = name;
        Age = age;
        CoursesEnrolled = new List<string>();
    }

    public void EnrollCourse(string course)
    {
        CoursesEnrolled.Add(course);
    }

    public void DropCourse(string course)
    {
        CoursesEnrolled.Remove(course);
    }

    public void DisplayInformation()
    {
        Console.WriteLine("Student ID: " + StudentId);
        Console.WriteLine("Name: " + Name);
        Console.WriteLine("Age: " + Age);
        Console.WriteLine("Courses Enrolled: " + string.Join(", ", CoursesEnrolled));
    }
}

class Class
{
    public string ClassId { get; set; }
    public string Name { get; set; }
    public string Instructor { get; set; }
    public string Department { get; set; }
    public string Course { get; set; }
    public List<Student> Students { get; set; }

    public Class(string classId, string name, string instructor, string department, string course)
    {
        ClassId = classId;
        Name = name;
        Instructor = instructor;
        Department = department;
        Course = course;
        Students = new List<Student>();
    }

    public void AddStudent(Student student)
    {
        Students.Add(student);
    }

    public void RemoveStudent(Student student)
    {
        Students.Remove(student);
    }

    public void DisplayInformation()
    {
        Console.WriteLine("Class ID: " + ClassId);
        Console.WriteLine("Name: " + Name);
        Console.WriteLine("Instructor: " + Instructor);
        Console.WriteLine("Department: " + Department);
        Console.WriteLine("Course: " + Course);
        Console.WriteLine("Students: " + string.Join(", ", Students));
    }
}

class Department
{
    public string DepartmentId { get; set; }
    public string Name { get; set; }
    public List<Class> ClassesOffered { get; set; }

    public Department(string departmentId, string name)
    {
        DepartmentId = departmentId;
        Name = name;
        ClassesOffered = new List<Class>();
    }

    public void AddClass(Class classObj)
    {
        ClassesOffered.Add(classObj);
    }

    public void RemoveClass(Class classObj)
    {
        ClassesOffered.Remove(classObj);
    }

    public void DisplayInformation()
    {
        Console.WriteLine("Department ID: " + DepartmentId);
        Console.WriteLine("Name: " + Name);
        Console.WriteLine("Classes Offered: " + string.Join(", ", ClassesOffered));
    }
}

class Instructor
{
    public string InstructorId { get; set; }
    public string Name { get; set; }
    public List<Class> ClassesTaught { get; set; }

    public Instructor(string instructorId, string name)
    {
        InstructorId = instructorId;
        Name = name;
        ClassesTaught = new List<Class>();
    }

    public void AddClass(Class classObj)
    {
        ClassesTaught.Add(classObj);
    }

    public void RemoveClass(Class classObj)
    {
        ClassesTaught.Remove(classObj);
    }

    public void DisplayInformation()
    {
        Console.WriteLine("Instructor ID: " + InstructorId);
        Console.WriteLine("Name: " + Name);
        Console.WriteLine("Classes Taught: " + string.Join(", ", ClassesTaught));
    }
}

class Course
{
    public string CourseId { get; set; }
    public string Name { get; set; }
    public string Department { get; set; }
}

class MainProgram
{
    static void Main(string[] args)
    {
        // Here are some sample course objects
        Course course1 = new Course
        {
            CourseId = "SODV1251",
            Name = "Web Programming",
            Department = "Computer Science"
        };

        Course course2 = new Course
        {
            CourseId = "DCM1071",
            Name = "Discrete Math",
            Department = "Mathematics"
        };

        // smple student objects 
        Student student1 = new Student("S001", "Alice", 20);
        Student student2 = new Student("S002", "Bob", 21);

        // enrolling students
        student1.EnrollCourse(course1.Name);
        student1.EnrollCourse(course2.Name);
        student2.EnrollCourse(course1.Name);

        // crate instructor
        Instructor instructor1 = new Instructor("I001", "Dr. Smith");

        // sample classes objects
        Class class1 = new Class("SODV1251", "Web Programming Class", instructor1.Name, course1.Department, course1.Name);
        class1.AddStudent(student1);
        class1.AddStudent(student2);

        Class class2 = new Class("DCM1071", "Discrete Math Class", instructor1.Name, course2.Department, course2.Name);
        class2.AddStudent(student1);

        // sample department class
        Department department1 = new Department("D001", "Computer Science");
        department1.AddClass(class1);

        Department department2 = new Department("D002", "Mathematics");
        department2.AddClass(class2);


        Console.WriteLine("Course Information:");
        Console.WriteLine("Course 1:");
        Console.WriteLine("Course ID: " + course1.CourseId);
        Console.WriteLine("Name: " + course1.Name);
        Console.WriteLine("Department: " + course1.Department);

        Console.WriteLine("\nCourse 2:");
        Console.WriteLine("Course ID: " + course2.CourseId);
        Console.WriteLine("Name: " + course2.Name);
        Console.WriteLine("Department: " + course2.Department);

        Console.WriteLine("\nStudent Information:");
        student1.DisplayInformation();
        student2.DisplayInformation();

        Console.WriteLine("\nClass Information:");
        class1.DisplayInformation();
        class2.DisplayInformation();

        Console.WriteLine("\nDepartment Information:");
        department1.DisplayInformation();
        department2.DisplayInformation();
    }
}

part 2: 
// add using system as needed
namespace OOP.FinalP2
{
    // Abstract class representing a person
    abstract class Person
    {
        public string Id { get; set; }
        public string Name { get; set; }

        // Abstract method to display information
        public abstract void DisplayInformation();
    }

    // Concrete class representing a student
    class Student : Person
    {
        public int Age { get; set; }
        public List<string> CoursesEnrolled { get; set; }

        public Student(string id, string name, int age)
        {
            Id = id;
            Name = name;
            Age = age;
            CoursesEnrolled = new List<string>();
        }

        public void EnrollCourse(string course)
        {
            CoursesEnrolled.Add(course);
        }

        public void DropCourse(string course)
        {
            CoursesEnrolled.Remove(course);
        }

        public override void DisplayInformation()
        {
            Console.WriteLine("Student ID: " + Id);
            Console.WriteLine("Name: " + Name);
            Console.WriteLine("Age: " + Age);
            Console.WriteLine("Courses Enrolled: " + string.Join(", ", CoursesEnrolled));
        }
    }

    // Concrete class representing an instructor
    class Instructor : Person
    {
        public string Department { get; set; }

        public Instructor(string id, string name, string department)
        {
            Id = id;
            Name = name;
            Department = department;
        }

        public override void DisplayInformation()
        {
            Console.WriteLine("Instructor ID: " + Id);
            Console.WriteLine("Name: " + Name);
            Console.WriteLine("Department: " + Department);
        }
    }

    class MainProgram
    {
        static void Main(string[] args)
        {
            // Created a list of people (students and instructors)
            List<Person> people = new List<Person>();

            // Add some students and instructors to the list
            people.Add(new Student("S001", "Alice", 20));
            people.Add(new Student("S002", "Bob", 21));
            people.Add(new Instructor("I001", "Dr. Smith", "Computer Science"));
            people.Add(new Instructor("I002", "Prof. Johnson", "Mathematics"));

            // Displaying information about each person using polymorphism
            foreach (Person person in people)
            {
                person.DisplayInformation();
                Console.WriteLine();
            }

            Console.ReadLine();
        }
    }
}

part 3 is the same as part 4 without the LINQ queiries and the 'search based on specific criteria' method

