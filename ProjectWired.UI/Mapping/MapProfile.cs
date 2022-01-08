using AutoMapper;
using ProjectWired.Core.DTOs;
using ProjectWired.Core.Models;

namespace ProjectWired.UI.Mapping
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<CreateExamDto, Exam>();
            CreateMap<Exam, CreateExamDto>();
        }
    }
}
