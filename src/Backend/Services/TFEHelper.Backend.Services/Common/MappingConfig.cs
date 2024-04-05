using AutoMapper;
using AutoMapper.Extensions.EnumMapping;
using System.Data;
using TFEHelper.Backend.Domain.Classes.API;
using TFEHelper.Backend.Domain.Classes.Database;
using TFEHelper.Backend.Domain.Enums;
using TFEHelper.Backend.Plugins.PluginBase.Common.Classes;
using TFEHelper.Backend.Plugins.PluginBase.Specifications.PublicationsCollector.Classes;
using TFEHelper.Backend.Plugins.PluginBase.Specifications.PublicationsCollector.Enums;
using TFEHelper.Backend.Services.Contracts.DTO.API;
using TFEHelper.Backend.Services.Contracts.DTO.Plugin;
using Publication = TFEHelper.Backend.Domain.Classes.Models.Publication;
using PublicationsCollectorParametersPLG = TFEHelper.Backend.Plugins.PluginBase.Specifications.PublicationsCollector.Classes.PublicationsCollectorParametersPLG;

namespace TFEHelper.Backend.Services.Common
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Publication, PublicationDTO>().ReverseMap();

            CreateMap<Publication, PublicationPLG>().ReverseMap();

            CreateMap<PublicationDTO, PublicationPLG>().ReverseMap();

            CreateMap<PluginInfo, PluginInfoDTO>().ReverseMap();

            CreateMap<PaginationParameters, PaginationParametersDTO>().ReverseMap();

            CreateMap(typeof(PaginatedList<>), typeof(PaginatedListDTO<>)).ReverseMap();

            CreateMap<FileFormatType, FileFormatDTOType>()
                .ConvertUsingEnumMapping(opt => opt.MapByValue())
                .ReverseMap();

            CreateMap<SearchSourceType, SearchSourceDTOType>()
                .ConvertUsingEnumMapping(opt => opt.MapByValue())
                .ReverseMap();

            CreateMap<SearchSourceType, SearchSourcePLGType>()
                .ConvertUsingEnumMapping(opt => opt.MapByValue())
                .ReverseMap();

            CreateMap<SearchSourceDTOType, SearchSourcePLGType>()
                .ConvertUsingEnumMapping(opt => opt.MapByValue())
                .ReverseMap();

            CreateMap<BibTeXPublicationType, BibTeXPublicationDTOType>()
                .ConvertUsingEnumMapping(opt => opt.MapByValue())
                .ReverseMap();

            CreateMap<BibTeXPublicationType, BibTeXPublicationPLGType>()
                .ConvertUsingEnumMapping(opt => opt.MapByValue())
                .ReverseMap();

            CreateMap<BibTeXPublicationDTOType, BibTeXPublicationPLGType>()
                .ConvertUsingEnumMapping(opt => opt.MapByValue())
                .ReverseMap();

            CreateMap<PublicationsCollectorParametersPLG, PublicationsCollectorParametersDTO>().ReverseMap();

            CreateMap<PublicationsCollectorParametersPLG, PublicationsCollectorParametersPLG>().ReverseMap();

            CreateMap<SearchParameter, SearchParameterDTO>().ReverseMap();

            CreateMap<SearchParameter, DatabaseParameter>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(o => DatabaseParameterType.String))
                .ForMember(dest => dest.Direction, opt => opt.MapFrom(o => ParameterDirection.Input));

            CreateMap<SearchParameterDTO, DatabaseParameter>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(o => DatabaseParameterType.String))
                .ForMember(dest => dest.Direction, opt => opt.MapFrom(o => ParameterDirection.Input));
        }
    }
}
