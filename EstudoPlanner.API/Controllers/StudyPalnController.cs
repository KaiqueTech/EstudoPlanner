using EstudoPlanner.BLL.Services.StudyPlan;
using EstudoPlanner.DTO.StudyPlan;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EstudoPlanner.API.Controllers;

[Route("api/estudo-planner/[controller]")]
[ApiController]
public class StudyPalnController : ControllerBase
{
    private readonly IStudyPlanService _studyPlanService;
    public StudyPalnController(IStudyPlanService studyPlanService)
    {
        _studyPlanService = studyPlanService;
    }

    [HttpPost("create-study-plan")]
    [Authorize]
    public async Task<IActionResult> Create([FromBody]CreateStudyPlanDto  createStudyPlanDto)
    {
        var created = await _studyPlanService.CreateStudyPlan(createStudyPlanDto);
        return CreatedAtAction(nameof(GetStudyPlansByUser), new {userId = created.IdUser}, created);
    }
    
    [HttpGet("user/{userId}")]
    [Authorize]
    public async Task<IActionResult> GetStudyPlansByUser(Guid userId)
    {
        var studyPlans = await _studyPlanService.GetAllStudyPlan(userId);
        return Ok(studyPlans);
    }
}