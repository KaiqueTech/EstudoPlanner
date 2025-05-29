namespace EstudoPlanner.Domain.Models;

public class UserModel
{
    public Guid IdUser { get; set; }
    public string Name { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string PasswordHash { get; set; } = default!;

    public ICollection<StudyPlanModel> StudyPlans { get; private set; } = new List<StudyPlanModel>();
    
}