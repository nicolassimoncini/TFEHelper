using RestSharp;
using RestSharp.Serializers;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;

namespace TFEHelper.Backend.Plugins.Pubmed.Classes
{

    internal class Serializer : ISerializer, IDeserializer
    {
        private readonly bool _ignoreDTD;
        private ContentType _contentType;
        ContentType ISerializer.ContentType { get => _contentType; set => _contentType = value; }

        public Serializer(bool ignoreDTD = true)
        {
            _ignoreDTD = ignoreDTD;
        }

        private string Sanitize(string data)
        {
            const string htmlTags = "a,b,br,i,p,q,sub,sup";
            htmlTags.Split(",").ToList()
                .ForEach(x => data = data
                    .Replace("<" + x + ">", "", true, CultureInfo.InvariantCulture)
                    .Replace("</" + x + ">", "", true, CultureInfo.InvariantCulture));

            return data;
        }

#nullable enable
        public T? Deserialize<T>(RestResponse response)
        {
            XmlReaderSettings settings = new XmlReaderSettings();

            if (_ignoreDTD)
            {
                settings.DtdProcessing = DtdProcessing.Ignore;
                settings.XmlResolver = null;
            };

            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using XmlReader reader = XmlReader.Create(new StringReader(Sanitize(response.Content)), settings);
            return (T?)serializer.Deserialize(reader);
        }

        public string Serialize(object obj)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(obj.GetType());

            using (StringWriter textWriter = new StringWriter())
            {
                xmlSerializer.Serialize(textWriter, obj);
                return textWriter.ToString();
            }
        }
    }
#nullable disable

    internal class XMLCustomSerializer : IRestSerializer
    {
        private readonly Serializer _serializer;

        public ISerializer Serializer => _serializer;
        public IDeserializer Deserializer => _serializer;

        public string[] AcceptedContentTypes { get; } = ["text/xml", "application/xml"];
        public SupportsContentType SupportsContentType => (x) => x == ContentType.Xml;
        public DataFormat DataFormat => DataFormat.Xml;

        public XMLCustomSerializer(bool ignoreDTD = true)
        {
            _serializer = new Serializer(ignoreDTD);
        }
        
        public string Serialize(Parameter parameter)
        {
            return _serializer.Serialize(parameter.Value);
        }
    }
}
