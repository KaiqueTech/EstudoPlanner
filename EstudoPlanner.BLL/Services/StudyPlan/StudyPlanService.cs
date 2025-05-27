using System.Collections;
using AutoMapper;
using EstudoPlanner.DAL.DataContext;
using EstudoPlanner.Domain.Models;
using EstudoPlanner.DTO.StudyPlan;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace EstudoPlanner.BLL.Services.StudyPlan;

public class StudyPlanService : IStudyPlanService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;
    public StudyPlanService(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
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
                IdUser = createStudyPlanDto.IdUser,
                Disciplines = createStudyPlanDto.DisciplinesDto.Select(d => new StudyPlanDisciplineModel
                {
                    IdStudyPlan = Guid.NewGuid(),
                    Discipline = d.Discipline
                }).ToList(),
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
            
            _context.StudyPlans.Add(studyPlan);
            await _context.SaveChangesAsync();

            return _mapper.Map<StudyPlanResponseDto>(studyPlan);
        }
        catch (Exception e)
        {
            throw new ArgumentException(e.Message);
        }
        
    }

    public async Task<StudyPlanResponseDto> GetStudyPlanById(Guid id)
    {
        try
        {
            var studyPlan = await _context.StudyPlans
                .Include(plan => plan.Schedules)
                .FirstOrDefaultAsync(plan => plan.IdStudyPlan == id);

            if (studyPlan == null)
            {
                throw new KeyNotFoundException("StudyPlan not found");
            }
            
            return _mapper.Map<StudyPlanResponseDto>(studyPlan);
        }
        catch (Exception ex)
        {
            throw new Exception($"An ocurred when search for the study plan! {ex.Message}");
        }
    }

    public async Task<List<StudyPlanResponseDto>> GetAllStudyPlanByUserId(Guid userId)
    {
        try
        {
            var studyPlans = await _context.StudyPlans
                .Include(schedule => schedule.Schedules)
                .Where(plan => plan.IdUser == userId)
                .ToListAsync();

            if (studyPlans == null || studyPlans.Count == 0)
            {
                throw new KeyNotFoundException("StudyPlan not found");
            }
            
            return _mapper.Map<List<StudyPlanResponseDto>>(studyPlans);
        }
        catch (Exception ex)
        {
            throw new ArgumentException($"Not found study plans for user ID:{userId}, {ex.Message}");
        }
    }

    public async Task<StudyPlanResponseDto> UpdateStudyPlan(Guid id, UpdateStudyPlanDto updateStudyPlanDto)
    {
        try
        {
            if (updateStudyPlanDto == null)
                throw new ArgumentNullException(nameof(updateStudyPlanDto), "Fill in the fields");

            var existingPlan = await _context.StudyPlans
                .Include(plan => plan.Schedules)
                //.AsNoTracking()
                .FirstOrDefaultAsync(plan => plan.IdStudyPlan == id);

            if (existingPlan == null)
                throw new KeyNotFoundException("StudyPlan not found");
            
            existingPlan.Title = updateStudyPlanDto.Title;
            existingPlan.Description = updateStudyPlanDto.Description;
            existingPlan.Disciplines = updateStudyPlanDto.DisciplineDto.Select(d => new StudyPlanDisciplineModel
            {
                IdStudyPlan = Guid.Empty,
                Discipline = d.Discipline
            }).ToList();
            
            _context.Schedules.RemoveRange(existingPlan.Schedules);
            await _context.SaveChangesAsync();
            
            var newSchedules = updateStudyPlanDto.SchedulesDto.Select(scheduleDto =>
            {
                if (scheduleDto.EndTime <= scheduleDto.StartTime)
                    throw new ArgumentException($"EndTime must be greater than StartTime for day {scheduleDto.DayOfWeek}");

                return new ScheduleModel
                {
                    IdSchedule = Guid.NewGuid(),
                    DayOfWeek = scheduleDto.DayOfWeek,
                    StartTime = scheduleDto.StartTime,
                    EndTime = scheduleDto.EndTime,
                    IdStudyPlan = existingPlan.IdStudyPlan
                };
            }).ToList();
            
            existingPlan.Schedules = newSchedules;
            
            //_context.StudyPlans.Update(existingPlan);
            await _context.AddRangeAsync(newSchedules);
            await _context.SaveChangesAsync();

            return _mapper.Map<StudyPlanResponseDto>(existingPlan);
        }
        catch (Exception ex)
        {
            throw new Exception($"Update failed: {ex.Message}");
        }
    }
    

    public async Task<bool> DeleteStudyPlan(Guid id)
    {
        try
        {
            var existingPlan = await _context.StudyPlans
                .Include(plan => plan.Schedules)
                .FirstOrDefaultAsync(plan => plan.IdStudyPlan == id);

            if (existingPlan == null)
            {
                throw new KeyNotFoundException("StudyPlan not found");
            }

            _context.StudyPlans.Remove(existingPlan);
            await _context.SaveChangesAsync();

            return true;
        }
        catch (Exception ex)
        {
            throw new Exception($"Delete failed: {ex.Message}");
        }
    }
}