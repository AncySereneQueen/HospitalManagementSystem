using HospitalManagement.Models;
namespace HospitalManagement.Interfaces;
public interface IBillingService
{
    Bill? Generate(int appointmentId);
    IReadOnlyList<Bill> GetAll();
}
