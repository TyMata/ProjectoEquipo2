using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary
{
    /// <summary>
    /// Esta clase 
    /// </summary>
    public class TokenRegister
    {
        public static Dictionary<string , List<string>> TokenList = new Dictionary<string, List<string>>();

        public static string GenerateToken(string company)
        {
            Random rnd = new Random();
            StringBuilder token = new StringBuilder();
            for (int i = 0; i < 3; i++)         //Creo un nuevo token
            {
                int num = rnd.Next(10000, 100000);
                token.Append(num.ToString());
                if (i != 2) token.Append("-");
            }
            if (TokenList.ContainsKey(company))        //Me fijo si ya existe la empresa y de ser asi le añado el Token a la lista
            {
                List<string> listaActualizada;
                TokenList.TryGetValue(company, out listaActualizada);
                listaActualizada.Add(token.ToString());
                TokenList[company]= listaActualizada;
                return token.ToString();
            }
            
            return "No existe la empresa";   //CAMBIAR  POR EXCEPCION
        }
        public static bool IsValidToken(string codigo, out string response)
        {
            foreach (KeyValuePair<string,List<string>> x in TokenList)
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