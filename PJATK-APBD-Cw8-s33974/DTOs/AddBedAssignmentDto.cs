using System.ComponentModel.DataAnnotations;

namespace PJATK_APBD_Cw8_s33974.DTOs;

public record AddBedAssignmentDto(
	[Required]
	DateTime From,
	DateTime? To,
	[Required]
	string BedType,
	[Required]
	string Ward
);