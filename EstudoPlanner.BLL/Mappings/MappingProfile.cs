using AutoMapper;
using EstudoPlanner.Domain.Models;
using EstudoPlanner.DTO.Auth;
using EstudoPlanner.DTO.StudyPlan;

namespace EstudoPlanner.BLL.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        //Login and Register
        CreateMap<UserModel, LoginResponseDto>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.Token, opt => opt.Ignore());

        CreateMap<ScheduleModel, ScheduleResponseDto>();
        CreateMap<StudyPlanModel, StudyPlanResponseDto>()
            .ForMember(dest => dest.ScheduleResponses, opt 
                => opt.MapFrom(src => src.Schedules));
    }
}