namespace PJATK_APBD_Cw8_s33974.DTOs;

public record AdmissionDto(
	int Id,
	DateTime Date,
	DateTime? DischargeDate,
	WardDto WardId
);