using Neat.BibTeX.BibModel;
using Neat.BibTeX.BibParsers;
using Neat.Unicode;
using System.Reflection;
using TFEHelper.Backend.Domain.Attributes;
using TFEHelper.Backend.Domain.Classes.Models;
using TFEHelper.Backend.Domain.Enums;
using TFEHelper.Backend.Domain.Extensions;

namespace TFEHelper.Backend.Core.Processors.BibTeX
{
    internal class BibTeXProcessor
    {
        /// <summary>
        /// Importa el contenido de un archivo BibTeX (.bib) y lo retorna mapeado a una lista de <see cref="Publication"/>.
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="source"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<List<Publication>> ImportAsync(string filePath, SearchSourceType source, CancellationToken cancellationToken = default)
        {
            static string GetFieldValueAsString(Bib16Entry entry, string fieldName)
            {
                return ((Bib16GeneralEntry)entry)
                    .Fields.FirstOrDefault(x => x.Name == fieldName, new Bib16Field()).Value.OnlyComponent.NameOrLiteral;
            }

            return await Task.Run(async () =>
            {
                string str = await File.ReadAllTextAsync(filePath, cancellationToken);
                Bib16ParserCatchErrors parser = new Bib16ParserCatchErrors();
                List<Publication> publications = new List<Publication>();

                parser.Parse(str);                

                foreach (var entry in parser.Entries)
                {
                    publications.Add(new Publication()
                    {
                        Type = entry.Type.ToPublicationType(),
                        Key = ((Bib16GeneralEntry)entry).Key,
                        Abstract = GetFieldValueAsString(entry, "abstract"),
                        Authors = GetFieldValueAsString(entry, "author"),//GetFieldValueAsString(entry, "author")?.ToList<string>(new string[] { ";", " and ", }, (x) => { return x; }),
                        DOI = GetFieldValueAsString(entry, "doi"),                        
                        ISBN = GetFieldValueAsString(entry, "isbn"),
                        ISSN = GetFieldValueAsString(entry, "issn"),
                        Keywords = GetFieldValueAsString(entry, "keywords"), //GetFieldValueAsString(entry, "keywords")?.ToList<string>(new string[] { "," }, (x) => { return x; }),
                        Year = Convert.ToInt32(GetFieldValueAsString(entry, "year")),
                        Source = source,
                        Title = GetFieldValueAsString(entry, "title"),
                        URL = GetFieldValueAsString(entry, "url"),
                        Pages = GetFieldValueAsString(entry, "pages")
                    });
                }
                return publications;
            }, cancellationToken);
        }

        /// <summary>
        /// Exporta una lista de <typeparamref name="T"/> a un archivo indicado por <paramref name="filePath"/> bajo el formato BibTeX.
        /// </summary>
        /// <param name="list"></param>
        /// <param name="filePath"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task ExportAsync<T>(List<T> list, string filePath, CancellationToken cancellationToken = default) where T : class, IBibTeXRecord
        {
            string recordHeader = "@{0}{{{1}," + Environment.NewLine;
            string field = "{0} = {{{1}}}," + Environment.NewLine; ;
            string recordFooter = "}" + Environment.NewLine; ;

            string bibFile = string.Empty;
            string newRecord;
            
            foreach (var record in list)
            {
                // header
                //newRecord = string.Format(recordHeader, record.Type.ToString().ToLower(), record.Key);
                newRecord = string.Format(recordHeader, record.Type.ToString().ToLower(), record.Key.IsDefaultOrEmpty() ? "undefined" : record.Key);

                // cuerpo
                foreach (var fieldInfo in record.GetType().GetProperties().Where(p => p.GetCustomAttribute(typeof(BibTeXKeyAttribute)) == null))
                {
                    if (fieldInfo.GetValue(record, null) != null)
                        newRecord += string.Format(field, fieldInfo.Name.ToLower(), fieldInfo.GetValue(record, null));
                }

                // footer
                newRecord += recordFooter;
                bibFile += newRecord;
            }

            await File.WriteAllTextAsync(filePath, bibFile, cancellationToken);
        }
    }
}