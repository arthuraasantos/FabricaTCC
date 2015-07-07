using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FrontEnd.Models
{
    public class UF
    {
        public string Valor { get; set; }
        public string Descricao { get; set; }
        public List<UF> Listar()
        {
            return new List<UF>{
                new UF{ Valor = "AC", Descricao = "Acre"},
                new UF{ Valor = "AP", Descricao = "Amapa"},
                new UF{ Valor = "AM", Descricao = "Amazonas"},
                new UF{ Valor = "BA", Descricao = "Bahia"},
                new UF{ Valor = "CE", Descricao = "Ceará"},
                new UF{ Valor = "DF", Descricao = "Distrito Federal"},
                new UF{ Valor = "GO", Descricao = "Goiás"},
                new UF{ Valor = "MA", Descricao = "Maranhão"},
                new UF{ Valor = "MT", Descricao = "Mato Grosso"},
                new UF{ Valor = "MS", Descricao = "Mato Grosso do Sul"},
                new UF{ Valor = "MG", Descricao = "Minas Gerais"},
                new UF{ Valor = "PA", Descricao = "Pará"},
                new UF{ Valor = "PB", Descricao = "Paraíba"},
                new UF{ Valor = "PR", Descricao = "Paraná"},
                new UF{ Valor = "PE", Descricao = "Pernambuco"},
                new UF{ Valor = "PI", Descricao = "Piauí"},
                new UF{ Valor = "RJ", Descricao = "Rio de Janeiro"},
                new UF{ Valor = "RN", Descricao = "Rio Grande do Norte"},
                new UF{ Valor = "RS", Descricao = "Rio Grande do Sul"},
                new UF{ Valor = "RO", Descricao = "Rondônia"},
                new UF{ Valor = "RR", Descricao = "Roraima"},
                new UF{ Valor = "SC", Descricao = "Santa Catarina"},
                new UF{ Valor = "SP", Descricao = "São Paulo"},
                new UF{ Valor = "SE", Descricao = "Sergipe"},
                new UF{ Valor = "TO", Descricao = "Tocantins"}
            };
        }
    }
}