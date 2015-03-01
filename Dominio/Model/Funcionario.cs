using Dominio.Model;
using Seedwork.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Model
{
    public class Funcionario : EntityBase
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


        public virtual Empresa Empresa { get; set; }

        public virtual PerfilDeAcesso PerfilDeAcesso { get; set; }

        public string TESTE { get; set;}
 
        
    }
}
