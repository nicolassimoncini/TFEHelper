using Microsoft.Extensions.Logging;
using QueryString;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using TFEHelper.Backend.Plugins.arXiv.DTO;
using TFEHelper.Backend.Plugins.arXiv.Extensions;
using TFEHelper.Backend.Plugins.PluginBase.Specifications.PublicationsCollector.Classes;
using TFEHelper.Backend.Plugins.PluginBase.Tools;

namespace TFEHelper.Backend.Plugins.arXiv.Classes
{
    internal class ArxivAPIWrapper
    {
        private readonly ILogger _logger;
        private QueryStringBuilder _queryBuilder;

        public ArxivAPIWrapper(ILogger logger)
        {
            _queryBuilder = new QueryStringBuilder();
            _logger = logger;
        }

        private string BuildSearchQuery(PublicationsCollectorParametersPLG searchParameters, PluginConfigurationController config)
        {
            const string FMT_SPACE = " ";
            const string FMT_QUOTE = "%22"; // """
            const string FMT_OPEN_PARENTHESES = "%28"; // "("
            const string FMT_CLOSE_PARENTHESES = "%29"; // ")"            
            const string FMT_DATE = " AND submittedDate:[{0:yyyyMMdd0000} TO {1:yyyyMMdd2359}]";

            int defaultPageSize = config.Get<int>("DefaultPageSize");
            if (defaultPageSize > searchParameters.ReturnQuantityLimit) defaultPageSize = searchParameters.ReturnQuantityLimit;

            var query = searchParameters.Query;

            query += string.Format(FMT_DATE, searchParameters.DateFrom, searchParameters.DateTo);
            query += " AND (cat:" + Regex.Replace(searchParameters.Subject, @"\s+", "").Split(",").ToString(" OR cat:", x => { return x; }) + ")";

            query = Regex.Replace(query, @"\s+", FMT_SPACE);
            query = query.Replace("\"", FMT_QUOTE);
            query = query.Replace("(", FMT_OPEN_PARENTHESES);
            query = query.Replace(")", FMT_CLOSE_PARENTHESES);

            _queryBuilder.Add("search_query", query);
            _queryBuilder.Add("start", 0);
            _queryBuilder.Add("max_results", defaultPageSize);

            return config.Get<string>("SearchURI") + _queryBuilder.Build();
        }

        private bool ValidateFeed(XElement feed, out string errorMessage)
        {
            errorMessage = null;
            var entries = feed.Elements().Where(x => x.Name.LocalName == "entry").ToList();

            if (entries.Count == 1)
            {
                if (entries.First().Elements().Where(x => x.Name.LocalName == "title").FirstOrDefault()?.Value == "Error")
                {
                    errorMessage = entries.First().Elements().Where(x => x.Name.LocalName == "summary").FirstOrDefault()?.Value;
                    return false;
                }
                else return true;
            }
            else return true;
        }

        private ArxivFeedDTO BuildFeed(XElement feed)
        {
            return new ArxivFeedDTO()
            {
                Id = feed.Elements().Where(x => x.Name.LocalName == "id").FirstOrDefault()?.Value,
                ItemsPerPage = Convert.ToInt32(feed.Elements().Where(x => x.Name.LocalName == "itemsPerPage").FirstOrDefault()?.Value),
                TotalResults = Convert.ToInt32(feed.Elements().Where(x => x.Name.LocalName == "totalResults").FirstOrDefault()?.Value),
                StartIndex = Convert.ToInt32(feed.Elements().Where(x => x.Name.LocalName == "startIndex").FirstOrDefault()?.Value),
                Link = feed.Elements().Where(x => x.Name.LocalName == "link").FirstOrDefault()?.Attribute("href").Value,
                Title = feed.Elements().Where(x => x.Name.LocalName == "title").FirstOrDefault()?.Value,
                Updated = feed.Elements().Where(x => x.Name.LocalName == "updated").FirstOrDefault()?.Value,
                Entries = new List<ArxivEntryDTO>()
            };
        }

        private ArxivEntryDTO BuildEntry(XElement entry)
        {
            // Title
            string title = entry.Elements().Where(x => x.Name.LocalName == "title").FirstOrDefault()?.Value;

            // Id
            string id = entry.Elements().Where(x => x.Name.LocalName == "id").FirstOrDefault()?.Value;

            // Published
            DateTime published = DateTime.Parse(entry.Elements().Where(x => x.Name.LocalName == "published").FirstOrDefault()?.Value);

            // Updated
            DateTime updated = DateTime.Parse(entry.Elements().Where(x => x.Name.LocalName == "updated").FirstOrDefault()?.Value);

            // Summary
            string summary = entry.Elements().Where(x => x.Name.LocalName == "summary").FirstOrDefault()?.Value;

            // Authors
            List<ArxivAuthorDTO> authors = new List<ArxivAuthorDTO>();
            foreach (XElement author in entry.Elements().Where(x => x.Name.LocalName == "author")?.ToList())
            {
                authors.Add(new ArxivAuthorDTO() 
                { 
                    Name = author.Elements().Where(x => x.Name.LocalName == "name").FirstOrDefault()?.Value,
                    Affiliation = author.Elements().Where(x => x.Name.LocalName == "affiliation").FirstOrDefault()?.Value
                });
            }

            // Links
            List<ArxivLinkDTO> links = new List<ArxivLinkDTO>();
            foreach (XElement link in entry.Elements().Where(x => x.Name.LocalName == "link")?.ToList())
            {
                links.Add(new ArxivLinkDTO()
                {
                    HRef = link.Attribute("href")?.Value,
                    Rel = link.Attribute("rel")?.Value,
                    Title = link.Attribute("title")?.Value,
                    Type = link.Attribute("type")?.Value
                });
            }

            // Categories
            List<ArxivCategoryDTO> categories = new List<ArxivCategoryDTO>();
            foreach (XElement category in entry.Elements().Where(x => x.Name.LocalName == "category")?.ToList())
            {
                categories.Add(new ArxivCategoryDTO()
                {
                    Schema = category.Attribute("scheme")?.Value,
                    Term = category.Attribute("term")?.Value
                });
            }

            // PrimaryCateogory
            ArxivCategoryDTO primaryCategory = new ArxivCategoryDTO()
            {
                Schema = entry.Elements().Where(x => x.Name.LocalName == "primary_category").FirstOrDefault()?.Attribute("scheme")?.Value,
                Term = entry.Elements().Where(x => x.Name.LocalName == "primary_category").FirstOrDefault()?.Attribute("term")?.Value,
            };

            // Comment
            string comment = entry.Elements().Where(x => x.Name.LocalName == "comment").FirstOrDefault()?.Value;

            // JournalRefference
            string journalRefference = entry.Elements().Where(x => x.Name.LocalName == "journal_ref").FirstOrDefault()?.Value;

            // DOI
            string doi = entry.Elements().Where(x => x.Name.LocalName == "doi").FirstOrDefault()?.Value;

            return new ArxivEntryDTO()
            {
                Title = title,
                Id = id,
                Published = published,
                Updated = updated,
                Summary = summary,
                Authors = authors,
                Links = links,
                Categories = categories,
                PrimaryCateogory = primaryCategory,
                Comment = comment,
                JournalRefference = journalRefference,
                DOI = doi
            };
        }

        public async Task<IEnumerable<ArxivEntryDTO>> GetAllRecordsAsync(PublicationsCollectorParametersPLG searchParameters, PluginConfigurationController config, CancellationToken cancellationToken = default)
        {
            XDocument doc;
            List<XElement> entries;
            ArxivFeedDTO feed = null;
            bool maxReached = false;
            int remaining = searchParameters.ReturnQuantityLimit;
            var searchQuery = BuildSearchQuery(searchParameters, config);

            _logger.LogInformation("Requesting {ReturnQuantityLimit} publications...", searchParameters.ReturnQuantityLimit);

            do
            {
                using (var xmlReader = XmlReader.Create(searchQuery, new XmlReaderSettings() { Async = true }))
                {
                    doc = await XDocument.LoadAsync(xmlReader, LoadOptions.PreserveWhitespace, cancellationToken);

                    if (!ValidateFeed(doc.Root, out string errorMessage)) throw new Exception(errorMessage);

                    // Armamos el feed...
                    if (feed is null) feed = BuildFeed(doc.Root);
                    if (feed.TotalResults == 0) break;

                    // Armamos los entries...
                    entries = doc.Root.Elements().Where(x => x.Name.LocalName == "entry").ToList();
                    foreach (var entry in entries) feed.Entries.Add(BuildEntry(entry));

                    _logger.LogInformation($"Retrieved {feed.Entries.Count()} / {searchParameters.ReturnQuantityLimit} of {feed.TotalResults} total records.");

                    remaining -= entries.Count();

                    if (remaining > 0)
                    {
                        int prevStart = Convert.ToInt32(_queryBuilder.Get("start"));
                        _queryBuilder.Remove("start"); 
                        _queryBuilder.Add("start", prevStart + feed.ItemsPerPage);

                        if (remaining < feed.ItemsPerPage)
                        {
                            _queryBuilder.Remove("max_results");
                            _queryBuilder.Add("max_results", remaining);
                        }
                        await Task.Delay(config.Get<int>("PollingDelayInMs"), cancellationToken);
                    }
                    else maxReached = true;
                }                
                searchQuery = config.Get<string>("SearchURI") + _queryBuilder.Build();

            } while (!maxReached);

            return feed.Entries;
        }
    }
}