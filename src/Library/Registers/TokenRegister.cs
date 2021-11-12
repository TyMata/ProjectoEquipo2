using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary
{
    /// <summary>
    /// Esta clase  representa un registro de tokens
    /// </summary>
    public class TokenRegister
    {   
        /// <summary>
        /// Diccionario con nombre de empresas y sus respectivos tokens habilitados
        /// </summary>
        /// <returns></returns>
        public  Dictionary<string , Company> TokenList = new Dictionary<string,Company>();

        /// <summary>
        /// Verifica si el token es valido
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public Company IsValidToken(string codigo)
        {
            if (!TokenList.ContainsKey(codigo))
            {
                throw new Exception();
            }
            return TokenList[codigo];

            
        }

        /// <summary>
        /// Metodo par añadir un token al diccionario de tokens
        /// </summary>
        /// <param name="token"></param>
        /// <param name="company"></param>
        public void Add(string token,Company company)
        {
            this.TokenList.Add(token,company);
        }

        /// <summary>
        /// Metodo para remover un token del diccionario de tokens
        /// </summary>
        /// <param name="token"></param>
        public void Remove(string token)
        {
            this.TokenList.Remove(token);
        }


        /// <summary>
        /// Por la ley de demeter se crea Contains
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public bool Contains(string codigo)
        {
            if(this.TokenList.ContainsKey(codigo))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //  public static string GenerateToken(string nameCompany)
        // {
        //     Random rnd = new Random();
        //     StringBuilder token = new StringBuilder();
        //     bool response;
        //     Company company = Singleton<CompanyRegister>.Instance.GetCompanyByUserId(int id);
        //     if(response)
        //     {    for (int i = 0; i < 3; i++)         //Creo un nuevo token
        //         {
        //             int num = rnd.Next(10000, 100000);
        //             token.Append(num.ToString());
        //             if (i != 2) token.Append("-");
        //         }
        //         if (TokenRegister.TokenList.ContainsKey(company))        //Me fijo si ya existe la empresa y de ser asi le añado el Token a la lista
        //         {
        //             List<string> listaActualizada;
        //             TokenRegister.TokenList.TryGetValue(company, out listaActualizada);
        //             listaActualizada.Add(token.ToString());
        //             TokenRegister.TokenList[company]= listaActualizada;
        //             return token.ToString();
        //         }
        //     }
            
        //     return "No existe la empresa";   //CAMBIAR  POR EXCEPCION
        // }
    }
}