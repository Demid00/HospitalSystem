using Microsoft.AspNetCore.Mvc;
using Hospital.Domain.Repositories;

namespace HospitalSystem.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HealthController : ControllerBase
{
    private readonly IPatientRepository _patientRepository;

    public HealthController(IPatientRepository patientRepository)
    {
        _patientRepository = patientRepository;
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new
        {
            message = "HospitalSystem API is running",
            repositoryType = _patientRepository.GetType().Name
        });
    }
}