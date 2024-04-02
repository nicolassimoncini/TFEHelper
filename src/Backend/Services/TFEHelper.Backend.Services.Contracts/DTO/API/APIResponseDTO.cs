using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TFEHelper.Backend.Services.Contracts.DTO.API
{
    public sealed class APIResponseDTO
    {
        public APIResponseDTO()
        {
            ErrorMessages = new List<string>();
        }

        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccessful { get; set; } = true;
        public List<string> ErrorMessages { get; set; }
        public object Payload { get; set; }
        public int TotalPages { get; set; }
    }
}
