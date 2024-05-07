using Swashbuckle.AspNetCore.Filters;
using System.Net;
using TFEHelper.Backend.Plugins.PluginBase.Common.Classes;
using TFEHelper.Backend.Services.Contracts.DTO.API;
using TFEHelper.Backend.Services.Contracts.DTO.Configuration;
using TFEHelper.Backend.Services.Contracts.DTO.Plugin;

namespace TFEHelper.Backend.API.Examples.Plugins
{
    public class GetAllPluginsResponseExample : IExamplesProvider<APIResponseDTO>
    {
        public APIResponseDTO GetExamples()
        {
            var container = new GlobalParametersContainer();
            container.CollectionValued.Add("Subjects", "Subject description 1", "Subject code 1");
            container.CollectionValued.Add("Subjects", "Subject description 2", "Subject code 2");

            return new APIResponseDTO()
            {
                IsSuccessful = true,
                StatusCode = HttpStatusCode.OK,
                ErrorMessages = new(),
                Payload = new List<PluginInfoDTO>()
                {
                    new PluginInfoDTO()
                    {
                        Name = "Plugin name",
                        Description = "Plugin description",
                        Id = 1,
                        Type = PluginDTOType.PublicationsCollector,
                        Version = new Version(1, 0, 0),
                        Parameters = container
                    }
                }
            };
        }
    }
}
