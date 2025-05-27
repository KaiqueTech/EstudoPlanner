using System.ComponentModel.DataAnnotations;
using EstudoPlanner.Domain.Enums;

namespace EstudoPlanner.DTO.StudyPlan;

public class CreateSchedulesDto
{
    public DayOfWeek  DayOfWeek { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
}

public class CreateDisciplineDto
{
    public Guid idDiscipline { get; set; }
    public DisciplinesEnum Discipline { get; set; }
}
public class CreateStudyPlanDto
{
    [Required(ErrorMessage = "Title is required")]
    public string Title { get; set; } = default!;
    public string? Description { get; set; }
    public Guid IdUser { get; set; }

    public List<CreateDisciplineDto> DisciplinesDto { get; set; } = new List<CreateDisciplineDto>();
    [Required(ErrorMessage = "Schedules is required")]
    public List<CreateSchedulesDto> SchedulesDto { get; set; } = new List<CreateSchedulesDto>();
    
}