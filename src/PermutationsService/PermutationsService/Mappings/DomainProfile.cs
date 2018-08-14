using AutoMapper;
using PermutationsService.Web.DataAccess.Entities;
using PermutationsService.Web.Models;

namespace PermutationsService.Web.Mappings
{
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            CreateMap<PermutationEntry, PermutationsBulkInsertResultModel>()
                .ForMember("Count", opt => opt.MapFrom(src => src.ResultCount))
                .ForMember("ElapsedTime", opt => opt.MapFrom(src => src.SpendedTime))
                .ForMember("Result", opt => opt.MapFrom(src => src.ResultString))
                .ForMember("Item", opt => opt.MapFrom(src => src.Item));
        }
    }
}
