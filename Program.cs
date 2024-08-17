using System;

class Program
{
    static void Main()
    {

        new Student("Hung", new DateTime(2001, 1, 1), "Address1", 160, 50, "S001111111", "University A", 2019, 8.5f);
        new Student("Hoa", new DateTime(2000, 2, 2), "Address2", 170, 60, "S002111111", "University B", 2018, 6.0f);
        new Student("Bi", new DateTime(1999, 3, 3), "Address3", 165, 55, "S003111111", "University C", 2017, 4.0f);
        new Student("Luong", new DateTime(1998, 3, 3), "Address4", 166, 55, "S003111112", "University C", 2017, 4.0f);

        List<Student> students = new List<Student>();
        while (true)
        {
            Console.WriteLine("\nChoose an option:");
            Console.WriteLine("1. Add a new student");
            Console.WriteLine("2. Search for a student by ID");
            Console.WriteLine("3. Update student details by ID");
            Console.WriteLine("4. Delete student by ID");
            Console.WriteLine("5. percent of academic performance");
            Console.WriteLine("6. Display GPA percentages");
            Console.WriteLine("7. Display list students by academic performance");
            Console.WriteLine("0. Exit");
            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine();



            switch (choice)
            {
                case "1":
                    CreateNewStudent();
                    Student.PrintAllStudents();
                    break;
                case "2":
                    SearchStudentById();
                    break;
                case "3":
                    UpdateStudentById();
                    break;
                case "4":
                    DeleteStudentById();
                    Student.PrintAllStudents();
                    break;
                case "5":
                    Student.DisplayAcademicPerformancePercentage();
                    break;
                case "6":
                    Student.DisplayGpaPercentage();
                    break;
                case "7":
                    DisplayStudentsByPerformance();
                    break;
                case "0":
                    SaveStudentsToFile(Student.students, @"C:\DanhSachSinhVien\students.txt");
                    Console.WriteLine("Student list saved. Program terminated.");
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }
    static void CreateNewStudent()
    {
        string name = InputName();
        DateTime dateOfBirth = InputDateOfBirth();
        string address = InputAddress();
        float height = InputHeight();
        float weight = InputWeight();
        string studentId = InputStudentId();
        string currentSchool = InputCurrentSchool();
        int yearOfUniversityEntry = InputYearOfUniversityEntry();
        float gpa = InputGPA();

        // Create a new Student object
        Student newStudent = new Student(name, dateOfBirth, address, height, weight, studentId, currentSchool, yearOfUniversityEntry, gpa);

        // Print success message
        Console.WriteLine("Student added successfully:");
        // Console.WriteLine(newStudent.ToString());
    }

    //read
    static void SearchStudentById()
    {
        Console.Write("Enter the Student ID to search: ");
        string studentId = Console.ReadLine();

        bool found = false;
        for (int i = 0; i < Student.StudentCount; i++)
        {
            if (Student.GetStudentByIndex(i).GetStudentId().Equals(studentId, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Student found:");
                Console.WriteLine(Student.GetStudentByIndex(i).ToString());
                found = true;
                break;
            }
        }

        if (!found)
        {
            Console.WriteLine("No data found matching the given ID.");
        }
    }

    static void UpdateStudentById()
    {
        Console.Write("Enter the Student ID to update: ");
        string studentId = Console.ReadLine();

        Student studentToUpdate = null;

        for (int i = 0; i < Student.StudentCount; i++)
        {
            if (Student.GetStudentByIndex(i).GetStudentId().Equals(studentId, StringComparison.OrdinalIgnoreCase))
            {
                studentToUpdate = Student.GetStudentByIndex(i);
                break;
            }
        }

        if (studentToUpdate == null)
        {
            Console.WriteLine("No student found with the given ID.");
            return;
        }

        try
        {
            string name = InputName();
            DateTime dateOfBirth = InputDateOfBirth();
            string address = InputAddress();
            float height = InputHeight();
            float weight = InputWeight();
            string currentSchool = InputCurrentSchool();
            int yearOfUniversityEntry = InputYearOfUniversityEntry();
            float gpa = InputGPA();

            // Update student details
            studentToUpdate.UpdateDetails(name, dateOfBirth, address, height, weight, currentSchool, yearOfUniversityEntry, gpa);

            Console.WriteLine("Student details updated successfully:");
            Console.WriteLine(studentToUpdate.ToString());
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Error: {ex.Message}. Update failed.");
        }
    }

    static void DeleteStudentById()
    {
        Console.Write("Enter the Student ID to delete: ");
        string studentId = Console.ReadLine();

        Student studentToRemove = null;

        // Find the student to remove
        foreach (var student in Student.students)
        {
            if (student.GetStudentId().Equals(studentId, StringComparison.OrdinalIgnoreCase))
            {
                studentToRemove = student;
                break;
            }
        }

        if (studentToRemove == null)
        {
            Console.WriteLine("No student found with the given ID.");
            return;
        }

        // Remove the student
        Student.students.Remove(studentToRemove);

        // Update IDs of remaining students
        for (int i = 0; i < Student.StudentCount; i++)
        {
            Student.students[i].UpdateId(i + 1);
        }

        Console.WriteLine("Student deleted and IDs updated successfully.");
    }
    static void DisplayStudentsByPerformance()
    {
        while (true)
        {
            Console.WriteLine("\nEnter the academic performance (Poor, Weak, Average, Good, Excellent, Outstanding): ");
            string input = Console.ReadLine();

            if (Enum.TryParse(input, true, out AcademicPerformance performance))
            {
                Student.DisplayStudentsByPerformance(performance);
                break;
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid academic performance.");
            }
        }
    }
    static void SaveStudentsToFile(List<Student> students, string fileName)
    {
        // Ensure the directory exists
        string directory = Path.GetDirectoryName(fileName);
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        using (StreamWriter writer = new StreamWriter(fileName))
        {
            foreach (var student in students)
            {
                writer.WriteLine($"ID: {student.Id}");
                writer.WriteLine($"Name: {student.Name}");
                writer.WriteLine($"Date of Birth: {student.DateOfBirth:yyyy-MM-dd}");
                writer.WriteLine($"Address: {student.Address}");
                writer.WriteLine($"Height: {student.Height}");
                writer.WriteLine($"Weight: {student.Weight}");
                writer.WriteLine($"Current School: {student.CurrentSchool}");
                writer.WriteLine($"Year of University Entry: {student.YearOfUniversityEntry}");
                writer.WriteLine($"GPA: {student.GPA}");
                writer.WriteLine($"Academic Performance: {student.AcademicPerformance}");
                writer.WriteLine(new string('-', 20));
            }
        }
    }


    static string InputName()
    {
        while (true)
        {
            try
            {
                Console.Write("Name: ");
                string name = Console.ReadLine();
                Validate.Name(name);
                return name;
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error: {ex.Message}. Please enter the name again.");
            }
        }
    }

    static DateTime InputDateOfBirth()
    {
        while (true)
        {
            try
            {
                Console.Write("Date of Birth (yyyy-mm-dd): ");
                DateTime dateOfBirth = DateTime.Parse(Console.ReadLine());
                Validate.DateOfBirth(dateOfBirth);
                return dateOfBirth;
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error: {ex.Message}. Please enter the date of birth again.");
            }
            catch (FormatException)
            {
                Console.WriteLine("Format Error: Please enter a valid date.");
            }
        }
    }

    static string InputAddress()
    {
        while (true)
        {
            try
            {
                Console.Write("Address: ");
                string address = Console.ReadLine();
                Validate.Address(address);
                return address;
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error: {ex.Message}. Please enter the address again.");
            }
        }
    }

    static float InputHeight()
    {
        while (true)
        {
            try
            {
                Console.Write("Height (cm): ");
                float height = float.Parse(Console.ReadLine());
                Validate.Height(height);
                return height;
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error: {ex.Message}. Please enter the height again.");
            }
            catch (FormatException)
            {
                Console.WriteLine("Format Error: Please enter a valid number for height.");
            }
        }
    }

    static float InputWeight()
    {
        while (true)
        {
            try
            {
                Console.Write("Weight (kg): ");
                float weight = float.Parse(Console.ReadLine());
                Validate.Weight(weight);
                return weight;
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error: {ex.Message}. Please enter the weight again.");
            }
            catch (FormatException)
            {
                Console.WriteLine("Format Error: Please enter a valid number for weight.");
            }
        }
    }

    static string InputStudentId()
    {
        while (true)
        {
            try
            {
                Console.Write("Student ID: ");
                string studentId = Console.ReadLine();
                Validate.StudentId(studentId);

                // Check if the StudentId already exists
                if (Student.IsStudentIdDuplicate(studentId))
                {
                    Console.WriteLine("Error: Student ID already exists. Please enter a different Student ID.");
                }
                else
                {
                    return studentId;
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error: {ex.Message}. Please enter the student ID again.");
            }
        }
    }


    static string InputCurrentSchool()
    {
        while (true)
        {
            try
            {
                Console.Write("Current School: ");
                string currentSchool = Console.ReadLine();
                Validate.CurrentSchool(currentSchool);
                return currentSchool;
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error: {ex.Message}. Please enter the current school again.");
            }
        }
    }

    static int InputYearOfUniversityEntry()
    {
        while (true)
        {
            try
            {
                Console.Write("Year of University Entry: ");
                int yearOfUniversityEntry = int.Parse(Console.ReadLine());
                Validate.YearOfUniversityEntry(yearOfUniversityEntry);
                return yearOfUniversityEntry;
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error: {ex.Message}. Please enter the year of university entry again.");
            }
            catch (FormatException)
            {
                Console.WriteLine("Format Error: Please enter a valid number for the year of university entry.");
            }
        }
    }

    static float InputGPA()
    {
        while (true)
        {
            try
            {
                Console.Write("GPA: ");
                float gpa = float.Parse(Console.ReadLine());
                Validate.GPA(gpa);
                return gpa;
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error: {ex.Message}. Please enter the GPA again.");
            }
            catch (FormatException)
            {
                Console.WriteLine("Format Error: Please enter a valid number for GPA.");
            }
        }
    }
}
