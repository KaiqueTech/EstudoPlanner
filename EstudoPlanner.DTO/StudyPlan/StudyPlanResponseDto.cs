namespace EstudoPlanner.DTO.StudyPlan;

public class ScheduleResponseDto()
{
    public Guid  IdSchedule { get; set; }
    public DayOfWeek DayOfWeek { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
}
public class StudyPlanResponseDto
{
    public  Guid IdStudyPlan { get; set; }
    public string Title { get; set; } = default!;
    public string? Description { get; set; }
    public Guid IdUser { get; set; }

    public List<ScheduleResponseDto> ScheduleResponses { get; set; } = new List<ScheduleResponseDto>();

}