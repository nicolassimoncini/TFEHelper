using AutoMapper;
using AutoMapper.Extensions.EnumMapping;
using System.Data;
using TFEHelper.Backend.Domain.Classes.API;
using TFEHelper.Backend.Domain.Classes.Database;
using TFEHelper.Backend.Domain.Classes.DTO;
using TFEHelper.Backend.Domain.Enums;
using BibTeXPublicationTypeFromPlugin = TFEHelper.Backend.Plugins.PluginBase.Enums.BibTeXPublicationType;
using BibTeXPublicationTypeLocal = TFEHelper.Backend.Domain.Enums.BibTeXPublicationType;
using PublicationFromModel = TFEHelper.Backend.Domain.Classes.Models.Publication;
using PublicationFromPlugin = TFEHelper.Backend.Plugins.PluginBase.Classes.Publication;
using PublicationsCollectorParametersFromModel = TFEHelper.Backend.Domain.Classes.Plugin.PublicationsCollectorParameters;
using PublicationsCollectorParametersFromPlugin = TFEHelper.Backend.Plugins.PluginBase.Classes.PublicationsCollectorParameters;

namespace TFEHelper.Backend.Domain.Config
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<PublicationFromModel, PublicationDTO>().ReverseMap();
            
            CreateMap<PublicationFromModel, PublicationFromPlugin>().ReverseMap();
            
            CreateMap<BibTeXPublicationTypeLocal, BibTeXPublicationTypeFromPlugin>()
                .ConvertUsingEnumMapping(opt => opt.MapByValue())
                .ReverseMap(); 
            
            CreateMap<PublicationsCollectorParametersFromModel, PublicationsCollectorParametersFromPlugin>().ReverseMap();
            
            CreateMap<SearchParameter, DatabaseParameter>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(o => DatabaseParameterType.String))
                .ForMember(dest => dest.Direction, opt => opt.MapFrom(o => ParameterDirection.Input));
        }
    }
}