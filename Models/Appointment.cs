namespace HospitalManagement.Models;

public class Appointment
{
    public int Id { get; }
    public Patient Patient { get; }
    public Doctor Doctor { get; }
    public DateTime Date { get; }
    public Appointment(int id, Patient patient, Doctor doctor, DateTime date)
    {

        Id = id;
        Patient = patient;
        Doctor = doctor;
        Date = date;
    }
}
