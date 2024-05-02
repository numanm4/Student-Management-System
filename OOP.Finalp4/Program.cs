using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Xml;
using static Class;



// Generic collection class
class Collection<T> : IEnumerable<T>
{
    private List<T> items;

    public Collection()
    {
        items = new List<T>();
    }

    public void Add(T item)
    {
        items.Add(item);
    }

    public bool Remove(T item)
    {
        return items.Remove(item);
    }

    public T Find(Func<T, bool> predicate)
    {
        return items.FirstOrDefault(predicate);
    }

    public IEnumerable<T> FindAll(Func<T, bool> predicate)
    {
        return items.Where(predicate);
    }

    public void DisplayAll(Action<T> action)
    {
        foreach (var item in items)
        {
            action(item);
        }
    }

    public IEnumerator<T> GetEnumerator()
    {
        return items.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

// created abstract person class
abstract class Person
{
    public string Id { get; set; }
    public string Name { get; set; }

    // Abstract method to display information
    public abstract void DisplayInformation();
}

// Concrete student class
class Student : Person
{
    private static int NextId = 1;
    public int Age { get; set; }
    public List<Class> Classes { get; set; }

    public Student()
    {
        Id = (1000 + NextId++).ToString();
        Classes = new List<Class>();
    }

    public Student(string name, int age) : this()
    {
        Name = name;
        Age = age;
    }

    public event StudentEnrolledEventHandler StudentEnrolled;

    public void EnrollClass(Class newClass)
    {
        Classes.Add(newClass);
        StudentEnrolled?.Invoke(this, newClass);
    }

    public void DropClass(Class oldClass)
    {
        Classes.Remove(oldClass);
    }

    public override void DisplayInformation()
    {
        Console.WriteLine($"Student ID: {Id}");
        Console.WriteLine($"Name: {Name}");
        Console.WriteLine($"Age: {Age}");
        Console.WriteLine("Classes Enrolled:");
        foreach (var classEnrolled in Classes)
        {
            Console.WriteLine($"- {classEnrolled.Name}");
        }
    }
}


// Concrete instructior class 
class Instructor : Person
{
    private static int NextId = 1;
    public string Department { get; set; }

    public Instructor(string name, string department)
    {
        Id = (2000 + NextId++).ToString();
        Name = name;
        Department = department;
    }

    public override void DisplayInformation()
    {
        Console.WriteLine($"Instructor ID: {Id}");
        Console.WriteLine($"Name: {Name}");
        Console.WriteLine($"Department: {Department}");
    }
}

// Class representing a class
class Class
{
    private static int NextId = 1;
    public int Id { get; private set; }
    public string Name { get; set; }
    public Instructor Instructor { get; set; }
    public List<Student> Students { get; set; }

    //delegate for the student enrollment event
    public delegate void StudentEnrolledEventHandler(Student student, Class @class);
    public event StudentEnrolledEventHandler StudentEnrolled;

    public Class(string name, Instructor instructor)
    {
        Id = 3000 + NextId++;
        Name = name;
        Instructor = instructor;
        Students = new List<Student>();
    }

    public void AddStudent(Student student)
    {
        Students.Add(student);
        student.EnrollClass(this);

        // Trigger the student enrolled event
        StudentEnrolled?.Invoke(student, this);
    }

    public void RemoveStudent(Student student)
    {
        Students.Remove(student);
        student.DropClass(this);
    }

    public void DisplayInformation()
    {
        Console.WriteLine($"Class ID: {Id}");
        Console.WriteLine($"Name: {Name}");
        Console.WriteLine($"Instructor: {Instructor.Name}");
        Console.WriteLine("Students Enrolled:");
        foreach (var student in Students)
        {
            Console.WriteLine($"- {student.Name}");
        }
    }
}

// Class DEpartment
class Department
{
    private static int NextId = 1;
    public int Id { get; private set; }
    public string Name { get; set; }
    public List<Course> CoursesOffered { get; set; }

    public Department(string name)
    {
        Id = 4000 + NextId++;
        Name = name;
        CoursesOffered = new List<Course>();
    }

    public void AddCourse(Course newCourse)
    {
        CoursesOffered.Add(newCourse);
    }

    public void RemoveCourse(Course course)
    {
        CoursesOffered.Remove(course);
    }

    public void DisplayInformation()
    {
        Console.WriteLine($"Department ID: {Id}");
        Console.WriteLine($"Name: {Name}");
        Console.WriteLine("Courses Offered:");
        foreach (var course in CoursesOffered)
        {
            Console.WriteLine($"- {course.Name}");
        }
    }
}

// class COurse
class Course
{
    private static int NextId = 1;
    public string Id { get; set; }
    public string Name { get; set; }
    public Department Department { get; set; }
    public List<Class> Classes { get; set; }

    public Course(string name, Department department)
    {
        Id = (100 + NextId++).ToString();
        Name = name;
        Department = department;
        Classes = new List<Class>();
    }

    public void AddClass(Class newClass)
    {
        Classes.Add(newClass);
    }

    public void RemoveClass(Class oldClass)
    {
        Classes.Remove(oldClass);
    }

    public void DisplayInformation()
    {
        Console.WriteLine($"Course ID: {Id}");
        Console.WriteLine($"Name: {Name}");
        Console.WriteLine($"Department: {Department.Name}");
        Console.WriteLine("Classes Offered:");
        foreach (var classOffered in Classes)
        {
            Console.WriteLine($"- {classOffered.Name}");
        }
    }
}


class MainProgram
{
    static Collection<Department> departments = new Collection<Department>();
    static Collection<Class> classes = new Collection<Class>();
    static Collection<Student> students = new Collection<Student>();
    static Collection<Instructor> instructors = new Collection<Instructor>();
    static Collection<Course> courses = new Collection<Course>();

    public delegate void StudentEnrolledEventHandler(Student student, Class @class);

    static void Main(string[] args)
    {
        foreach (var student in students)
        {
            student.StudentEnrolled += OnStudentEnrolled;
        }

        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("Welcome to the Student Management System");
            Console.WriteLine("1. Manage Departments");
            Console.WriteLine("2. Manage Courses");
            Console.WriteLine("3. Manage Classes");
            Console.WriteLine("4. Manage Students");
            Console.WriteLine("5. Manage Instructors");
            Console.WriteLine("6. Search for Objects");
            Console.WriteLine("7. Exit");
            Console.WriteLine("Enter your choice: ");
            int choice = Convert.ToInt32(Console.ReadLine());

            Console.Clear();

            switch (choice)
            {
                case 1:
                    ManageDepartments();
                    break;
                case 2:
                    ManageCourses();
                    break;
                case 3:
                    ManageClasses();
                    break;
                case 4:
                    ManageStudents();
                    break;
                case 5:
                    ManageInstructors();
                    break;
                case 6:
                    SearchObjects();
                    break;
                case 7:
                    return;
                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }
    }

    // event handler for when a student is enrolled in a class
    static void OnStudentEnrolled(Student student, Class classObj)
    {
        Console.WriteLine($"Student {student.Name} enrolled in class {classObj.Name}");
    }

    static void ManageDepartments()
    {
        Console.WriteLine("1. Create Department");
        Console.WriteLine("2. View Departments");
        Console.WriteLine("3. Delete Department");
        Console.WriteLine("Enter your choice: ");
        if (!int.TryParse(Console.ReadLine(), out int deptChoice))
        {
            Console.WriteLine("Invalid choice. Please enter a valid integer.");
            return;
        }

        switch (deptChoice)
        {
            case 1:
                // Create Department
                Console.WriteLine("Enter Department Name: ");
                string deptName = Console.ReadLine();
                Department newDept = new Department(deptName);
                departments.Add(newDept);
                Console.WriteLine($"{deptName} department created successfully.");
                break;
            case 2:
                // View Departments
                departments.DisplayAll(d => d.DisplayInformation());
                break;
            case 3:
                // Delete Department
                Console.WriteLine("Enter Department ID to delete: ");
                int deptIdToDelete;
                if (!int.TryParse(Console.ReadLine(), out deptIdToDelete))
                {
                    Console.WriteLine("Invalid department ID. Please enter a valid integer.");
                    break;
                }
                var deptToDelete = departments.Find(d => d.Id == deptIdToDelete);
                if (deptToDelete == null)
                {
                    Console.WriteLine("Department not found.");
                    break;
                }
                departments.Remove(deptToDelete);
                Console.WriteLine("Department deleted successfully.");
                break;
            default:
                Console.WriteLine("Invalid choice.");
                break;
        }
    }

    static void ManageCourses()
    {
        Console.WriteLine("1. Create Course");
        Console.WriteLine("2. View Courses");
        Console.WriteLine("3. Delete Course");
        Console.WriteLine("Enter your choice: ");
        int courseChoice = Convert.ToInt32(Console.ReadLine());
        switch (courseChoice)
        {
            case 1:
                // Create Course
                Console.WriteLine("Enter Course Name: ");
                string courseName = Console.ReadLine();
                Console.WriteLine("Enter Department ID for this course: ");
                int courseDeptId = Convert.ToInt32(Console.ReadLine());
                var department = departments.Find(d => d.Id == courseDeptId);
                if (department == null)
                {
                    Console.WriteLine("Department not found.");
                    break;
                }
                Course newCourse = new Course(courseName, department);
                courses.Add(newCourse);
                department.AddCourse(newCourse);
                Console.WriteLine($"{courseName} course created successfully.");
                break;
            case 2:
                // View Courses
                foreach (var course in courses)
                {
                    course.DisplayInformation();
                    Console.WriteLine();
                }
                break;
            case 3:
                // Delete Course
                Console.WriteLine("Enter Course ID to delete: ");
                string courseIdToDelete = Console.ReadLine();
                var courseToDelete = courses.Find(c => c.Id == courseIdToDelete);
                if (courseToDelete == null)
                {
                    Console.WriteLine("Course not found.");
                    break;
                }
                courses.Remove(courseToDelete);
                var deptForCourseToDelete = departments.Find(d => d.CoursesOffered.Contains(courseToDelete));
                if (deptForCourseToDelete != null)
                {
                    deptForCourseToDelete.RemoveCourse(courseToDelete);
                }
                Console.WriteLine("Course deleted successfully.");
                break;
            default:
                Console.WriteLine("Invalid choice.");
                break;
        }
    }

    static void ManageClasses()
    {
        Console.WriteLine("1. Create Class");
        Console.WriteLine("2. View Classes");
        Console.WriteLine("3. Delete Class");
        Console.WriteLine("Enter your choice: ");
        int classChoice = Convert.ToInt32(Console.ReadLine());

        switch (classChoice)
        {
            case 1:
                // Create Class
                Console.WriteLine("Enter Class Name: ");
                string className = Console.ReadLine();
                Console.WriteLine("Enter Instructor ID for this class: ");
                int instructorId;

                while (!int.TryParse(Console.ReadLine(), out instructorId))
                {
                    Console.WriteLine("Invalid input. Please enter a valid integer for the Instructor ID:");
                }

                // Find the instructor by ID
                var instructor = instructors.Find(i => i.Id == instructorId.ToString());


                // Check if instructor is found
                if (instructor == null)
                {
                    Console.WriteLine("Instructor not found with the given ID.");
                }
                else
                {
                    Console.WriteLine($"Instructor found: {instructor.Name}");
                }


                // Create a new class with the provided class name and instructor
                Class newClass = new Class(className, instructor);
                classes.Add(newClass);
                Console.WriteLine($"{className} class created successfully.");
                break;
            case 2:
                // View Classes
                classes.DisplayAll(c => c.DisplayInformation());
                break;
            case 3:
                // Delete Class 
                Console.WriteLine("Enter Class ID to delete: ");
                int classIdToDelete = Convert.ToInt32(Console.ReadLine());
                var classToDelete = classes.Find(c => c.Id == classIdToDelete);
                if (classToDelete == null)
                {
                    Console.WriteLine("Class not found.");
                    break;
                }
                classes.Remove(classToDelete);
                Console.WriteLine("Class deleted successfully.");
                break;
            default:
                Console.WriteLine("Invalid choice.");
                break;
        }
    }


    static void ManageStudents()
    {
        Console.WriteLine("1. Create Student");
        Console.WriteLine("2. View Students");
        Console.WriteLine("3. Delete Student");
        Console.WriteLine("4. Enroll Student in Class");
        Console.WriteLine("5. Drop Student from Class");
        Console.WriteLine("6. Search for Students");
        Console.WriteLine("Enter your choice: ");
        int studentChoice = Convert.ToInt32(Console.ReadLine());
        switch (studentChoice)
        {
            case 1:
                // Create Student menu option 
                Console.WriteLine("Enter Student Name: ");
                string studentName = Console.ReadLine();
                Console.WriteLine("Enter Student Age: ");
                int studentAge = Convert.ToInt32(Console.ReadLine());
                Student newStudent = new Student(studentName, studentAge);
                students.Add(newStudent);
                Console.WriteLine("Student created successfully.");
                Console.WriteLine($"Name: {studentName}, Age: {studentAge}, ID: {newStudent.Id}");
                break;
            case 2:
                // View Students
                foreach (var student in students)
                {
                    student.DisplayInformation();
                    Console.WriteLine();
                }
                break;
            case 3:
                // Delete Student the student from the class 
                Console.WriteLine("Enter Student ID to delete: ");
                int studentId = Convert.ToInt32(Console.ReadLine());
                var studentToDelete = students.FirstOrDefault(s => s.Id == studentId.ToString());
                Console.WriteLine("Student deleted successfully.");
                if (studentToDelete != null)
                {
                    students.Remove(studentToDelete);
                }
                else
                {
                    Console.WriteLine("Student not found.");
                }
                break;
            case 4:
                // Enroll the student in the class.
                Console.WriteLine("Enter Student ID: ");
                foreach (var student in students)
                {
                    Console.WriteLine($"- {student.Id}, {student.Name}");
                }
                int studentIdToEnroll;
                if (!int.TryParse(Console.ReadLine(), out studentIdToEnroll))
                {
                    Console.WriteLine("Invalid student ID. Please enter a valid integer.");
                    break;
                }
                var studentToEnrollClass = students.FirstOrDefault(s => s.Id == studentIdToEnroll.ToString());

                if (studentToEnrollClass == null)
                {
                    Console.WriteLine("Student not found.");
                    break;
                }

                Console.WriteLine("Enter Class ID to enroll student in:");
                foreach (var classObj in classes)
                {
                    Console.WriteLine($"- {classObj.Id}, {classObj.Name}");
                }
                int classIdToEnroll;
                if (!int.TryParse(Console.ReadLine(), out classIdToEnroll))
                {
                    Console.WriteLine("Invalid class ID. Please enter a valid integer.");
                    break;
                }

                var classToEnroll = classes.FirstOrDefault(c => c.Id == classIdToEnroll);

                if (classToEnroll == null)
                {
                    Console.WriteLine("Class not found.");
                    break;
                }

                classToEnroll.AddStudent(studentToEnrollClass);
                Console.WriteLine($"Student {studentToEnrollClass.Name} enrolled in class {classToEnroll.Name}");
                break;
            case 5:
                // Drop Student from Class
                Console.WriteLine("Enter Student ID: ");
                foreach (var student in students)
                {
                    Console.WriteLine($"- {student.Id}, {student.Name}");
                }
                int studentIdToDrop;
                if (!int.TryParse(Console.ReadLine(), out studentIdToDrop))
                {
                    Console.WriteLine("Invalid student ID. Please enter a valid integer.");
                    break;
                }

                var studentToDropClass = students.FirstOrDefault(s => s.Id == studentIdToDrop.ToString());

                if (studentToDropClass == null)
                {
                    Console.WriteLine("Student not found.");
                    break;
                }

                Console.WriteLine("Enter Class ID to drop student from:");
                foreach (var classObj in classes)
                {
                    Console.WriteLine($"- {classObj.Id}, {classObj.Name}");
                }
                int classIdToDrop;
                if (!int.TryParse(Console.ReadLine(), out classIdToDrop))
                {
                    Console.WriteLine("Invalid class ID. Please enter a valid integer.");
                    break;
                }

                var classToDrop = classes.FirstOrDefault(c => c.Id == classIdToDrop);

                if (classToDrop == null)
                {
                    Console.WriteLine("Class not found.");
                    break;
                }

                classToDrop.RemoveStudent(studentToDropClass);
                Console.WriteLine($"Student {studentToDropClass.Name} dropped from class {classToDrop.Name}");
                break;
            case 6:
                // Search for Students
                Console.WriteLine("Search students based on criteria:");
                Console.WriteLine("1. By name");
                Console.WriteLine("2. By age");
                Console.WriteLine("Enter your choice:");
                int searchChoice = Convert.ToInt32(Console.ReadLine());

                switch (searchChoice)
                {
                    case 1:
                        Console.WriteLine("Enter student name to search:");
                        string searchName = Console.ReadLine();
                        var studentsByName = students.FindAll(s => s.Name.Equals(searchName, StringComparison.OrdinalIgnoreCase));

                        if (studentsByName.Any())
                        {
                            foreach (var student in studentsByName)
                            {
                                student.DisplayInformation();
                            }
                        }
                        else
                        {
                            Console.WriteLine("No students found with the name provided.");
                        }
                        break;
                    case 2:
                        Console.WriteLine("Enter student age to search:");
                        int searchAge = Convert.ToInt32(Console.ReadLine());
                        var studentsByAge = students.FindAll(s => s.Age == searchAge);

                        if (studentsByAge.Any())
                        {
                            foreach (var student in studentsByAge)
                            {
                                student.DisplayInformation();
                            }
                        }
                        else
                        {
                            Console.WriteLine("No students found with the age provided.");
                        }
                        break;
                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
                break;
            default:
                Console.WriteLine("Invalid choice.");
                break;
        }
    }

    static void ManageInstructors()
    {
        Console.WriteLine("1. Create Instructor");
        Console.WriteLine("2. View Instructors");
        Console.WriteLine("3. Delete Instructor");
        Console.WriteLine("Enter your choice: ");
        int instructorChoice = Convert.ToInt32(Console.ReadLine());
        switch (instructorChoice)
        {
            case 1:
                // create InstRuctor
                Console.WriteLine("Enter Instructor Name: ");
                string instructorName = Console.ReadLine();
                Console.WriteLine("Enter Instructor Department: ");
                string instructorDept = Console.ReadLine();
                Instructor newInstructor = new Instructor(instructorName, instructorDept);
                instructors.Add(newInstructor);
                Console.WriteLine("Instructor created successfully.");
                Console.WriteLine($"Name: {instructorName}, Department: {instructorDept}, ID: {newInstructor.Id}");
                break;
            case 2:
                // view instructors
                foreach (var instructor in instructors)
                {
                    instructor.DisplayInformation();
                    Console.WriteLine();
                }
                break;
            case 3:
                // delete Instructor
                Console.WriteLine("Enter Instructor ID to delete: ");
                int instructorIdToDelete = Convert.ToInt32(Console.ReadLine());
                var instructorToDelete = instructors.Find(i => i.Id == instructorIdToDelete.ToString());
                if (instructorToDelete == null)
                {
                    Console.WriteLine("Instructor not found.");
                    break;
                }
                instructors.Remove(instructorToDelete);
                Console.WriteLine("Instructor deleted successfully.");
                break;
            default:
                Console.WriteLine("Invalid choice.");
                break;
        }
    }

    static void SaveDataToFile()
    {
        string filePath = "university_data.json";

        // Create a dictionary to hold the data
        var data = new Dictionary<string, object>
    {
        { "students", students },
        { "instructors", instructors },
        { "classes", classes },
        { "courses", courses },
        { "departments", departments }
    };

        // Serialize the data to JSON
        string jsonData = JsonConvert.SerializeObject(data, Newtonsoft.Json.Formatting.Indented);

        // Write the JSON data to the file
        File.WriteAllText(filePath, jsonData);
        Console.WriteLine($"Data saved to {filePath}");
    }

    static void LoadDataFromFile()
    {
        string filePath = "university_data.json";

        // Check if the file exists
        if (!File.Exists(filePath))
        {
            Console.WriteLine($"File not found: {filePath}");
            return;
        }

        // Read the JSON data from the file
        string jsonData = File.ReadAllText(filePath);

        // Deserialize the JSON data
        var data = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonData);

        students = new Collection<Student>();
        instructors = new Collection<Instructor>();
        classes = new Collection<Class>();
        courses = new Collection<Course>();
        departments = new Collection<Department>();

        // Load data into the collections
        var studentList = JsonConvert.DeserializeObject<List<Student>>(data["students"].ToString());
        var instructorList = JsonConvert.DeserializeObject<List<Instructor>>(data["instructors"].ToString());
        var classList = JsonConvert.DeserializeObject<List<Class>>(data["classes"].ToString());
        var courseList = JsonConvert.DeserializeObject<List<Course>>(data["courses"].ToString());
        var departmentList = JsonConvert.DeserializeObject<List<Department>>(data["departments"].ToString());

        // Add the deserialized data to the collections
        foreach (var student in studentList)
        {
            students.Add(student);
        }

        foreach (var instructor in instructorList)
        {
            instructors.Add(instructor);
        }

        foreach (var classObj in classList)
        {
            classes.Add(classObj);
        }

        foreach (var course in courseList)
        {
            courses.Add(course);
        }

        foreach (var department in departmentList)
        {
            departments.Add(department);
        }

        Console.WriteLine($"Data loaded from {filePath}");
    }

    static void SearchObjects()
    {
        Console.WriteLine("1. Search for Students");
        Console.WriteLine("2. Search for Instructors");
        Console.WriteLine("3. Search for Classes");
        Console.WriteLine("4. Search for Courses");
        Console.WriteLine("5. Search for Departments");
        Console.WriteLine("6. Save data to file");
        Console.WriteLine("7. Load data from file");
        Console.WriteLine("Enter your choice: ");
        int searchChoice = Convert.ToInt32(Console.ReadLine());

        Console.Clear();

        switch (searchChoice)
        {
            case 1:
                // call search for Students method
                SearchStudents();
                break;
            case 2:
                // calling search for Instructors method
                SearchInstructors();
                break;
            case 3:
                //calling search for classes method
                SearchClasses();
                break;
            case 4:
                //calling Search for Courses method
                SearchCourses();
                break;
            case 5:
                //calling Search for DEpartments method
                SearchDepartments();
                break;
            case 6:
                //calling  Save data to file method
                SaveDataToFile();
                break;
            case 7:
                // calling Load data from file method
                LoadDataFromFile();
                break;
            default:
                Console.WriteLine("Invalid choice.");
                break;
        }
    }

    static void SearchStudents()
    {
        Console.WriteLine("Search students based on criteria:");
        Console.WriteLine("1. By name");
        Console.WriteLine("2. By age");
        Console.WriteLine("Enter your choice:");
        int searchChoice = Convert.ToInt32(Console.ReadLine());

        switch (searchChoice)
        {
            case 1:
                Console.WriteLine("Enter student name to search:");
                string searchName = Console.ReadLine();
                var studentsByName = students.FindAll(s => s.Name.Equals(searchName, StringComparison.OrdinalIgnoreCase));

                if (studentsByName.Any())
                {
                    foreach (var student in studentsByName)
                    {
                        student.DisplayInformation();
                    }
                }
                else
                {
                    Console.WriteLine("No students found with the name provided.");
                }
                break;
            case 2:
                Console.WriteLine("Enter student age to search:");
                int searchAge = Convert.ToInt32(Console.ReadLine());
                var studentsByAge = students.FindAll(s => s.Age == searchAge);

                if (studentsByAge.Any())
                {
                    foreach (var student in studentsByAge)
                    {
                        student.DisplayInformation();
                    }
                }
                else
                {
                    Console.WriteLine("No students found with the age provided.");
                }
                break;
            default:
                Console.WriteLine("Invalid choice.");
                break;
        }
    }

    static void SearchInstructors()
    {
        Console.WriteLine("Search instructors based on criteria:");
        Console.WriteLine("1. By name");
        Console.WriteLine("2. By department");
        Console.WriteLine("Enter your choice:");
        int searchChoice = Convert.ToInt32(Console.ReadLine());

        switch (searchChoice)
        {
            case 1:
                Console.WriteLine("Enter instructor name to search:");
                string searchName = Console.ReadLine();
                var instructorsByName = instructors.FindAll(i => i.Name.Equals(searchName, StringComparison.OrdinalIgnoreCase));

                if (instructorsByName.Any())
                {
                    foreach (var instructor in instructorsByName)
                    {
                        instructor.DisplayInformation();
                    }
                }
                else
                {
                    Console.WriteLine("No instructors found with the name provided.");
                }
                break;
            case 2:
                Console.WriteLine("Enter instructor department to search:");
                string searchDept = Console.ReadLine();
                var instructorsByDept = instructors.FindAll(i => i.Department.Equals(searchDept, StringComparison.OrdinalIgnoreCase));

                if (instructorsByDept.Any())
                {
                    foreach (var instructor in instructorsByDept)
                    {
                        instructor.DisplayInformation();
                    }
                }
                else
                {
                    Console.WriteLine("No instructors found with the department provided.");
                }
                break;
            default:
                Console.WriteLine("Invalid choice.");
                break;
        }
    }

    static void SearchClasses()
    {
        Console.WriteLine("Search classes based on criteria:");
        Console.WriteLine("1. By name");
        Console.WriteLine("2. By instructor");
        Console.WriteLine("Enter your choice:");
        int searchChoice = Convert.ToInt32(Console.ReadLine());

        switch (searchChoice)
        {
            case 1:
                Console.WriteLine("Enter class name to search:");
                string searchName = Console.ReadLine();
                var classesByName = classes.FindAll(c => c.Name.Equals(searchName, StringComparison.OrdinalIgnoreCase));

                if (classesByName.Any())
                {
                    foreach (var classObj in classesByName)
                    {
                        classObj.DisplayInformation();
                    }
                }
                else
                {
                    Console.WriteLine("No classes found with the name provided.");
                }
                break;
            case 2:
                Console.WriteLine("Enter instructor ID to search for classes:");
                string searchInstructorId = Console.ReadLine();
                var classesByInstructor = classes.FindAll(c => c.Instructor.Id.Equals(searchInstructorId, StringComparison.OrdinalIgnoreCase));

                if (classesByInstructor.Any())
                {
                    foreach (var classObj in classesByInstructor)
                    {
                        classObj.DisplayInformation();
                    }
                }
                else
                {
                    Console.WriteLine("No classes found with the instructor provided.");
                }
                break;
            default:
                Console.WriteLine("Invalid choice.");
                break;
        }
    }

    static void SearchCourses()
    {
        Console.WriteLine("Search courses based on criteria:");
        Console.WriteLine("1. By name");
        Console.WriteLine("2. By department");
        Console.WriteLine("Enter your choice:");
        int searchChoice = Convert.ToInt32(Console.ReadLine());

        switch (searchChoice)
        {
            case 1:
                Console.WriteLine("Enter course name to search:");
                string searchName = Console.ReadLine();
                var coursesByName = courses.FindAll(c => c.Name.Equals(searchName, StringComparison.OrdinalIgnoreCase));

                if (coursesByName.Any())
                {
                    foreach (var course in coursesByName)
                    {
                        course.DisplayInformation();
                    }
                }
                else
                {
                    Console.WriteLine("No courses found with the name provided.");
                }
                break;
            case 2:
                Console.WriteLine("Enter department ID to search for courses:");
                string searchDeptId = Console.ReadLine();
                var coursesByDepartment = courses.FindAll(c => c.Department.Id.Equals(searchDeptId));

                if (coursesByDepartment.Any())
                {
                    foreach (var course in coursesByDepartment)
                    {
                        course.DisplayInformation();
                    }
                }
                else
                {
                    Console.WriteLine("No courses found in the provided department.");
                }
                break;
            default:
                Console.WriteLine("Invalid choice.");
                break;
        }
    }

    static void SearchDepartments()
    {
        Console.WriteLine("Search departments based on criteria:");
        Console.WriteLine("1. By name");
        Console.WriteLine("2. By ID");
        Console.WriteLine("Enter your choice:");
        int searchChoice = Convert.ToInt32(Console.ReadLine());

        switch (searchChoice)
        {
            case 1:
                Console.WriteLine("Enter department name to search:");
                string searchName = Console.ReadLine();
                var departmentsByName = departments.FindAll(d => d.Name.Equals(searchName, StringComparison.OrdinalIgnoreCase));

                if (departmentsByName.Any())
                {
                    foreach (var department in departmentsByName)
                    {
                        department.DisplayInformation();
                    }
                }
                else
                {
                    Console.WriteLine("No departments found with the name provided.");
                }
                break;
            case 2:
                Console.WriteLine("Enter department ID to search:");
                int searchId = Convert.ToInt32(Console.ReadLine());
                var departmentsById = departments.FindAll(d => d.Id == searchId);

                if (departmentsById.Any())
                {
                    foreach (var department in departmentsById)
                    {
                        department.DisplayInformation();
                    }
                }
                else
                {
                    Console.WriteLine("No departments found with the ID provided.");
                }
                break;
            default:
                Console.WriteLine("Invalid choice.");
                break;
        }
    }
}