﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TFEHelper.Backend.Plugins.Pubmed.DTO.EFetch
{
    public class PubmedEFetchArticlePublicationTypeDTO
    {
        [XmlAttribute("UI")]
        public string UI { get; set; }

        [XmlText]
        public string Value { get; set; }
    }
}
