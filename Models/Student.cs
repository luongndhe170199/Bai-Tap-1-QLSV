using System;
using System.Collections.Generic;

public class Student : Person
{
    private string StudentId { get; set; }
    public string CurrentSchool { get; set; }
    public int YearOfUniversityEntry { get; set; }
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


        StudentId = studentId;
        CurrentSchool = currentSchool;
        YearOfUniversityEntry = yearOfUniversityEntry;
        GPA = gpa;
        UpdateAcademicPerformance();

        AddStudent(this);
    }
    private AcademicPerformance CalculateAcademicPerformance(float gpa)
    {
        switch (gpa)
        {
            case < 3.0f:
                return AcademicPerformance.Poor;
            case >= 3.0f and < 5.0f:
                return AcademicPerformance.Weak;
            case >= 5.0f and < 6.5f:
                return AcademicPerformance.Average;
            case >= 6.5f and < 7.5f:
                return AcademicPerformance.Good;
            case >= 7.5f and < 9.0f:
                return AcademicPerformance.Excellent;
            default:
                return AcademicPerformance.Outstanding;
        }
    }


    public void UpdateAcademicPerformance()
    {
        AcademicPerformance = CalculateAcademicPerformance(GPA);
    }

    public static void AddStudent(Student student)
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
