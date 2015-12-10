
using Seedwork.Const;
using System.Collections.Generic;

namespace FrontEnd.Models
{
    public class JsonResponse
    {
        public JsonResponse()
        {
            IsValid = true;
            TypeResponse = TypeResponse.Success;
        }

        public bool IsValid { get; set; }
        public  TypeResponse TypeResponse { get; set; }
        public string Message { get; set; }

    }
}