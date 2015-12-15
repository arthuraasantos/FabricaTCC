using Dominio.Model;
using Seedwork.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Repository
{
    public interface IFuncionarioRepository : IRepository<Funcionario>
    {
        Funcionario PesquisaParaLogin(string email, string senha);
        Funcionario PesquisaPeloEmail(string email);
        IQueryable<Funcionario> ListarComPerfil(PerfilDeAcesso Perfil);
        int GetCount();
        

    }
}
