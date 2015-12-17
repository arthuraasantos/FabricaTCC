

namespace TCCPontoEletronico.AppService.Interface.DTOs
{
    public class NewRegisterDTO
    {
        public string OrganizationName { get; set; }
        public string EmployeeName{ get; set; }
        public string EmployeeCpf{ get; set; }
        public string EmployeeEmail{ get; set; }
        public string EmployeePassword { get; set; }

        public NewRegisterDTO(string fantasyName, string employeeName, string employeeCpf, string employeeEmail)
        {
            OrganizationName = fantasyName;
            EmployeeName = employeeName;
            EmployeeCpf = employeeCpf;
            EmployeeEmail = employeeEmail;
        }
    }
}
