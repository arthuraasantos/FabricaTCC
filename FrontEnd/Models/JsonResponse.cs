
using System.Collections.Generic;

namespace FrontEnd.Models
{
    public class JsonResponse
    {
        public JsonResponse()
        {

        }

        public bool IsValid { get; set; }
        public string TypeResponse { get; set; }
        public string Message { get; set; }

    }
}