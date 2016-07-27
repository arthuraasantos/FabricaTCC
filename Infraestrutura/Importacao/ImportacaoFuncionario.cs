using System;
using System.Collections.Generic;
using Seedwork.Const;
using System.Linq;
using Dominio.Model;
using System.Reflection;
using static SeedWork.Tools.Validacao;
using System.Globalization;
using Dominio.Repository;

namespace Infraestrutura.Importacao
{
    public class ImportacaoFuncionario : Importacao
    {
        private readonly IFuncionarioRepository _funcionarioRepository;
        private string[] camposFuncionario = { "Bairro", "Cep", "Cpf", "Cidade", "Email", "Estado", "Logradouro", "DataNascimento", "Nome", "Número", "Senha" };

        public ImportacaoFuncionario(IFuncionarioRepository funcionarioRepository)
        {
            _funcionarioRepository = funcionarioRepository;
        }

        public override bool ImportarTextoPlanilha(List<string[]> textoPlanilha, string[] relacaoColunas, TipoImportacao tipoImportacao)
        {
            if (tipoImportacao != TipoImportacao.Funcionario)
                throw new ArgumentException("Tipo de importação inválido para essa operação");

            int[] indicesRelacionados = { };
            var listaDosNomes = indicesRelacionados.ToList();

            for (int i = 0; i < relacaoColunas.GetLength(0); i++)
            {
                listaDosNomes.Add(int.Parse(relacaoColunas[i]));
            }

            indicesRelacionados = listaDosNomes.ToArray();

            #region Pega o indice e transforma em nomes para criar o objeto Funcionario

            var nomeColunasRelacionadas = (new string[] { }).ToList();
            
            for (int j = 0; j < indicesRelacionados.Length; j++)
            {
                nomeColunasRelacionadas.Add(camposFuncionario.GetValue(indicesRelacionados[j]).ToString());
            }

            var nomeColunasRelacionadasArray = nomeColunasRelacionadas.ToArray();
            #endregion

            #region Baseado na lista de nomes, monta lista de funcionarios para inserir no sistema 

            var novosFuncionarios = new List<Funcionario>();

            for (int i = 0; i < textoPlanilha.ToArray().Length; i++)
            {
                var novoFuncionario = new Funcionario();

                for (int j = 0; j < indicesRelacionados.Length; j++)
                {
                    Type tipoFuncionario = typeof(Funcionario);
                    var propriedade = tipoFuncionario.GetProperty(nomeColunasRelacionadasArray[j]);
                    var tipoDesejado = IsNullableType(propriedade.PropertyType) ? Nullable.GetUnderlyingType(propriedade.PropertyType) : propriedade.PropertyType;
                    #region Formata campo de acordo com seu tipo

                   
                    switch (nomeColunasRelacionadasArray[j])
                    {
                        case "DataNascimento":
                            string data = $"{textoPlanilha[i][j]} 00:00";
                            CultureInfo cult = new CultureInfo("pt-BR");
                            novoFuncionario.DataNascimento = DateTime.Parse(data,cult);
                            break;
                        default:
                            propriedade.SetValue(novoFuncionario, Convert.ChangeType(textoPlanilha[i][j], tipoDesejado), null);
                            break;
                    }

                    #endregion
                }

                novosFuncionarios.Add(novoFuncionario);
            }


            #endregion

            #region Inserir Funcionarios no Banco

            novosFuncionarios.ForEach(funcionario =>
            {
                _funcionarioRepository.Salvar(funcionario);
            });
            _funcionarioRepository.Executar();

            #endregion
            return true;
        }


    }
}
