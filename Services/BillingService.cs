using HospitalManagement.Interfaces;
using HospitalManagement.Models;
namespace HospitalManagement.Services;

public class BillingService : IBillingService
{
    private readonly IAppointmentService _appointments;
    private readonly List<Bill> _bills = new();
    public BillingService(IAppointmentService appointments) => _appointments = appointments;
    public Bill? Generate(int appointmentId)
    {
        var appointment = _appointments.Find(appointmentId);
        if (appointment == null || _bills.Any(b => b.Appointment.Id == appointmentId))
            return null;
        var bill = new Bill(_bills.Count + 1, appointment, appointment.Doctor.Fee);
        _bills.Add(bill);
        return bill;
    }
    public IReadOnlyList<Bill> GetAll() => _bills.AsReadOnly();
}
