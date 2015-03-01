using Dominio.Repository;
using Dominio.Repository;
using Dominio.Services;
using Infraestrutura;
using Infraestrutura.Repositorios;
using Ninject.Modules;
using Ninject.Web.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FrontEnd.App_Start
{
    public class PontoNinjectModule : NinjectModule
    {
        
        public override void Load()
        {
            //Contexto
            Bind<MyContext>().ToSelf().InRequestScope();

            //Repositorios
            Bind<IEmpresaRepository>().To<EmpresaRepository>();
            Bind<IFuncionarioRepository>().To<FuncionarioRepository>();
            Bind<IPerfilDeAcessoRepository>().To<PerfilDeAcessoRepository>();
            Bind<IPontoRepository>().To<PontoRepository>();

            //Servicos
            Bind<IPontoEletronicoService>().To<PontoEletronicoService>();
        }
    }
}