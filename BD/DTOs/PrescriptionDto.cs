﻿namespace BD.DTOs;

public class PrescriptionDto
{
    public int IdPrescription { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
    public DoctorDto Doctor { get; set; }
    public List<PrescriptionMedicamentDetailsDto> Medicaments { get; set; }
}