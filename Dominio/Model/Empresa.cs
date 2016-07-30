using Dominio.Interface;
using Seedwork.Entity;
using System.Collections.Generic;
using System;

namespace Dominio.Model
{
    public class Empresa : EntityBase
    {
        public string RazaoSocial { get; set; }
        public string NomeFantasia { get; set; }
        public string Cnpj { get; set; }

        public int? NumeroEndereco { get; set; }
        public string Bloqueado { get; set; }
        public string Cep { get; set; }
        public string Logradouro { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string Pais { get; set; }
        public virtual List<Funcionario> Funcionarios { get; set; }

    }
}
