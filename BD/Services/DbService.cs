using BD.Data;
using BD.DTOs;
using BD.Interface;
using BD.Models;
using Microsoft.EntityFrameworkCore;

namespace BD.Services;

public class DbService(AppDbContext data) : IDbService
{
    public async Task<ICollection<AddPrescriptionRequest>> GetPrescriptionsDetailsAsync()
    {
        return await data.Prescriptions.Select(pr => new AddPrescriptionRequest
        {
            Date = pr.Date,
            DueDate = pr.DueDate,
            IdDoctor = pr.IdDoctor,
            Patient = new PatientDto
            {
                IdPatient = pr.Patient.IdPatient,
                FirstName = pr.Patient.FirstName,
                LastName = pr.Patient.LastName,
                Birthdate = pr.Patient.Birthdate
            },
            Medicaments = pr.Prescription_Medicaments.Select(pm => new PrescriptionMedicamentDto
            {
                IdMedicament = pm.IdMedicament,
                Dose = pm.Dose,
                Details = pm.Details
            }).ToList()
        })
        .ToListAsync();
    }
    
    public async Task AddPrescriptionAsync(AddPrescriptionRequest request)
    {
        if (request.Medicaments.Count > 10)
            throw new ArgumentException("Musi byc ponizej 10 lekow");

        if (request.DueDate < request.Date)
            throw new ArgumentException("Recepta wygasla");

        var doctor = await data.Doctors.FindAsync(request.IdDoctor);
        if (doctor == null)
            throw new ArgumentException("Podany lekarz nie istnieje.");

        var patient = await data.Patients.FindAsync(request.Patient.IdPatient);
        if (patient == null)
        {
            patient = new Patient
            {
                IdPatient = request.Patient.IdPatient,
                FirstName = request.Patient.FirstName,
                LastName = request.Patient.LastName,
                Birthdate = request.Patient.Birthdate
            };
            data.Patients.Add(patient);
            await data.SaveChangesAsync();
        }

        var prescription = new Prescription
        {
            Date = request.Date,
            DueDate = request.DueDate,
            IdDoctor = request.IdDoctor,
            IdPatient = patient.IdPatient
        };

        data.Prescriptions.Add(prescription);
        await data.SaveChangesAsync();

        foreach (var med in request.Medicaments)
        {
            var medicament = await data.Medicaments.FindAsync(med.IdMedicament);
            if (medicament == null)
                throw new ArgumentException($"Brak leku");

            var pm = new Prescription_Medicament
            {
                IdPrescription = prescription.IdPrescription,
                IdMedicament = med.IdMedicament,
                Dose = med.Dose,
                Details = med.Details
            };

            data.Prescription_Medicaments.Add(pm);
        }

        await data.SaveChangesAsync();
    }
    
    public async Task<GetPatientDetailsDto?> GetPatientDetailsAsync(int idPatient)
    {
        return await data.Patients
            .Where(p => p.IdPatient == idPatient)
            .Select(p => new GetPatientDetailsDto
            {
                IdPatient = p.IdPatient,
                FirstName = p.FirstName,
                LastName = p.LastName,
                Birthdate = p.Birthdate,
                Prescriptions = p.Prescriptions
                    .OrderBy(pr => pr.DueDate)
                    .Select(pr => new PrescriptionDto
                    {
                        IdPrescription = pr.IdPrescription,
                        Date = pr.Date,
                        DueDate = pr.DueDate,
                        Doctor = new DoctorDto
                        {
                            IdDoctor = pr.Doctor.IdDoctor,
                            FirstName = pr.Doctor.FirstName,
                            LastName = pr.Doctor.LastName,
                            Email = pr.Doctor.Email
                        },
                        Medicaments = pr.Prescription_Medicaments.Select(pm => new PrescriptionMedicamentDetailsDto
                        {
                            IdMedicament = pm.Medicament.IdMedicament,
                            Name = pm.Medicament.Name,
                            Description = pm.Medicament.Description,
                            Dose = pm.Dose
                        }).ToList()
                    }).ToList()
            })
            .FirstOrDefaultAsync();
    }
}