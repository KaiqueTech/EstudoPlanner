﻿using EstudoPlanner.Domain.Enums;

namespace EstudoPlanner.Domain.Models;

public class StudyPlanModel
{
    public Guid IdStudyPlan { get; set; }
    public string Title { get; set; } = default!;
    public string? Description { get; set; }
    
    public List<StudyPlanDisciplineModel> Disciplines { get; set; } = default!;
    
    public Guid IdUser { get; set; }
    public UserModel  User { get; set; } = default!;

    public List<ScheduleModel> Schedules { get; set; } = new List<ScheduleModel>();
}