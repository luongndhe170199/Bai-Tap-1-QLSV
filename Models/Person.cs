using System;

public class Person
{
    private static int _lastId = 0;

    public int Id { get; private set; }
    public string Name { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Address { get; set; }
    public float Height { get; set; }
    public float Weight { get; set; }

    // Constructor for the Person class
    public Person(string name, DateTime dateOfBirth, string address, float height, float weight)
    {

        Id = ++_lastId;
        Name = name;
        DateOfBirth = dateOfBirth;
        Address = address;
        Height = height;
        Weight = weight;
    }
    public void UpdateId(int newId)
    {
        Id = newId;
    }
    public override string ToString()
    {
        return $"ID: {Id}, Name: {Name}, Date of Birth: {DateOfBirth.ToShortDateString()}, Address: {Address}, Height: {Height} cm, Weight: {Weight} kg";
    }
}