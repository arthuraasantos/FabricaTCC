using Dominio.Repository;
using Infraestrutura.Mapeamento;
using Infraestrutura.Repositorios;
using Infraestrutura.UnitOfWork;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Infraestrutura
{
    public class PontoContext : DbContext, IUnitOfWork
    {
        private readonly PontoContext _contexto;

        public PontoContext() : base(ConfigurationManager.ConnectionStrings["pontoConn"].ConnectionString)
        {
            var ensureDllIsCopied =
                System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }

        #region DbSets

        private IEmpresaRepository _empresas;
        private IHorarioDeExpedienteRepository _horarioDeExpediente;
        private IFuncionarioRepository _funcionarios;
        private IFeriasRepository _ferias;
        private IFolgaRepository _folgas;
        private IPerfilDeAcessoRepository _perfilDeAcesso;
        private IPontoRepository _pontos;
        private ISolicitacaoRepository _solicitacao;


        public IEmpresaRepository Empresas {
            get
            {
                if (_empresas == null) _empresas = new EmpresaRepository(_contexto);
                return _empresas;
            }
        }

        public IHorarioDeExpedienteRepository HorarioDeExpediente
        {
            get
            {
                if (_horarioDeExpediente == null) _horarioDeExpediente = new HorarioDeExpedienteRepository(_contexto);
                return _horarioDeExpediente;
            }
        }

        public IFuncionarioRepository Funcionarios {
            get
            {
                if (_funcionarios == null) _funcionarios = new FuncionarioRepository(_contexto);
                return _funcionarios;
            }
        }

        public IFeriasRepository Ferias {
            get
            {
                if (_ferias == null) _ferias = new FeriasRepository(_contexto);
                return _ferias;
            }
        }

        public IFolgaRepository Folgas {
            get
            {
                if (_folgas == null) _folgas = new FolgaRepository(_contexto);
                return _folgas;
            }
        }

        public IPerfilDeAcessoRepository PerfilDeAcesso {
            get
            {
                if (_perfilDeAcesso == null) _perfilDeAcesso = new PerfilDeAcessoRepository(_contexto);
                return _perfilDeAcesso;
            }
        }

        public IPontoRepository Pontos {
            get
            {
                if (_pontos == null) _pontos = new PontoRepository(_contexto);
                return _pontos;
            }
        }

        public ISolicitacaoRepository Solicitacao {
            get
            {
                if (_solicitacao == null) _solicitacao = new SolicitacaoRepository(_contexto);
                return _solicitacao;
            } }

        #endregion

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new FuncionarioDbMapping());
            modelBuilder.Configurations.Add(new PerfilDeAcessoDbMapping());
            modelBuilder.Configurations.Add(new PontoDbMapping());
            modelBuilder.Configurations.Add(new EmpresaDbMapping());
            modelBuilder.Configurations.Add(new SolicitacaoDbMapping());
            modelBuilder.Configurations.Add(new FeriasDbMapping());
            modelBuilder.Configurations.Add(new FolgaDbMapping());
            modelBuilder.Configurations.Add(new HorarioDeExpedienteDBMapping());

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }

        public void Commit() => _contexto.SaveChanges();

    }
}
