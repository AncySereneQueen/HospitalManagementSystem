namespace HospitalManagement.Models;

public abstract class Person
{
    public int Id { get; }
    public string Name { get; private set; }

    protected Person(int id, string name)
    {
        Id = id;
        Name = name.Trim();
    }
}
