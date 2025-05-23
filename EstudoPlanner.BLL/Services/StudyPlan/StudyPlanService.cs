﻿using System.Collections;
using EstudoPlanner.DAL.DataContext;
using EstudoPlanner.Domain.Models;
using EstudoPlanner.DTO.StudyPlan;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

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
                IdUser = createStudyPlanDto.IdUser,
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

            return new StudyPlanResponseDto
            {
                IdStudyPlan = studyPlan.IdStudyPlan,
                Title = studyPlan.Title,
                Description = studyPlan.Description,
                IdUser = studyPlan.IdUser,
                ScheduleResponses = studyPlan.Schedules.Select(schedule => new ScheduleResponseDto
                {
                    IdSchedule = schedule.IdSchedule,
                    DayOfWeek = schedule.DayOfWeek,
                    StartTime = schedule.StartTime,
                    EndTime = schedule.EndTime
                }).ToList()
            };
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

            return new StudyPlanResponseDto
            {
                IdStudyPlan = studyPlan.IdStudyPlan,
                Title = studyPlan.Title,
                Description = studyPlan.Description,
                IdUser = studyPlan.IdUser,
                ScheduleResponses = studyPlan.Schedules.Select(s => new ScheduleResponseDto
                {
                    DayOfWeek = s.DayOfWeek,
                    StartTime = s.StartTime,
                    EndTime = s.EndTime
                }).ToList()
            };
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

            var response = studyPlans.Select(plan => new StudyPlanResponseDto
            {
                IdStudyPlan = plan.IdStudyPlan,
                Title = plan.Title,
                Description = plan.Description,
                IdUser = plan.IdUser,
                ScheduleResponses = plan.Schedules.Select(schedule => new ScheduleResponseDto
                {
                    DayOfWeek = schedule.DayOfWeek,
                    StartTime = schedule.StartTime,
                    EndTime = schedule.EndTime
                }).ToList()
            }).ToList();
            return response;
        }
        catch (Exception ex)
        {
            throw new ArgumentException($"Not found study plan for user ID:{userId}, {ex.Message}");
        }
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