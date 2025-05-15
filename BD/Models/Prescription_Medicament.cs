using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;


namespace BD.Models;

[Table("Prescription_Medicament")]
[PrimaryKey(nameof(IdMedicament), nameof(IdPrescription))]
public class Prescription_Medicament
{
    public int IdMedicament { get; set; }

    public Medicament Medicament { get; set; }

    public int IdPrescription { get; set; }

    public Prescription Prescription { get; set; }

    public int Dose { get; set; }

    [MaxLength(100)]
    public string Details { get; set; }
}