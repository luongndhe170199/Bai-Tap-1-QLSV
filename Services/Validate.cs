using System;
public static class Validate
{
    public static void Name(string name)
    {
        if (string.IsNullOrWhiteSpace(name) || name.Length > Constants.NameMaxLength)
            throw new ArgumentException($"Name must be between 1 and {Constants.NameMaxLength} characters.");
    }

    public static void DateOfBirth(DateTime dateOfBirth)
    {
        if (dateOfBirth.Year <= Constants.MinYearOfBirth)
            throw new ArgumentException($"Date of birth must be after {Constants.MinYearOfBirth}.");
    }

    public static void Address(string address)
    {
        if (address != null && address.Length > Constants.AddressMaxLength)
            throw new ArgumentException($"Address must be less than {Constants.AddressMaxLength} characters.");
    }

    public static void Height(float height)
    {
        if (height < Constants.MinHeight || height > Constants.MaxHeight)
            throw new ArgumentException($"Height must be between {Constants.MinHeight} and {Constants.MaxHeight} cm.");
    }

    public static void Weight(float weight)
    {
        if (weight < Constants.MinWeight || weight > Constants.MaxWeight)
            throw new ArgumentException($"Weight must be between {Constants.MinWeight} and {Constants.MaxWeight} kg.");
    }

    public static void StudentId(string studentId)
    {
        if (string.IsNullOrWhiteSpace(studentId) || studentId.Length != Constants.StudentIdLength)
            throw new ArgumentException($"Student ID must be {Constants.StudentIdLength} characters long.");
    }

    public static void CurrentSchool(string currentSchool)
    {
        if (string.IsNullOrWhiteSpace(currentSchool) || currentSchool.Length > Constants.CurrentSchoolMaxLength)
            throw new ArgumentException($"Current School must be less than {Constants.CurrentSchoolMaxLength} characters.");
    }

    public static void YearOfUniversityEntry(int yearOfUniversityEntry)
    {
        int currentYear = DateTime.Now.Year;

        if (yearOfUniversityEntry <= Constants.MinYearOfUniversityEntry)
            throw new ArgumentException($"Year of university entry must be after {Constants.MinYearOfUniversityEntry}.");

        if (yearOfUniversityEntry > currentYear)
            throw new ArgumentException($"Year of university entry cannot be in the future. Please enter a year no later than {currentYear}.");
    }


    public static void GPA(float gpa)
    {
        if (gpa < Constants.MinGPA || gpa > Constants.MaxGPA)
            throw new ArgumentException($"GPA must be between {Constants.MinGPA} and {Constants.MaxGPA}.");
    }
}
