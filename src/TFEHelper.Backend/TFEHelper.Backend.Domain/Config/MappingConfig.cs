using AutoMapper;
using AutoMapper.Extensions.EnumMapping;
using TFEHelper.Backend.Domain.Classes.DTO;
using BibTeXPublicationTypeFromPlugin = TFEHelper.Backend.Plugins.PluginBase.Enums.BibTeXPublicationType;
using BibTeXPublicationTypeLocal = TFEHelper.Backend.Domain.Enums.BibTeXPublicationType;
using PublicationFromPlugin = TFEHelper.Backend.Plugins.PluginBase.Classes.Publication;
using PublicationLocal = TFEHelper.Backend.Domain.Classes.Models.Publication;

namespace TFEHelper.Backend.Domain.Config
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<PublicationLocal, PublicationDTO>().ReverseMap();
            CreateMap<PublicationLocal, PublicationFromPlugin>().ReverseMap();
            CreateMap<BibTeXPublicationTypeLocal, BibTeXPublicationTypeFromPlugin>()
                .ConvertUsingEnumMapping(opt => opt.MapByValue())
                .ReverseMap();            
        }
    }
}