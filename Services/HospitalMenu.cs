using System.Globalization;
using HospitalManagement.Interfaces;
using HospitalManagement.Services;

namespace HospitalManagement.Services;

public class HospitalMenu
{
    private readonly IPatientService _patients;
    private readonly IDoctorService _doctors;
    private readonly IAppointmentService _appointments;
    private readonly IBillingService _billing;

    public HospitalMenu()
    {
        _patients = new PatientService();
        _doctors = new DoctorService();
        _appointments = new AppointmentService(_patients, _doctors);
        _billing = new BillingService(_appointments);
        LoadSampleData();
    }

    private void LoadSampleData()
    {
        // Default patients
        _patients.Register("Anu", 28);
        _patients.Register("Rahul", 35);

        // Default doctors
        _doctors.Add("Dr. Arun", "Cardiology", 500);
        _doctors.Add("Dr. Meera", "Neurology", 700);
    }

    public void Display()
    {
        while (true)
        {
            DisplayHeader("HOSPITAL MANAGEMENT SYSTEM");
            Console.WriteLine("1. Register Patient");
            Console.WriteLine("2. View Patients");
            Console.WriteLine("3. Add Doctor");
            Console.WriteLine("4. View Doctors");
            Console.WriteLine("5. Book Appointment");
            Console.WriteLine("6. View Appointments");
            Console.WriteLine("7. Generate Bill");
            Console.WriteLine("8. View Bills");
            Console.WriteLine("0. Exit");
            Console.Write("\nChoose an option: ");
            string choice = Console.ReadLine() ?? "";
            Console.WriteLine();
            if (choice == "0") { Console.WriteLine("Thank you!"); return; }
            switch (choice)
            {
                case "1": RegisterPatient(); break;
                case "2": ShowPatients(); break;
                case "3": AddDoctor(); break;
                case "4": ShowDoctors(); break;
                case "5": BookAppointment(false); break;
                case "6": ShowAppointments(); break;
                case "7": GenerateBill(); break;
                case "8": ShowBills(); break;
                default: Console.WriteLine("Invalid option. Try again."); break;
            }
        }
    }

    private static void DisplayHeader(string title)
    {
        Console.WriteLine();
        Console.WriteLine(new string('=', 50));
        Console.WriteLine("  " + title);
        Console.WriteLine(new string('=', 50));
        Console.WriteLine();
    }

    private static string GetInput(string label)
    {
        Console.Write(label + ": ");
        return Console.ReadLine()?.Trim() ?? "";
    }

    private static int GetInputNumber(string label, int min, int max)
    {
        while (true)
        {
            if (int.TryParse(GetInput(label), out int value) && value >= min && value <= max)
                return value;
            Console.WriteLine($"Please enter a number between {min} and {max}.");
        }
    }

    private static decimal GetInputFee(string label)
    {
        while (true)
        {
            if (decimal.TryParse(GetInput(label), out decimal value) && value >= 0)
                return value;
            Console.WriteLine("Please enter a valid non-negative fee.");
        }
    }

    private static string GetInputRequired(string label)
    {
        while (true)
        {
            string value = GetInput(label);
            if (!string.IsNullOrWhiteSpace(value)) return value;
            Console.WriteLine("This field is required.");
        }
    }

    private static DateTime GetInputDate(string label)
    {
        while (true)
        {
            string input = GetInput(label);
            if (DateTime.TryParseExact(input, "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture,
                DateTimeStyles.None, out DateTime date) && date > DateTime.Now)
                return date;
            Console.WriteLine("Enter a future date in yyyy-MM-dd HH:mm format.");
        }
    }

    private void RegisterPatient()
    {
        DisplayHeader("REGISTER PATIENT");
        var patient = _patients.Register(GetInputRequired("Patient name"), GetInputNumber("Age", 0, 120));
        Console.WriteLine($"\nPatient registered successfully! ID: {patient.Id}\n");
        BookAppointment(true);
    }
    private void ShowPatients()
    {
        DisplayHeader("PATIENT LIST");
        if (_patients.GetAll().Count == 0) Console.WriteLine("No patients registered.");
        foreach (var patient in _patients.GetAll())
            Console.WriteLine($"ID: {patient.Id} | Name: {patient.Name} | Age: {patient.Age}");
        Console.WriteLine();
    }
    private void AddDoctor()
    {
        DisplayHeader("ADD DOCTOR");
        var doctor = _doctors.Add(GetInputRequired("Doctor name"), GetInputRequired("Specialization"), GetInputFee("Consultation fee"));
        Console.WriteLine($"\nDoctor added successfully! ID: {doctor.Id}\n");
    }
    private void ShowDoctors()
    {
        DisplayHeader("DOCTOR LIST");
        if (_doctors.GetAll().Count == 0) Console.WriteLine("No doctors added.");
        foreach (var doctor in _doctors.GetAll())
            Console.WriteLine($"ID: {doctor.Id} | Dr. {doctor.Name} | {doctor.Specialization} | Fee: {doctor.Fee:F2}");
        Console.WriteLine();
    }
    private void BookAppointment(bool withReg)
    {
        DisplayHeader("BOOK APPOINTMENT");
        if(!withReg)
        {
            ShowPatients();
        }
        ShowDoctors();
        int patientId = GetInputNumber("Patient ID", 1, int.MaxValue);
        int doctorId = GetInputNumber("Doctor ID", 1, int.MaxValue);
        DateTime date = GetInputDate("Appointment date and time (yyyy-MM-dd HH:mm)");
        var appointment = _appointments.Book(patientId, doctorId, date);
        if (appointment == null)
        {
            Console.WriteLine("Invalid patient/doctor ID or doctor already booked at that time.");
            string choice = GetInput("Would you like to try again? (y/n)");

            if (!choice.Equals("y", StringComparison.OrdinalIgnoreCase))
            {
                return;
            }
            else
            {
                BookAppointment(withReg);
            }
        }
        else
            Console.WriteLine($"\nAppointment booked successfully! ID: {appointment.Id}\n");
    }
    private void ShowAppointments()
    {
        DisplayHeader("APPOINTMENT LIST");
        if (_appointments.GetAll().Count == 0) Console.WriteLine("No appointments booked.");
        foreach (var appointment in _appointments.GetAll())
            Console.WriteLine($"ID: {appointment.Id} | {appointment.Date:yyyy-MM-dd HH:mm} | Patient: {appointment.Patient.Name} | Doctor: {appointment.Doctor.Name}");
        Console.WriteLine();
    }
    private void GenerateBill()
    {
        DisplayHeader("GENERATE BILL");
        ShowAppointments();
        var bill = _billing.Generate(GetInputNumber("Appointment ID", 1, int.MaxValue));
        if (bill == null)
            Console.WriteLine("Invalid appointment ID or bill already generated.");
        else
            Console.WriteLine($"\nBill #{bill.Id} | Patient: {bill.Appointment.Patient.Name} | Amount: {bill.Amount:F2}\n");
    }
    private void ShowBills()
    {
        DisplayHeader("BILL LIST");
        if (_billing.GetAll().Count == 0) Console.WriteLine("No bills generated.");
        foreach (var bill in _billing.GetAll())
            Console.WriteLine($"Bill #{bill.Id} | Patient: {bill.Appointment.Patient.Name} | Amount: {bill.Amount:F2}");
        Console.WriteLine();
    }
}
