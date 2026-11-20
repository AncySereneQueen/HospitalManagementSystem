using HospitalManagement.Models;
namespace HospitalManagement.Interfaces;
public interface IPatientService
{
    Patient Register(string name, int age);
    Patient? Find(int id);
    IReadOnlyList<Patient> GetAll();
}
