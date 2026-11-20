namespace HospitalManagement.Models;

public class Bill
{
    public int Id { get; }
    public Appointment Appointment { get; }
    public decimal Amount { get; }
    public Bill(int id, Appointment appointment, decimal amount)
    {

        Id = id;
        Appointment = appointment;
        Amount = amount;
    }
}
