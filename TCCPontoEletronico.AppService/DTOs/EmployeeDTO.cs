
using System;

namespace TCCPontoEletronico.AppService.Interface.DTOs
{
    public class FuncionarioDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string OrganizationFantasyName { get; set; }
        public string Password { get; set; }
        
    }
}
