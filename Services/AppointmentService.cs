using HospitalManagement.Interfaces;
using HospitalManagement.Models;
namespace HospitalManagement.Services;

public class AppointmentService : IAppointmentService
{
    private readonly IPatientService _patients;
    private readonly IDoctorService _doctors;
    private readonly List<Appointment> _appointments = new();

    public AppointmentService(IPatientService patients, IDoctorService doctors)
    {
        _patients = patients;
        _doctors = doctors;
    }
    public Appointment? Book(int patientId, int doctorId, DateTime date)
    {
        var patient = _patients.Find(patientId);
        var doctor = _doctors.Find(doctorId);
        if (patient == null || doctor == null || date <= DateTime.Now ||
            _appointments.Any(a => a.Doctor.Id == doctorId && a.Date == date))
            return null;
        var appointment = new Appointment(_appointments.Count + 1, patient, doctor, date);
        _appointments.Add(appointment);
        return appointment;
    }
    public Appointment? Find(int id) => _appointments.FirstOrDefault(a => a.Id == id);
    public IReadOnlyList<Appointment> GetAll() => _appointments.AsReadOnly();
}
