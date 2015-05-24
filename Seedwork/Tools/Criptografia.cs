using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace SeedWork.Tools
{
    public static class Criptografia
    {
        public static string Encrypt(string senha)
        {
            try
            {
                MD5 md5 = new MD5CryptoServiceProvider();
                md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(senha));

                byte[] criptografia = md5.Hash;

                StringBuilder resultado = new StringBuilder();

                for (int i = 0; i < criptografia.Length; i++)
                {
                    resultado.Append(criptografia[i].ToString("x"));
                }

                return resultado.ToString();
            }
            catch
            {
                throw;
            }
        }
    }
}
