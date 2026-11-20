using HospitalManagement.Models;
namespace HospitalManagement.Interfaces;
public interface IDoctorService
{
    Doctor Add(string name, string specialization, decimal fee);
    Doctor? Find(int id);
    IReadOnlyList<Doctor> GetAll();
}
