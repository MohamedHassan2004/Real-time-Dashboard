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
                opt => opt.MapFrom(src => DateTime.UtcNow - src.LastStatusChange));
        }
    }
}
