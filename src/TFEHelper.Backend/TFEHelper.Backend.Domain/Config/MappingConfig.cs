using AutoMapper;
using TFEHelper.Backend.Domain.Classes.DTO;
using TFEHelper.Backend.Domain.Classes.Models;

namespace TFEHelper.Backend.Domain.Config
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Publication, PublicationDTO>().ReverseMap();
        }
    }
}