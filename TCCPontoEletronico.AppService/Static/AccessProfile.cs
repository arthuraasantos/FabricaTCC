using System;


namespace TCCPontoEletronico.AppService.Static
{
    internal static class AccessProfile
    {
        //ToDo refazer, implementando serviço para pegar do banco esses dados sem expor aqui HARDCODED
        public static Guid GetAdminId() => Guid.Parse("09C15773-D1A5-4DAA-A3B3-E8C6741D63FD");
        public static Guid GetManagerId() => Guid.Parse("4036B285-E7FB-4AC0-AAC4-5196FDE05DD8");
        public static Guid GetEmployeeId() => Guid.Parse("3ACC5625-0663-47C2-BB4D-E507EE5B004F");
      
    }
}
