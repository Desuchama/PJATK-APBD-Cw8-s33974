using Microsoft.AspNetCore.Mvc;
using PJATK_APBD_Cw8_s33974.DTOs;
using PJATK_APBD_Cw8_s33974.Exceptions;
using PJATK_APBD_Cw8_s33974.Services;

namespace PJATK_APBD_Cw8_s33974.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class PatientsController(IPatientService service) : Controller
{
	[HttpGet]
	public async Task<IActionResult> GetAll(string? search)
	{
		return Ok(await service.GetAllAsync(search));
	}
	// muszę zastosować {id} zamiast {id:int}, ponieważ kluczem głównym pacjenta jest PESEL
	// chyba, że źle zrozumiałem polecenie
	[HttpPost("{id}/bedassignments")]
	public async Task<IActionResult> Add([FromBody] AddBedAssignmentDto request, string id)
	{
		try
		{
			await service.AddAsync(request, id);
			return Created();
		}
		catch (ConflictException e)
		{
			return NotFound(e.Message);
		}
	}
}