using PJATK_APBD_Cw8_s33974.DTOs;

namespace PJATK_APBD_Cw8_s33974.Services;

public interface IPatientService
{
	Task<IEnumerable<PatientResponseDto>> GetAllAsync(string? search);
	Task<AddBedAssignmentDto> AddAsync(AddBedAssignmentDto request);
}