namespace HospitalManagement.Models;

public class Doctor : Person
{
    public string Specialization { get; }
    public decimal Fee { get; }
    public Doctor(int id, string name, string specialization, decimal fee) : base(id, name)
    {
        Specialization = specialization.Trim();
        Fee = fee;
    }
}
