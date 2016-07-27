using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seedwork.Entity
{
    public class Tools
    {
        public DadosEndereco BuscaCep(string cep)
        {
            DataSet ds = new DataSet();
            DadosEndereco endereco = new DadosEndereco();
            ds.ReadXml("http://viacep.com.br/ws/"+cep.Replace("-","")+"/xml/");
            endereco.logradouro = ds.Tables[0].Rows[0]["logradouro"].ToString();
            endereco.bairro = ds.Tables[0].Rows[0]["bairro"].ToString();
            endereco.cidade = ds.Tables[0].Rows[0]["localidade"].ToString();
            endereco.uf = ds.Tables[0].Rows[0]["uf"].ToString();
            return endereco;
        }

        
    }
}
