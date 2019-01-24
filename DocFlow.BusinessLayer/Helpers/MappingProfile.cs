using AutoMapper;
using DocFlow.BusinessLayer.Models.Report;
using DocFlow.BusinessLayer.Models.Report.History;
using DocFlow.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocFlow.BusinessLayer.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ReportValuesHistory, ReportValuesHistoryViewModel>()
                .ForMember(dest => dest.ReportLabel, opts => opts.MapFrom(src => src.ReportValue.ReportLabel));

        }
    }
}