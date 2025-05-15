using BD.DTOs;

namespace BD.Interface;

public interface IDbService
{
    public Task<ICollection<AddPrescriptionRequest>> GetPrescriptionsDetailsAsync();
    public Task AddPrescriptionAsync(AddPrescriptionRequest request);
    Task<GetPatientDetailsDto?> GetPatientDetailsAsync(int idPatient);
}