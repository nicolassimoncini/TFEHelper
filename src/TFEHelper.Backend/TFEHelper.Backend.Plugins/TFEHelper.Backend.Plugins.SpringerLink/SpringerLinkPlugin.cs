using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using TFEHelper.Backend.Plugins.PluginBase.Classes;
using TFEHelper.Backend.Plugins.PluginBase.Enums;
using TFEHelper.Backend.Plugins.PluginBase.Interfaces;
using TFEHelper.Backend.Plugins.SpringerLink.Classes;
using TFEHelper.Backend.Plugins.SpringerLink.DTO;
using TFEHelper.Backend.Plugins.SpringerLink.Enums;
using TFEHelper.Backend.Plugins.SpringerLink.Extensions;

namespace TFEHelper.Backend.Plugins.SpringerLink
{
    public class SpringerLinkPlugin : IPublicationsCollector
    {
        public string Name => "SpringerLink plugin";
        public Version Version => new Version(1, 0, 0);
        public PluginType Type => PluginType.PublicationsCollector;
        public string Description => "API adapter for Springer Link";

        public bool Configure()
        {
            return true;
        }

        public bool IsOnline()
        {
            return true;
        }

        public async Task<IEnumerable<Publication>> GetPublicationsAsync(SearchParameters searchParameters, CancellationToken cancellationToken = default)
        {
            const string uri = "http://api.springernature.com/metadata";
            const string APIKey = "51cc9a793e0d7714a068c4f2a2cc7f19";
            const int defaultPageSize = 5;
            const string defaultSubject = ""; // "Computer Science"

            using (var api = new SpringerLinkAPIWrapper(uri, APIKey, SpringerLinkAPIAuthorizationType.QueryParameter))
            {
                api.Setup(
                    searchParameters.query,
                    searchParameters.DateFrom,
                    searchParameters.DateTo,
                    defaultSubject,
                    defaultPageSize,
                    searchParameters.ReturnQuantityLimit);

                List<SpringerLinkRecordDTO> result = await api.GetAllRecordsAsync(cancellationToken);
                List<Publication> publications = new List<Publication>();

                foreach (var record in result)
                {
                    publications.Add(new Publication()
                    {
                        Key = "undefined", // SpringerLink no define Key.
                        Abstract = record.Abstract,
                        Authors = record.Creators?.ToString("and", (x) => { return x.Creator; }),
                        DOI = record.DOI,
                        ISBN = record.ISBN,
                        ISSN = record.ElectronicISBN,
                        Keywords = null,
                        Pages = null,
                        Source = SearchSourceType.SpringerLink,
                        Title = record.Title,
                        Type = StringExtensions.NormalizePublicationType(record.ContentType).ToPublicationType(),
                        URL = record.URL?.FirstOrDefault()?.Value,
                        Year = record.PublicationDate.Year
                    });
                }

                return publications;
            };
        }
    }
}

// Apuntes:
// Documentación de la API: https://dev.springernature.com/
// http://api.springernature.com/meta/v2/json?q=software development framework ontology domain onlinedatefrom:2003-01-01%20onlinedateto:2023-12-31 subject:%22Computer Science%22&p=100&api_key=51cc9a793e0d7714a068c4f2a2cc7f19
//const string uri = "http://api.springernature.com/meta/v2";
//const string query = "software development framework ontology domain";
//const string query = "(software NEAR/5 development NEAR/5 framework*) OR (software NEAR/5 development NEAR/5 framework* NEAR/5 ontolog* NEAR/5 domain*)"; 