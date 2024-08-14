using System;

public class Person
{
    private static int _lastId = 0;

    private int Id { get; set; }  
    private string Name { get; set; }  
    private DateTime DateOfBirth { get; set; }  
    private string Address { get; set; }  
    private float Height { get; set; }  
    private float Weight { get; set; }  

    // Constructor for the Person class
    public Person(string name, DateTime dateOfBirth, string address, float height, float weight)
    {
        Validate.Name(name);
        Validate.DateOfBirth(dateOfBirth);
        Validate.Address(address);
        Validate.Height(height);
        Validate.Weight(weight);

        Id = ++_lastId;
        Name = name;
        DateOfBirth = dateOfBirth;
        Address = address;
        Height = height;
        Weight = weight;
    }

    public override string ToString()
    {
        return $"ID: {Id}, Name: {Name}, Date of Birth: {DateOfBirth.ToShortDateString()}, Address: {Address}, Height: {Height} cm, Weight: {Weight} kg";
    }
}