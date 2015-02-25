using Dominio.Model;
using Infraestrutura;
using Infraestrutura.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FrontEnd.Models
{
    public class FuncionarioNovo : EntityModel
    {
        public string Nome { get; set; }

        public string Cpf { get; set; }

        public string Identidade { get; set; }
        public string Email { get; set; }

        public DateTime? DataNascimento { get; set; }

        public Guid IdEmpresa { get; set; }
        public Guid IdPerfilDeAcesso { get; set; }


        

    }
}