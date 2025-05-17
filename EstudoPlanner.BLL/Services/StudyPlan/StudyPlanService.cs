using EstudoPlanner.DAL.DataContext;
using EstudoPlanner.Domain.Models;
using EstudoPlanner.DTO.StudyPlan;
using Microsoft.AspNetCore.Http.HttpResults;

namespace EstudoPlanner.BLL.Services.StudyPlan;

public class StudyPlanService : IStudyPlanService
{
    private readonly AppDbContext _context;
    public StudyPlanService(AppDbContext context)
    {
        _context = context;   
    }
    
    public async Task<StudyPlanResponseDto> CreateStudyPlan(CreateStudyPlanDto createStudyPlanDto)
    {
        try
        {
            var studyPlan = new StudyPlanModel
            {
                IdStudyPlan = Guid.NewGuid(),
                Title = createStudyPlanDto.Title,
                Description = createStudyPlanDto.Description,
                Schedules = createStudyPlanDto.SchedulesDto.Select(s => 
                {
                    if (s.EndTime <= s.StartTime)
                    {
                         throw new ArgumentException($"EndTime must be greater than StartTime for day {s.DayOfWeek}");
                    }

                    return new ScheduleModel
                    {
                        IdSchedule = Guid.NewGuid(),
                        DayOfWeek = s.DayOfWeek,
                        StartTime = s.StartTime,
                        EndTime = s.EndTime,
                        IdStudyPlan = Guid.Empty
                    };
                }).ToList()
            };

            _context.StudyPlans.AddAsync(studyPlan);
            await _context.SaveChangesAsync();

            return new StudyPlanResponseDto
            {
                IdStudyPlan = studyPlan.IdStudyPlan,
                Title = studyPlan.Title,
                Description = studyPlan.Description,
                IdUser = studyPlan.IdUser,
            

            };
        }
        catch (Exception e)
        {
            throw new ArgumentException(e.Message);
        }
        
    }

    public Task<StudyPlanResponseDto> GetStudyPlanById(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<StudyPlanResponseDto>> GetAllStudyPlan(Guid userId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateStudyPlan(Guid id, CreateStudyPlanDto createStudyPlanDto)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateStudyPlan(Guid id, StudyPlanResponseDto studyPlanResponseDto)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteStudyPlan(Guid id)
    {
        throw new NotImplementedException();
    }
}