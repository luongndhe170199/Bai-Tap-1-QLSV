using System;

class Program
{
    static void Main()
    {
        while (true)
        {
            Console.WriteLine("\nChoose an option:");
            Console.WriteLine("1. Add a new student");
            Console.WriteLine("2. Search for a student by ID");
            Console.WriteLine("3. Exit");
            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    CreateNewStudent();
                    break;
                case "2":
                    SearchStudentById();
                    break;
                case "3":
                    return; // Exit the program
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
        Console.WriteLine(newStudent.ToString());
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
}
