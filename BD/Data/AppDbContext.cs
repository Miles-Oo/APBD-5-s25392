using Microsoft.EntityFrameworkCore;
using BD.Models;

namespace BD.Data;

public class AppDbContext : DbContext
{
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Medicament> Medicaments { get; set; }
    public DbSet<Prescription> Prescriptions { get; set; }
    public DbSet<Prescription_Medicament> Prescription_Medicaments { get; set; }
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // Pacjenci
        modelBuilder.Entity<Patient>().HasData(
            new Patient { IdPatient = 1, FirstName = "Jan", LastName = "Kowalski", Birthdate = new DateTime(1985, 1, 1) },
            new Patient { IdPatient = 2, FirstName = "Anna", LastName = "Nowak", Birthdate = new DateTime(1990, 5, 10) }
        );

        // Lekarze
        modelBuilder.Entity<Doctor>().HasData(
            new Doctor { IdDoctor = 1, FirstName = "Adam", LastName = "Wolski", Email = "adam.wolski@clinic.com" },
            new Doctor { IdDoctor = 2, FirstName = "Maria", LastName = "Zielińska", Email = "maria.zielinska@clinic.com" }
        );

        // Leki
        modelBuilder.Entity<Medicament>().HasData(
            new Medicament { IdMedicament = 1, Name = "Paracetamol", Description = "Przeciwbólowy", Type = "Tabletka" },
            new Medicament { IdMedicament = 2, Name = "Ibuprofen", Description = "Przeciwzapalny", Type = "Kapsułka" }
        );

        // Recepty
        modelBuilder.Entity<Prescription>().HasData(
            new Prescription { IdPrescription = 1, Date = new DateTime(2025, 5, 1), DueDate = new DateTime(2025, 6, 1), IdPatient = 1, IdDoctor = 1 },
            new Prescription { IdPrescription = 2, Date = new DateTime(2025, 5, 2), DueDate = new DateTime(2025, 6, 2), IdPatient = 1, IdDoctor = 2 },
            new Prescription { IdPrescription = 3, Date = new DateTime(2025, 5, 3), DueDate = new DateTime(2025, 6, 3), IdPatient = 2, IdDoctor = 1 }
        );

        // Recepty - leki
        modelBuilder.Entity<Prescription_Medicament>().HasData(
            new Prescription_Medicament { IdMedicament = 1, IdPrescription = 1, Dose = 2, Details = "Po jedzeniu" },
            new Prescription_Medicament { IdMedicament = 2, IdPrescription = 1, Dose = 1, Details = "Rano" },
            new Prescription_Medicament { IdMedicament = 1, IdPrescription = 2, Dose = 3, Details = "Na noc" },
            new Prescription_Medicament { IdMedicament = 2, IdPrescription = 3, Dose = 1, Details = "Co 8h" }
        );
    }
}