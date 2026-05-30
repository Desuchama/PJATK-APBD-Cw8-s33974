using Microsoft.EntityFrameworkCore;
using PJATK_APBD_Cw8_s33974.DTOs;
using PJATK_APBD_Cw8_s33974.Infrastructure;

namespace PJATK_APBD_Cw8_s33974.Services;

public class PatientService(MasterContext ctx) : IPatientService
{
	public async Task<IEnumerable<PatientResponseDto>> GetAllAsync(string? search)
	{
		return await ctx.Patients
			.Where(p => p.FirstName.Contains(search) || p.LastName.Contains(search) || search == null)
			.Select(p => new PatientResponseDto(
				p.Pesel,
				p.FirstName,
				p.LastName,
				p.Age,
				p.Sex ? "Male" : "Female",
				p.Admissions.Select(adm => new AdmissionDto(
					adm.Id,
					adm.AdmissionDate,
					adm.DischargeDate,
					new WardDto(
						adm.Ward.Id,
						adm.Ward.Name,
						adm.Ward.Description)
					)
				),
				p.BedAssignments.Select(ba => new BedAssignmentDto(
					ba.Id,
					ba.From,
					ba.To,
					new BedDto(
						ba.BedId,
						new BedTypeDto(
							ba.Bed.BedType.Id,
							ba.Bed.BedType.Name,
							ba.Bed.BedType.Description
						),
						new RoomDto(
							ba.Bed.Room.Id,
							ba.Bed.Room.HasTv,
							new WardDto(
								ba.Bed.Room.Ward.Id,
								ba.Bed.Room.Ward.Name,
								ba.Bed.Room.Ward.Description)
						)
					)
				)
			)))
			.ToListAsync();
	}

	public async Task<AddBedAssignmentDto> AddAsync(AddBedAssignmentDto request)
	{
		throw new NotImplementedException();
	}
}