using System;
public class Student : Person
{
    private string StudentId { get; set; }  
    private string CurrentSchool { get; set; }  
    private int YearOfUniversityEntry { get; set; }  
    private float GPA { get; set; }  

    private static Student[] students = new Student[100];  
    private static int studentCount = 0;

    public static int StudentCount => studentCount;

    public static Student GetStudentByIndex(int index)
    {
        if (index >= 0 && index < studentCount)
        {
            return students[index];
        }
        return null;
    }

    // Constructor for the Student class
    public Student(string name, DateTime dateOfBirth, string address, float height, float weight, string studentId, string currentSchool, int yearOfUniversityEntry, float gpa)
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

        AddStudent(this);
    }

    private static void AddStudent(Student student)
    {
        if (studentCount < students.Length)
        {
            students[studentCount] = student;
            studentCount++;
        }
        else
        {
            Console.WriteLine("Student array is full!");
        }
    }
    // Method to print all students
    public static void PrintAllStudents()
    {
        for (int i = 0; i < studentCount; i++)
        {
            Console.WriteLine(students[i].ToString());
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
        return $"{base.ToString()}, Student ID: {StudentId}, School: {CurrentSchool}, Year of Entry: {YearOfUniversityEntry}, GPA: {GPA:F2}";
    }
}