using HospitalManagement.Models;
namespace HospitalManagement.Interfaces;
public interface IAppointmentService
{
    Appointment? Book(int patientId, int doctorId, DateTime date);
    IReadOnlyList<Appointment> GetAll();
    Appointment? Find(int id);
}
