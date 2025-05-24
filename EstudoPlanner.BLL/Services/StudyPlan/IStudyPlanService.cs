using EstudoPlanner.DTO.StudyPlan;
using Microsoft.AspNetCore.Mvc;

namespace EstudoPlanner.BLL.Services.StudyPlan;

public interface IStudyPlanService
{
    Task<StudyPlanResponseDto> CreateStudyPlan(CreateStudyPlanDto createStudyPlanDto);
    Task<StudyPlanResponseDto> GetStudyPlanById(Guid id);
    Task<List<StudyPlanResponseDto>> GetAllStudyPlanByUserId(Guid userId);
    Task<StudyPlanResponseDto> UpdateStudyPlan(Guid id, UpdateStudyPlanDto updateStudyPlanDto);
    Task<bool> DeleteStudyPlan(Guid id);
}