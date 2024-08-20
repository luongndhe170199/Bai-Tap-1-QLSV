using System;

public class Program
{
    public static void Main()
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
                 StudentManage.CreateNewStudent();
                 Student.PrintAllStudents();
                 break;
             case "2":
                 StudentManage.SearchStudentById();
                 break;
             case "3":
                 StudentManage.UpdateStudentById();
                 break;
             case "4":
                 StudentManage.DeleteStudentById();
                 Student.PrintAllStudents();
                 break;
             case "5":
                 StudentManage.DisplayAcademicPerformancePercentage();
                 break;
             case "6":
                 StudentManage.DisplayGpaPercentage();
                 break;
             case "7":
                 StudentManage.DisplayStudentsByPerformance();
                 break;
             case "0":
                 StudentManage.SaveStudentsToFile(Student.students, "students.txt");
                 Console.WriteLine("Student list saved. Program terminated.");
                 return;
             default:
                 Console.WriteLine("Invalid choice. Please try again.");
                 break;
         }
     }
 }
}
