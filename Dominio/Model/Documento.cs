using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Model
{
    public class Documento
    {
        public string Numero { get; private set; }
        public TipoDocumento Tipo { get; set; }

        public Documento()
        {

        }

        public Documento(string numero)
        {
            SetarNumero(numero);
            Tipo = IdentificarTipoDocumento(Numero);
        }

        public void SetarNumero(string numero)
        {
            Numero = numero;
            Tipo = IdentificarTipoDocumento(numero);
        }

        public static implicit operator Documento(string numero) 
            => new Documento(numero);
        
        

        public static implicit operator string(Documento documento) => documento.Numero;
            
        public TipoDocumento IdentificarTipoDocumento(string documento)
        {
            return documento.Length > 11 ? TipoDocumento.Cnpj : TipoDocumento.Cpf;
        }
    }

    public class Teste
    {
        public void Main()
        {
            var novoDocumento = new Documento("012345678919");
        }
    }

    public enum TipoDocumento
    {
        Cpf,
        Cnpj
    }
}
