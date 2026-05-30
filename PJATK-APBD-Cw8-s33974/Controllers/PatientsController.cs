using Microsoft.AspNetCore.Mvc;
using PJATK_APBD_Cw8_s33974.DTOs;
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
	[HttpPost]
	public async Task<IActionResult> Add([FromBody] AddBedAssignmentDto request)
	{
		//var pcResponseDto = await service.AddAsync(request);
		//return CreatedAtAction(nameof(GetById), new { id = pcResponseDto.Id }, pcResponseDto);
		return Ok(await service.AddAsync(request));
	}
}