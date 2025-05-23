using EstudoPlanner.DTO.StudyPlan;
using Microsoft.AspNetCore.Mvc;

namespace EstudoPlanner.BLL.Services.StudyPlan;

public interface IStudyPlanService
{
    Task<StudyPlanResponseDto> CreateStudyPlan(CreateStudyPlanDto createStudyPlanDto);
    Task<StudyPlanResponseDto> GetStudyPlanById(Guid id);
    Task<List<StudyPlanResponseDto>> GetAllStudyPlanByUserId(Guid userId);
    Task<bool> UpdateStudyPlan(Guid id, CreateStudyPlanDto createStudyPlanDto);
    Task<bool> DeleteStudyPlan(Guid id);
}