using System;
using System.Collections.Generic;
public enum AcademicPerformance
{
    Poor,
    Weak,
    Average,
    Good,
    Excellent,
    Outstanding
}

public class Student : Person
{
    private string StudentId { get; set; }
    public string CurrentSchool { get; private set; }
    public int YearOfUniversityEntry { get; private set; }
    public float GPA { get; set; }
    public AcademicPerformance AcademicPerformance { get; private set; }



    public static List<Student> students = new List<Student>();
    public static int StudentCount => students.Count;

    public static Student GetStudentByIndex(int index)
    {
        if (index >= 0 && index < students.Count)
        {
            return students[index];
        }
        return null;
    }

    // Constructor for the Student class
    public Student(string name, DateTime dateOfBirth, string address, float height, float weight,
                   string studentId, string currentSchool, int yearOfUniversityEntry, float gpa)
        : base(name, dateOfBirth, address, height, weight)
    {
        Validate.StudentId(studentId);
        Validate.CurrentSchool(currentSchool);
        Validate.YearOfUniversityEntry(yearOfUniversityEntry);
        Validate.GPA(gpa);

        if (IsStudentIdDuplicate(studentId))
            throw new ArgumentException("Student ID already exists.");

        StudentId = studentId;
        CurrentSchool = currentSchool;
        YearOfUniversityEntry = yearOfUniversityEntry;
        GPA = gpa;
        UpdateAcademicPerformance();

        AddStudent(this);
    }
    private void UpdateAcademicPerformance()
    {
        if (GPA < 3.0f)
            AcademicPerformance = AcademicPerformance.Poor;
        else if (GPA >= 3.0f && GPA < 5.0f)
            AcademicPerformance = AcademicPerformance.Weak;
        else if (GPA >= 5.0f && GPA < 6.5f)
            AcademicPerformance = AcademicPerformance.Average;
        else if (GPA >= 6.5f && GPA < 7.5f)
            AcademicPerformance = AcademicPerformance.Good;
        else if (GPA >= 7.5f && GPA < 9.0f)
            AcademicPerformance = AcademicPerformance.Excellent;
        else
            AcademicPerformance = AcademicPerformance.Outstanding;
    }

    private static void AddStudent(Student student)
    {
        students.Add(student);
    }

    // Method to print all students
    public static void PrintAllStudents()
    {
        foreach (var student in students)
        {
            Console.WriteLine(student.ToString());
        }
    }

    public void UpdateDetails(string name, DateTime dateOfBirth, string address, float height, float weight, string currentSchool, int yearOfUniversityEntry, float gpa)
    {
        Validate.Name(name);
        Validate.DateOfBirth(dateOfBirth);
        Validate.Address(address);
        Validate.Height(height);
        Validate.Weight(weight);
        Validate.CurrentSchool(currentSchool);
        Validate.YearOfUniversityEntry(yearOfUniversityEntry);
        Validate.GPA(gpa);

        base.Name = name;
        base.DateOfBirth = dateOfBirth;
        base.Address = address;
        base.Height = height;
        base.Weight = weight;
        this.CurrentSchool = currentSchool;
        this.YearOfUniversityEntry = yearOfUniversityEntry;
        this.GPA = gpa;
        UpdateAcademicPerformance();
    }


    // Method to calculate and display the percentage of students in each academic performance category
    public static void DisplayAcademicPerformancePercentage()
    {
        if (students.Count == 0)
        {
            Console.WriteLine("No students in the list.");
            return;
        }

        // Group students by academic performance and calculate the percentage
        var performanceGroups = students
            .GroupBy(student => student.AcademicPerformance)
            .Select(group => new
            {
                Performance = group.Key,
                Percentage = (double)group.Count() / students.Count * 100
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
        if (students.Count == 0)
        {
            Console.WriteLine("No students in the list.");
            return;
        }

        // Dictionary to store the frequency of each GPA score
        Dictionary<float, int> gpaCount = new Dictionary<float, int>();

        // Count the frequency of each GPA
        foreach (var student in students)
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
            float percentage = (float)entry.Value / students.Count * 100;
            Console.WriteLine($"GPA: {entry.Key}: {percentage:F2}%");
        }
    }
    // Method to display students based on the specified academic performance
    public static void DisplayStudentsByPerformance(AcademicPerformance performance)
    {
        var studentsByPerformance = students.Where(student => student.AcademicPerformance == performance).ToList();

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

    public static bool IsStudentIdDuplicate(string studentId)
    {
        foreach (var student in students)
        {
            if (student != null && student.StudentId.Equals(studentId, StringComparison.OrdinalIgnoreCase))
                return true;
        }
        return false;
    }

    // Public method to get the StudentId
    public string GetStudentId()
    {
        return StudentId;
    }

    public override string ToString()
    {
        return $"{base.ToString()}, Student ID: {StudentId}, School: {CurrentSchool}, Year of Entry: {YearOfUniversityEntry}, GPA: {GPA:F2}, Performance: {AcademicPerformance}";
    }
}
