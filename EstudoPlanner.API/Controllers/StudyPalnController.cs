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
        var created = await _studyPlanService.CreateStudyPlan(createStudyPlanDto);
        return Ok(created);
    }
    
    [HttpGet("study-plan{id}")]
    [Authorize]
    public async Task<IActionResult> GetStudyPlanById(Guid id)
    {
        var studyPlans = await _studyPlanService.GetStudyPlanById(id);
        return Ok(studyPlans);
    }

    [HttpGet("study-plans/{userId}")]
    [Authorize]
    public async Task<IActionResult> GetAllStudyPlansByUser(Guid userId)
    {
        var studyPlans = await _studyPlanService.GetAllStudyPlanByUserId(userId);
        return Ok(studyPlans);
    }
}