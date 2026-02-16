using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Dashboard.Application.DTOs;
using Dashboard.Domain.Entities;

namespace Dashboard.Application.AutoMapper
{

    public class DashboardProfile : Profile
    {
        public DashboardProfile()
        {
            CreateMap<Agent, AgentDTO>()
                .ForMember(dest => dest.Duration,
                opt => opt.MapFrom(src => (DateTime.UtcNow - src.LastStatusChange).ToString(@"hh\:mm\:ss")))
                .ForMember(dest => dest.LastStatusChange,
                opt => opt.MapFrom(src => src.LastStatusChange.ToString("yyyy-MM-ddTHH:mm:ssZ")));
        }
    }
}
