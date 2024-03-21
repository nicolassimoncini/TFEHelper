using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using TFEHelper.Backend.Plugins.PluginBase.Classes;
using TFEHelper.Backend.Plugins.PluginBase.Enums;
using TFEHelper.Backend.Plugins.SpringerLink.DTO;
using TFEHelper.Backend.Plugins.SpringerLink.Enums;
using TFEHelper.Backend.Plugins.SpringerLink.Extensions;

namespace TFEHelper.Backend.Plugins.SpringerLink.Classes
{
    internal static class SpringerLinkHandler
    {
        /*
        /// <summary>
        /// Obtiene los registros desde SpringerLink y guarda los resultados en un archivo .json
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task GetRecordsAsync(CancellationToken cancellationToken = default)
        {
            // http://api.springernature.com/meta/v2/json?q=software development framework ontology domain onlinedatefrom:2003-01-01%20onlinedateto:2023-12-31 subject:%22Computer Science%22&p=100&api_key=51cc9a793e0d7714a068c4f2a2cc7f19

            //const string uri = "http://api.springernature.com/meta/v2";
            const string uri = "http://api.springernature.com/metadata";
            const string APIKey = "51cc9a793e0d7714a068c4f2a2cc7f19";
            const string query = "software development framework ontology domain";
            //const string query = "(software NEAR/5 development NEAR/5 framework*) OR (software NEAR/5 development NEAR/5 framework* NEAR/5 ontolog* NEAR/5 domain*)";

            using (var api = new SpringerLinkAPIWrapper(uri, APIKey, SpringerLinkAPIAuthorizationType.QueryParameter))
            {
                api.Setup(
                    query,
                    new DateTime(2003, 1, 1),
                    new DateTime(2023, 12, 31),
                    "Computer Science",
                    5);

                List<SpringerLinkRecordDTO> result = await api.GetAllRecordsAsync(cancellationToken);
                using (FileStream createStream = File.Open("records.json", FileMode.Create))
                {
                    await JsonSerializer.SerializeAsync(createStream, result, new JsonSerializerOptions { WriteIndented = true }, cancellationToken);
                    await createStream.DisposeAsync();
                }
            };
        }
        */

        public static async Task<List<Publication>> ImportAsync(string filePath, CancellationToken cancellationToken = default)
        {
            string NormalizePublicationType(string pt) => pt switch
            {
                "Chapter ConferencePaper" => BibTeXPublicationType.Conference.ToString(),
                "Chapter" => BibTeXPublicationType.Book.ToString(),
                _ => pt
            };

            List<Publication> publications = new List<Publication>();

            using (FileStream openStream = File.OpenRead(filePath))
            {
                List<SpringerLinkRecordDTO> result = await JsonSerializer.DeserializeAsync<List<SpringerLinkRecordDTO>>(openStream, JsonSerializerOptions.Default, cancellationToken);
                foreach (var record in result ?? Enumerable.Empty<SpringerLinkRecordDTO>())
                {
                    publications.Add(new Publication()
                    {
                        Key = "undefined", // SpringerLink no define Key.
                        Abstract = record.Abstract,
                        Authors = record.Creators?.ToString("and", (x) => { return x.Creator; }), //record.Creators?.ConvertAll<string>(x => x.Creator),
                        DOI = record.DOI,
                        ISBN = record.ISBN,
                        ISSN = record.ElectronicISBN,
                        Keywords = null,
                        Pages = null,
                        Source = SearchSourceType.SpringerLink,
                        Title = record.Title,
                        Type = NormalizePublicationType(record.ContentType).ToPublicationType(),
                        URL = record.URL?.FirstOrDefault()?.Value,
                        Year = record.PublicationDate.Year
                    });
                }
            }
            return publications;
        }
    }
}
