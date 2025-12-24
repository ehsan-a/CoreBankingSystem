using AutoMapper;
using CoreBanking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace CoreBanking.Application.Mappings
{
    public class AuditLogProfile : Profile
    {
        public AuditLogProfile()
        {
           
            CreateMap(typeof(object), typeof(AuditLog))
                .ForMember("EntityName", opt => opt.MapFrom((src, dest) => src.GetType().Name))
                .ForMember("EntityId", opt => opt.MapFrom((src, dest) =>
                {
                    var prop = src.GetType().GetProperty("Id");
                    return prop != null ? prop.GetValue(src) : null;
                }))
                .ForMember("NewValue", opt => opt.MapFrom(src => JsonSerializer.Serialize(src)))
                .ForMember("ActionType", opt => opt.Ignore())
                .ForMember("PerformedBy", opt => opt.Ignore())
                .ForMember("OldValue", opt => opt.Ignore()); 
        }
    }
}
