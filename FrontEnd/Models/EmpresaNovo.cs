using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FrontEnd.Models
{
    public class EmpresaNovo : EntityModel
    {  
        public string RazaoSocial { get; set; }
        public string Cnpj { get; set; }
        public string NomeFantasia { get; set; }
        
    }
}