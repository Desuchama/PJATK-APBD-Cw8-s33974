using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using PJATK_APBD_Cw8_s33974.DTOs;
using PJATK_APBD_Cw8_s33974.Entities;
using PJATK_APBD_Cw8_s33974.Exceptions;
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

	public async Task<AddBedAssignmentDto> AddAsync(AddBedAssignmentDto request, string id)
	{
		if (await ctx.Beds
			    .Where(b => b.BedType.Name == request.BedType)
			    .Where(b => b.Room.Ward.Name == request.Ward)
			    .AllAsync(b => b.BedAssignments
				    .Any(ba => ba.From <= request.From && (ba.To >= request.From || ba.To == null) ||
				                 ba.From <= request.To && (ba.To >= request.From || ba.To == null) )))
		{
			throw new ConflictException("No free bed found");
		}

		var bed = await ctx.Beds
			.Where(b => b.BedType.Name == request.BedType)
			.Where(b => b.Room.Ward.Name == request.Ward)
			.FirstOrDefaultAsync(b => b.BedAssignments.Any(ba =>
				!(ba.From <= request.From && ba.To >= request.From ||
				  ba.From <= request.To && ba.To >= request.From)));

		// if (bed == null)
		// {
		// 	throw new ConflictException("No free bed found");
		// }
		// else
		// {
			var assignment = new BedAssignment()
			{	
				PatientPesel = id,
				BedId = bed.Id,
				From = request.From,
				To = request.To,
			};
			await ctx.BedAssignments.AddAsync(assignment);
		// }
		
		await ctx.SaveChangesAsync();
		
		return null;
	}
}