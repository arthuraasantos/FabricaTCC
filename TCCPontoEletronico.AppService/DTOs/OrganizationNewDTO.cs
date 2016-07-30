

using System;

namespace TCCPontoEletronico.AppService.Interface.DTOs
{
    public class EmpresaNovoDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public EmpresaNovoDto()
        {

        }
        public EmpresaNovoDto(Guid organizationId, string name)
        {
            Id = organizationId;
            Name = name;
        }
    }
}
