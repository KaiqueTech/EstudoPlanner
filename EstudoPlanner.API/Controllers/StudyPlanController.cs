using System.Security.Claims;
using EstudoPlanner.BLL.Services.StudyPlan;
using EstudoPlanner.DTO.StudyPlan;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EstudoPlanner.API.Controllers;

[Route("api/estudo-planner/[controller]")]
[ApiController]
public class StudyPlanController : ControllerBase
{
    private readonly IStudyPlanService _studyPlanService;
    public StudyPlanController(IStudyPlanService studyPlanService)
    {
        _studyPlanService = studyPlanService;
    }

    [HttpPost("create-study-plan")]
    [Authorize]
    public async Task<IActionResult> Create([FromBody]CreateStudyPlanDto  createStudyPlanDto)
    {
        try
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userIdClaim == null)
            {
                return Unauthorized();
            }
            
            var userId = Guid.Parse(userIdClaim);
            var created = await _studyPlanService.CreateStudyPlan(userId,createStudyPlanDto);
            return Ok(created);
        }
        catch (Exception e)
        {
            StatusCode(400, $"Error creating study plan: {e.Message}");
            return BadRequest();
        }
    }
    
    [HttpGet("study-plan{id}")]
    [Authorize]
    public async Task<IActionResult> GetStudyPlanById(Guid id)
    {
        try
        {
            var studyPlans = await _studyPlanService.GetStudyPlanById(id);
            return Ok(studyPlans);
        }
        catch (Exception e)
        {
            StatusCode(400, $"Not found study plan for ID{id}: {e.Message}");
            return BadRequest();
        }
    }

    [HttpGet("study-plans")]
    [Authorize]
    public async Task<IActionResult> GetAllStudyPlansByUser()
    {
        try
        {
            var userIdClaims = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userIdClaims == null)
            {
                return Unauthorized();
            }
            var userId = Guid.Parse(userIdClaims);
            var studyPlans = await _studyPlanService.GetAllStudyPlanByUserId(userId);
            return Ok(studyPlans);
        }
        catch (Exception ex)
        {
            StatusCode(400, $"Not found study plans: {ex.Message}");
            return BadRequest();
        }
    }

    [HttpPut("update-study-plan")]
    [Authorize]
    public async Task<IActionResult> UpdateStudyPlan([FromBody]UpdateStudyPlanDto updateStudyPlanDto)
    {
        try
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userIdClaim == null)
            {
                return Unauthorized();
            }
            
            var userId = Guid.Parse(userIdClaim);
            var studyPlan = await _studyPlanService.UpdateStudyPlan(userId,updateStudyPlanDto);
            
            if (studyPlan == null)
            {
                NotFound("Study plan not found");
            }

            return NoContent();

        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Server internal erros: {ex.Message}");
        }
    }

    [HttpDelete("Delete-study-plan/{id}")]
    [Authorize]
    public async Task<IActionResult> DeleteStudyPlan(Guid id)
    {
        try
        {
            var deleted = await _studyPlanService.DeleteStudyPlan(id);
            return Ok(deleted);
        }
        catch (Exception e)
        {
            StatusCode(400, $"Delete failed: {e.Message}");
            return BadRequest();
        }
    }
}