using HospitalManagement.Interfaces;
using HospitalManagement.Models;
namespace HospitalManagement.Services;

public class DoctorService : IDoctorService
{
    private readonly List<Doctor> _doctors = new();
    public Doctor Add(string name, string specialization, decimal fee)
    {
        var doctor = new Doctor(_doctors.Count + 1, name, specialization, fee);
        _doctors.Add(doctor);
        return doctor;
    }
    public Doctor? Find(int id) => _doctors.FirstOrDefault(d => d.Id == id);
    public IReadOnlyList<Doctor> GetAll() => _doctors.AsReadOnly();
}
