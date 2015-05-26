using Dominio.Model;
using Infraestrutura;
using Infraestrutura.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FrontEnd.Models
{
    public class FuncionarioNovo : EntityModel
    {
        public string Nome { get; set; }

        public string Cpf { get; set; }

        public string Identidade { get; set; }
        public string Email { get; set; }

        public string Senha { get; set; }
        public string SenhaConfirmacao { get; set; }

        public DateTime? DataNascimento { get; set; }

        public Guid? IdEmpresa { get; set; }
        public Guid? IdPerfilDeAcesso { get; set; }

        public IEnumerable<SelectListItem> Empresas { get; set; }
        public IEnumerable<SelectListItem> PerfisDeAcesso { get; set; }


        

    }
}