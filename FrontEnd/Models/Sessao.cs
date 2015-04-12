using Dominio.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FrontEnd.Models
{
    public static class Sessao
    {
        private static Funcionario _Funcionario;
        private static Empresa _Empresa;

        public static Funcionario FuncionarioLogado
        {
            get
            {
                _Funcionario = (Funcionario)System.Web.HttpContext.Current.Session["Funcionario"];
                return _Funcionario;
            }
        }

        public static Empresa EmpresaLogada
        {
            get
            {
                _Empresa = FuncionarioLogado.Empresa;
                return _Empresa;
            }
        }

    }
}