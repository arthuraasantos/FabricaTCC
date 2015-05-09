using Dominio.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data;
using System.Net;
using System.IO;
using System.ComponentModel.DataAnnotations;

namespace FrontEnd.Models
{
    public class FuncionarioEditar : EntityModel
    {
        public string Nome { get; set; }

        public string Email { get; set; }

        public string Senha { get; set; }

        public DateTime? DataNascimento { get; set; }

        public string NumeroEndereco { get; set; }

        public string Cpf { get; set; }

        public string Identidade { get; set; }

        public decimal SalarioBase { get; set; }

        public string Cep { get; set; }

        public string Logradouro { get; set; }

        public string Bairro { get; set; }

        public string Cidade { get; set; }

        public string Estado { get; set; }

        public string Pais { get; set; }


        public Guid IdEmpresa { get; set; }
        public Guid IdPerfilDeAcesso { get; set; }

    }
}