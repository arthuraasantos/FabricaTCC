using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FrontEnd.Models
{
    public class FuncionarioAlterarSenha : EntityModel
    {
        public string Nome { get; set; }
        public string SenhaAtual { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string SenhaNova { get; set; }

        [DataType(DataType.Password)]
        [Compare("SenhaNova")]  
        public string SenhaNovaConfirmacao { get; set; }
    }
}