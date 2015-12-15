

using System;

namespace TCCPontoEletronico.AppService.Interface.DTOs
{
    public class OrganizationNewDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public OrganizationNewDTO()
        {

        }
        public OrganizationNewDTO(Guid organizationId, string name)
        {
            Id = organizationId;
            Name = name;
        }
    }
}
