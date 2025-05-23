namespace EstudoPlanner.Domain.Models;

public class ScheduleModel
{
    public Guid IdSchedule { get; set; }
    public DayOfWeek DayOfWeek { get; set; } = default!;
    public TimeSpan StartTime { get; set; } = default!;
    public TimeSpan EndTime { get; set; } = default!;
    
    public Guid IdStudyPlan { get; set; }
    
    public StudyPlanModel  StudyPlan { get; set; } = default!;
    
}