using Dominio.Repository;
using Dominio.Services;
using Infraestrutura;
using Infraestrutura.Repositorios;
using Ninject.Modules;
using Ninject.Web.Common;
using TCCPontoEletronico.AppService.Entity;
using TCCPontoEletronico.AppService.Interface;

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
            Bind<ISolicitacaoRepository>().To<SolicitacaoRepository>();
            Bind<IFeriasRepository>().To<FeriasRepository>();
            Bind<IFolgaRepository>().To<FolgaRepository>();
            Bind<IHorarioDeExpedienteRepository>().To<HorarioDeExpedienteRepository>();
            
            //Servicos
            Bind<IPontoEletronicoService>().To<PontoEletronicoService>();
            Bind<IEmployeeService>().To<EmployeeService>();
            Bind<ILoginService>().To<LoginService>();
            Bind<IVacationService>().To<VacationService>();
            Bind<IClearanceService>().To<ClearanceService>();

        }
    }
}