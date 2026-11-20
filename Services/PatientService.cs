using HospitalManagement.Interfaces;
using HospitalManagement.Models;
namespace HospitalManagement.Services;

public class PatientService : IPatientService
{
    private readonly List<Patient> _patients = new();
    public Patient Register(string name, int age)
    {
        var patient = new Patient(_patients.Count + 1, name, age);
        _patients.Add(patient);
        return patient;
    }
    public Patient? Find(int id) => _patients.FirstOrDefault(p => p.Id == id);
    public IReadOnlyList<Patient> GetAll() => _patients.AsReadOnly();
}
