using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary
{
    /// <summary>
    /// Esta clase  representa un registro de tokens
    /// </summary>
    public class TokenRegisterServiceProvider
    {
        /// <summary>
        /// Genera un nuevo token de invitacion
        /// </summary>
        /// <param name="company"></param>
        /// <returns></returns>
        public string GenerateToken(string company)
        {
            Random rnd = new Random();
            StringBuilder token = new StringBuilder();
            for (int i = 0; i < 3; i++)         //Creo un nuevo token
            {
                int num = rnd.Next(10000, 100000);
                token.Append(num.ToString());
                if (i != 2) token.Append("-");
            }
            if (TokenRegister.tokenList.ContainsKey(company))        //Me fijo si ya existe la empresa y de ser asi le añado el Token a la lista
            {
                List<string> listaActualizada;
                TokenRegister.tokenList.TryGetValue(company, out listaActualizada);
                listaActualizada.Add(token.ToString());
                TokenRegister.tokenList[company]= listaActualizada;
                return token.ToString();
            }
            
            return "No existe la empresa";   //CAMBIAR  POR EXCEPCION
        }
        /// <summary>
        /// Verifica si el token es valido
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="response"></param>
        /// <returns></returns>
        public bool IsValidToken(string codigo, out string response)
        {
            foreach (KeyValuePair<string,List<string>> x in TokenRegister.tokenList)
            {
                foreach (string token in x.Value)
                {
                    if (token.Equals(codigo))
                    {
                        response = x.Key;
                        return true;
                    }
                }
            }
            response = "No es valido el codigo";
            return false;
        }
    }
}