using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace FrontEnd.Models
{
    public class EmpresaEditar : EntityModel
    {        

        [Description("Razão Social")]
        public string RazaoSocial { get; set; }
        
        public string NomeFantasia { get; set; }
        public string Cnpj { get; set; }
        public int NumeroEndereco { get; set; }
        
        [DisplayName("Bloqueado?")]
        public bool Bloqueado { get; set; }
        public string Cep { get; set; }
        public string Rua { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string Pais { get; set; }
    }
}