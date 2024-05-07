using Swashbuckle.AspNetCore.Filters;
using System.Net;
using TFEHelper.Backend.Plugins.PluginBase.Common.Classes;
using TFEHelper.Backend.Services.Contracts.DTO.API;
using TFEHelper.Backend.Services.Contracts.DTO.Plugin;

namespace TFEHelper.Backend.API.Examples.Plugins
{
    public class GetPublicationsFromPluginRequestExample : IExamplesProvider<PublicationsCollectorParametersDTO>
    {
        public PublicationsCollectorParametersDTO GetExamples()
        {
            return new PublicationsCollectorParametersDTO()
            {
                DateFrom = new DateOnly(2021, 1, 11),
                DateTo = new DateOnly(2022, 7, 20),
                Query = "software AND development",
                ReturnQuantityLimit = 100,
                SearchIn = "",
                Subject = "TEC"
            };           
        }
    }
}
