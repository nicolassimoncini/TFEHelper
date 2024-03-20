using AutoMapper;
using AutoMapper.Extensions.EnumMapping;
using TFEHelper.Backend.Domain.Classes.DTO;
using BibTeXPublicationTypeFromPlugin = TFEHelper.Backend.Plugins.PluginBase.Enums.BibTeXPublicationType;
using BibTeXPublicationTypeLocal = TFEHelper.Backend.Domain.Enums.BibTeXPublicationType;
using PublicationFromModel = TFEHelper.Backend.Domain.Classes.Models.Publication;
using PublicationFromPlugin = TFEHelper.Backend.Plugins.PluginBase.Classes.Publication;
using SearchParametersFromModel = TFEHelper.Backend.Domain.Classes.Models.SearchParameters;
using SearchParametersFromPlugin = TFEHelper.Backend.Plugins.PluginBase.Classes.SearchParameters;

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
            CreateMap<SearchParametersFromModel, SearchParametersFromPlugin>().ReverseMap();
        }
    }
}