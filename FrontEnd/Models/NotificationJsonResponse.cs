
using System.Collections.Generic;

namespace FrontEnd.Models
{
    public class NotificationJsonResponse: DefaultJsonResponse
    {
        public List<string> Messages { get; set; }
    }
}