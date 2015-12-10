using Dominio.Interface;
using Dominio.Model;
using Seedwork.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Model
{

    public class Funcionario : EntityBase, IEntityBase<Funcionario>
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
        public string Bloqueado { get; set; }
        public virtual Empresa Empresa { get; set; }
        public virtual PerfilDeAcesso PerfilDeAcesso { get; set; }
        public virtual HorarioDeExpediente HorarioDeExpediente { get; set; }

        /// <summary>
        /// Método para dizer se o funcionário tem o mínimo de informações para ser considerado válido
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public bool IsValid(Funcionario employee)
        {
            var response = true;

            if (employee.HorarioDeExpediente == null || employee.PerfilDeAcesso == null || employee.Empresa == null|| 
                employee.Bloqueado.Equals("S") || !string.IsNullOrWhiteSpace(employee.Email))
            {
                response = false;
            }

            return response;
        }
    }

}
