using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TFEHelper.Backend.Plugins.PluginBase.Classes;
using TFEHelper.Backend.Plugins.PluginBase.Enums;
using TFEHelper.Backend.Plugins.PluginBase.Interfaces;

namespace TFEHelper.Backend.Plugins.Dummy
{
    public class DummyPlugin : IPublicationsCollector
    {
        public string Name => "Dummy plugin"; 
        public Version Version => new Version(1,0,0);
        public string Description => "Test plugin for IPublicationsCollector";

        public bool Configure()
        {
            return true;
        }

        public Task<IEnumerable<Publication>> GetPublicationsAsync(string searchQuery, CancellationToken cancellationToken = default)
        {
            var result = new List<Publication>()
            {
                new Publication()
                {
                    Abstract = "abstract abstract abstract abstract abstract abstract abstract abstract ",
                    Authors = "author author and author",
                    DOI = "DOI value",
                    ISBN = "ISBN value",
                    ISSN = "ISSN value",
                    Key = "Key value",
                    Keywords = "Keywords value",
                    Pages = "Pages value",
                    Source = SearchSourceType.Manual,
                    Title = "Title value",
                    Type = BibTeXPublicationType.Article,
                    URL = "URL value",
                    Year = 1995
                },
                new Publication()
                {
                    Abstract = "abstract2 abstract2 abstract2 abstract2 abstract2 abstract2 abstract2 abstract2 ",
                    Authors = "author2 author2 and author2",
                    DOI = "DOI value2",
                    ISBN = "ISBN value2",
                    ISSN = "ISSN value2",
                    Key = "Key value2",
                    Keywords = "Keywords value2",
                    Pages = "Pages value2",
                    Source = SearchSourceType.SEDICI,
                    Title = "Title value2",
                    Type = BibTeXPublicationType.Conference,
                    URL = "URL value2",
                    Year = 1998
                },
                new Publication()
                {
                    Abstract = "abstract3 abstract3 abstract3 abstract3 abstract3 abstract3 abstract3 abstract3",
                    Authors = "author3 author3 and author3",
                    DOI = "DOI value3",
                    ISBN = "ISBN value3",
                    ISSN = "ISSN value3",
                    Key = "Key value3",
                    Keywords = "Keywords value3",
                    Pages = "Pages value3",
                    Source = SearchSourceType.ACMDigitalLibrary,
                    Title = "Title value3",
                    Type = BibTeXPublicationType.Inproceedings,
                    URL = "URL value3",
                    Year = 1983
                },
            };

            return Task.Run<IEnumerable<Publication>>(() => { return result; }, cancellationToken);
        }

        public bool IsOnline()
        {
            return true;
        }
    }
}
