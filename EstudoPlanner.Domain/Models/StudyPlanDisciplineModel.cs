using EstudoPlanner.Domain.Enums;

namespace EstudoPlanner.Domain.Models;

public class StudyPlanDisciplineModel
{
    public Guid IdStudyPlanDiscipline { get; set; }
    public StudyPlanModel StudyPlan { get; set; }
    
    public DisciplinesEnum  Discipline { get; set; }
    
    public Guid IdStudyPlan { get; set; }
}