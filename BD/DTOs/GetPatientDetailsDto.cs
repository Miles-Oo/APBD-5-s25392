﻿namespace BD.DTOs;

public class GetPatientDetailsDto
{
    public int IdPatient { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime Birthdate { get; set; }

    public List<PrescriptionDto> Prescriptions { get; set; }
}