using Dominio.Repository;
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
            Bind<PontoContext>().ToSelf().InRequestScope();

            //Repositorios
            Bind<IEmpresaRepository>().To<EmpresaRepository>();
            Bind<IFuncionarioRepository>().To<FuncionarioRepository>();
            Bind<IPerfilDeAcessoRepository>().To<PerfilDeAcessoRepository>();
            Bind<IPontoRepository>().To<PontoRepository>();
            Bind<ISolicitacaoRepository>().To<SolicitacaoRepository>();
            Bind<IFeriasRepository>().To<FeriasRepository>();
            Bind<IFolgaRepository>().To<FolgaRepository>();
            Bind<IHorarioDeExpedienteRepository>().To<HorarioDeExpedienteRepository>();
            Bind<IItemHorarioDeExpedienteRepository>().To<ItemHorarioDeExpedienteRepository>();

            //Servicos
            Bind<IPontoEletronicoService>().To<PontoEletronicoService>();
            Bind<IFuncionarioService>().To<FuncionarioService>();
            Bind<ILoginService>().To<LoginService>();
            Bind<IVacationService>().To<VacationService>();
            Bind<IClearanceService>().To<ClearanceService>();
            Bind<ISolicitationService>().To<SolicitationService>();
            Bind<IEmpresaService>().To<EmpresaService>();
            Bind<IHorarioDeExpedienteService>().To<HorarioDeExpedienteService>();
            Bind<IEmailService>().To<EmailService>();

            
        }
    }
}