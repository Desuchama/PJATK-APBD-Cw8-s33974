namespace PJATK_APBD_Cw8_s33974.DTOs;

public record PatientResponseDto(
	string Pesel,
	string FirstName,
	string LastName,
	int Age,
	string Sex,
	IEnumerable<AdmissionDto> Admissions,
	IEnumerable<BedAssignmentDto> BedAssignments
);