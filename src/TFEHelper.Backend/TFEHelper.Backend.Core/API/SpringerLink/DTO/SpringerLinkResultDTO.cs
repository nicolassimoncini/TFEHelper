using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

namespace TFEHelper.Backend.Core.API.SpringerLink.DTO
{
    public class SpringerLinkResultDTO
    {
        public int Total { get; set; }
        public int Start { get; set; }
        public int PageLength { get; set; }
        public int RecordsDisplayed { get; set; }
    }
}

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.