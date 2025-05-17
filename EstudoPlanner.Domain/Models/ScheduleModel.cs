namespace EstudoPlanner.Domain.Models;

public class ScheduleModel
{
    public Guid IdSchedule { get; set; }
    public DayOfWeek DayOfWeek { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    
    public Guid IdStudyPlan { get; set; }
    
    public StudyPlanModel  StudyPlan { get; set; } = default!;
    
}