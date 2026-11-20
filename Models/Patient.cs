namespace HospitalManagement.Models;

public class Patient : Person
{
    public int Age { get; }
    public Patient(int id, string name, int age) : base(id, name)
    {
        Age = age;
    }
}
