using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class StudentManage
{
    private static List<Student> students = new List<Student>();

    public static void CreateNewStudent()
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
        students.Add(newStudent);
        // save to file
        SaveStudentsToFile(Student.students, "students.txt");
        // Print success message
        Console.WriteLine("Student added successfully:");
        // Console.WriteLine(newStudent.ToString());
    }
    public static Student FindStudentById(string studentId)
    {
        return Student.students.FirstOrDefault(s => s.GetStudentId().Equals(studentId, StringComparison.OrdinalIgnoreCase));
    }


    public static void SearchStudentById()
    {
        Console.Write("Enter the Student ID to search: ");
        string studentId = Console.ReadLine();

        Student student = FindStudentById(studentId);

        if (student != null)
        {
            Console.WriteLine("Student found:");
            Console.WriteLine(student.ToString());
        }
        else
        {
            Console.WriteLine("No data found matching the given ID.");
        }
    }


    public static void UpdateStudentById()
    {
        Console.Write("Enter the Student ID to update: ");
        string studentId = Console.ReadLine();

        Student studentToUpdate = FindStudentById(studentId);

        if (studentToUpdate == null)
        {
            Console.WriteLine("No student found with the given ID.");
            return;
        }

        while (true)
        {
            Console.WriteLine("\nSelect the field you want to update:");
            Console.WriteLine("1. Name");
            Console.WriteLine("2. Date of Birth");
            Console.WriteLine("3. Address");
            Console.WriteLine("4. Height");
            Console.WriteLine("5. Weight");
            Console.WriteLine("6. Current School");
            Console.WriteLine("7. Year of University Entry");
            Console.WriteLine("8. GPA");
            Console.WriteLine("0. Exit update");
            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine();

            try
            {
                switch (choice)
                {
                    case "1":
                        studentToUpdate.Name = InputName();
                        break;
                    case "2":
                        studentToUpdate.DateOfBirth = InputDateOfBirth();
                        break;
                    case "3":
                        studentToUpdate.Address = InputAddress();
                        break;
                    case "4":
                        studentToUpdate.Height = InputHeight();
                        break;
                    case "5":
                        studentToUpdate.Weight = InputWeight();
                        break;
                    case "6":
                        studentToUpdate.CurrentSchool = InputCurrentSchool();
                        break;
                    case "7":
                        studentToUpdate.YearOfUniversityEntry = InputYearOfUniversityEntry();
                        break;
                    case "8":
                        studentToUpdate.GPA = InputGPA();
                        studentToUpdate.UpdateAcademicPerformance();
                        break;
                    case "0":
                        Console.WriteLine("Update finished.");
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }

                Console.WriteLine("Student details updated successfully:");
                Console.WriteLine(studentToUpdate.ToString());
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error: {ex.Message}. Update failed.");
            }
        }
    }


    //delete
    public static void DeleteStudentById()
    {
        Console.Write("Enter the Student ID to delete: ");
        string studentId = Console.ReadLine();

        Student studentToRemove = FindStudentById(studentId);

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


    public static void DisplayStudentsByPerformance()
    {
        while (true)
        {
            Console.WriteLine("\nEnter the academic performance (Poor, Weak, Average, Good, Excellent, Outstanding): ");
            string input = Console.ReadLine();

            if (Enum.TryParse(input, true, out AcademicPerformance performance))
            {
                DisplayStudentsByPerformance(performance);
                break;
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid academic performance.");
            }
        }
    }

    // Method to calculate and display the percentage of students in each academic performance category
    public static void DisplayAcademicPerformancePercentage()
    {
        if (Student.students.Count == 0)
        {
            Console.WriteLine("No students in the list.");
            return;
        }

        // Group students by academic performance and calculate the percentage
        var performanceGroups = Student.students
            .GroupBy(student => student.AcademicPerformance)
            .Select(group => new
            {
                Performance = group.Key,
                Percentage = (double)group.Count() / Student.students.Count * 100
            })
            .OrderByDescending(result => result.Percentage)
            .ToList();

        // Display the results
        foreach (var group in performanceGroups)
        {
            Console.WriteLine($"{group.Performance}: {group.Percentage:F2}%");
        }
    }

    // Method to calculate and display the percentage of students based on their GPA scores
    public static void DisplayGpaPercentage()
    {
        if (Student.students.Count == 0)
        {
            Console.WriteLine("No students in the list.");
            return;
        }

        // Dictionary to store the frequency of each GPA score
        Dictionary<float, int> gpaCount = new Dictionary<float, int>();

        // Count the frequency of each GPA
        foreach (var student in Student.students)
        {
            if (gpaCount.ContainsKey(student.GPA))
            {
                gpaCount[student.GPA]++;
            }
            else
            {
                gpaCount[student.GPA] = 1;
            }
        }

        // Calculate and display the percentage of each GPA
        foreach (var entry in gpaCount)
        {
            float percentage = (float)entry.Value / Student.students.Count * 100;
            Console.WriteLine($"GPA: {entry.Key}: {percentage:F2}%");
        }
    }
    // Method to display students based on the specified academic performance
    public static void DisplayStudentsByPerformance(AcademicPerformance performance)
    {
        var studentsByPerformance = Student.students.Where(student => student.AcademicPerformance == performance).ToList();

        if (studentsByPerformance.Count == 0)
        {
            Console.WriteLine($"No students found with {performance} performance.");
        }
        else
        {
            Console.WriteLine($"\nStudents with {performance} performance:");
            foreach (var student in studentsByPerformance)
            {
                Console.WriteLine(student.ToString());
            }
        }
    }

    //save to file
    public static void SaveStudentsToFile(List<Student> students, string fileName)
    {
        // Get the base directory of the application
        string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

        // Combine the base directory with the file name
        string fullPath = Path.Combine(baseDirectory, fileName);

        // Ensure the directory exists (usually the base directory is already created)
        Directory.CreateDirectory(baseDirectory);

        using (StreamWriter writer = new StreamWriter(fullPath))
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

        Console.WriteLine($"Student list saved to {fullPath}");
    }



    // method input
    private static string InputName()
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

    private static DateTime InputDateOfBirth()
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

    private static string InputAddress()
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

    private static float InputHeight()
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

    private static float InputWeight()
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

    private static string InputStudentId()
    {
        while (true)
        {
            try
            {
                Console.Write("Student ID: ");
                string studentId = Console.ReadLine();
                Validate.StudentId(studentId);

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

    private static string InputCurrentSchool()
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

    private static int InputYearOfUniversityEntry()
    {
        while (true)
        {
            try
            {
                Console.Write("Year of University Entry: ");
                int yearOfEntry = int.Parse(Console.ReadLine());
                Validate.YearOfUniversityEntry(yearOfEntry);
                return yearOfEntry;
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error: {ex.Message}. Please enter the year of university entry again.");
            }
            catch (FormatException)
            {
                Console.WriteLine("Format Error: Please enter a valid year.");
            }
        }
    }

    private static float InputGPA()
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
