using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FrontEnd.Models
{
    public class PerfilDeAcessoPadrao
    {
        public Guid Id { get; set; }
        public string Descricao { get; set; }
        //private Guid IdPerfilRH
        //{
        //    get
        //    {
        //        //string id = "9d5e197a-9f4f-4568-9229-8d571e9f1ced";
        //        //IdPerfilRH = Guid.Parse(id);
        //        //return IdPerfilRH;
        //    }
        //    set{ }
        //}
        //private Guid IdPerfilFuncionario
        //{
        //    get
        //    {
        //        string id = "e43484ea-d65d-4d92-ab1e-3f414c00baf2";
        //        IdPerfilFuncionario = Guid.Parse(id);
        //        return IdPerfilFuncionario;

        //    }
        //    set { }
        //}

        //public List<PerfilDeAcessoPadrao> ListarPerfilDeAcessoPadrao()
        //{
        //    return new List<PerfilDeAcessoPadrao>
        //    {
        //        new PerfilDeAcessoPadrao { Id = IdPerfilRH, Descricao = "SetorRH"},
        //        new PerfilDeAcessoPadrao { Id = IdPerfilFuncionario, Descricao = "Funcionário"}
        //    };
        //}

    }
}