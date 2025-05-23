namespace EstudoPlanner.DTO.StudyPlan;

public class CreateSchedulesDto
{
    public DayOfWeek  DayOfWeek { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
}
public class CreateStudyPlanDto
{
    public string Title { get; set; } = default!;
    public string? Description { get; set; }
    public Guid IdUser { get; set; }
    
    public List<CreateSchedulesDto> SchedulesDto { get; set; } = new List<CreateSchedulesDto>();
    
}