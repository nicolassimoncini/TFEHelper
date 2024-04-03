using AutoMapper;
using AutoMapper.Extensions.EnumMapping;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TFEHelper.Backend.Domain.Classes.API;
using TFEHelper.Backend.Domain.Classes.Database;
using TFEHelper.Backend.Domain.Classes.Models;
using TFEHelper.Backend.Domain.Classes.Plugin;
using TFEHelper.Backend.Domain.Enums;
using TFEHelper.Backend.Services.Contracts.DTO.API;
using TFEHelper.Backend.Services.Contracts.DTO.Plugin;
using Publication = TFEHelper.Backend.Domain.Classes.Models.Publication;
using PublicationsCollectorParameters = TFEHelper.Backend.Plugins.PluginBase.Classes.PublicationsCollectorParameters;

namespace TFEHelper.Backend.Services
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Publication, PublicationDTO>().ReverseMap();

            CreateMap<PluginInfo, PluginInfoDTO>().ReverseMap();

            CreateMap<SearchParameter, SearchParameterDTO>().ReverseMap();

            CreateMap<PaginationParameters, PaginationParametersDTO>().ReverseMap();

            CreateMap(typeof(PaginatedList<>), typeof(PaginatedListDTO<>)).ReverseMap();

            CreateMap<FileFormatType, FileFormatDTOType>()
                .ConvertUsingEnumMapping(opt => opt.MapByValue())
                .ReverseMap();

            CreateMap<SearchSourceType, SearchSourceDTOType>()
                .ConvertUsingEnumMapping(opt => opt.MapByValue())
                .ReverseMap();

            CreateMap<BibTeXPublicationType, BibTeXPublicationDTOType>()
                .ConvertUsingEnumMapping(opt => opt.MapByValue())
                .ReverseMap();

            CreateMap<PublicationsCollectorParameters, PublicationsCollectorParametersDTO>().ReverseMap();

            CreateMap<SearchParameter, DatabaseParameter>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(o => DatabaseParameterType.String))
                .ForMember(dest => dest.Direction, opt => opt.MapFrom(o => ParameterDirection.Input));
        }
    }
}
