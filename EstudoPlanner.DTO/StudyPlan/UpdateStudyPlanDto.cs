namespace EstudoPlanner.DTO.StudyPlan;

public class UpdateStudyPlanDto
{
    public string Title { get; set; } = default!;
    public string? Description { get; set; }
    
    public List<CreateDisciplineDto>? DisciplineDto { get; set; }
    public List<CreateSchedulesDto>? SchedulesDto { get; set; }
}