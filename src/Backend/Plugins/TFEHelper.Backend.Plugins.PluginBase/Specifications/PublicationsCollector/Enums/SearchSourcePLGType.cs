using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFEHelper.Backend.Plugins.PluginBase.Specifications.PublicationsCollector.Enums
{
    public enum SearchSourcePLGType
    {
        ACMDigitalLibrary = 0,
        SEDICI = 1,
        ScienceDirect = 2,
        SpringerLink = 3,
        IEEEXplore = 4,
        Manual = 5,
        PubMed = 6,
        Scopus = 7,
        Eric = 8,
        Doaj = 9,
        arXiv = 10
    }
}
