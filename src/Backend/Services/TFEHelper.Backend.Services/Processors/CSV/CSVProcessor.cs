using CsvHelper;
using System.Globalization;
using TFEHelper.Backend.Domain.Classes.Models;
using TFEHelper.Backend.Domain.Enums;

namespace TFEHelper.Backend.Services.Processors.CSV
{
    internal static class CSVProcessor
    {
        /// <summary>
        /// Importa el contenido de un archivo csv y lo retorna mapeado a una lista de <see cref="Publication"/>.
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<List<Publication>> ImportAsync(string filePath, SearchSourceType source, CancellationToken cancellationToken = default)
        {
            List<Publication> result = new();

            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = csv.GetRecordsAsync<Publication>(cancellationToken).GetAsyncEnumerator(cancellationToken);
                
                while (await records.MoveNextAsync())
                {
                    records.Current.Source = source;
                    result.Add(records.Current);
                }                    
            }

            return result;
        }

        /// <summary>
        /// Graba el contenido de una lista de <see cref="Publication"/> en un archivo csv especificado en <paramref name="filePath"/>.
        /// </summary>
        /// <param name="list"></param>
        /// <param name="filePath"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task ExportAsync(List<Publication> list, string filePath, CancellationToken cancellationToken = default)
        {
            using (var writer = new StreamWriter(filePath))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteHeader<Publication>();
                await csv.WriteRecordsAsync(list, cancellationToken);
            }
        }
    }
}