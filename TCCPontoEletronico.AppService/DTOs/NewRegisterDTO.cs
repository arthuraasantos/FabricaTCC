

namespace TCCPontoEletronico.AppService.Interface.DTOs
{
    public class NewRegisterDTO
    {
        public string NomeFantasiaEmpresa { get; set; }
        public string CnpjEmpresa { get; set; }
        public string NomeFuncionario { get; set; }
        public string EmailFuncionario { get; set; }
        public string SenhaFuncionario { get; set; }

        public NewRegisterDTO(string nomeFantasia, string cnpj, string emailFuncionario, string nomeFuncionario, string senha)
        {
            NomeFantasiaEmpresa = nomeFantasia;
            CnpjEmpresa = cnpj;
            NomeFuncionario = nomeFuncionario;
            EmailFuncionario = emailFuncionario;
            SenhaFuncionario = senha;
        }
        
        public bool IsValid()
        {
            if (string.IsNullOrWhiteSpace(NomeFantasiaEmpresa))
                return false;
            if (string.IsNullOrWhiteSpace(CnpjEmpresa))
                return false;
            if (string.IsNullOrWhiteSpace(NomeFuncionario))
                return false;
            if (string.IsNullOrWhiteSpace(EmailFuncionario))
                return false;
            if (string.IsNullOrWhiteSpace(SenhaFuncionario))
                return false;

            return true;

        }

        

        
    }
}
