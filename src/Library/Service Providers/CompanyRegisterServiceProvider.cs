using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary
{
    /// <summary>
    /// Esta clase  representa un registro de tokens
    /// </summary>
    public class CompanyRegisterServiceProvider
    {
        /// <summary>
       /// Se añande  empresa a la lista de empresas
       /// </summary>
       /// <param name="company"></param>
        public static void AddCompany(Company company)
        {
            CompanyRegister.CompanyList.Add(company);
        }
        /// <summary>
        /// Remueve una empresa de la lista de empresa registrados
        /// </summary>
        /// <param name="Id"></param>
        public void RemoveCompany(int Id)
        {
            if (CompanyRegister.CompanyList != null)
            {
                foreach (Company x in CompanyRegister.CompanyList)
                {
                   if (x.Id.Equals(Id)) 
                   {
                       CompanyRegister.CompanyList.Remove(x);
                   }
                }
            }
        }
        /// <summary>
        /// Busca una oferta por el nombre y devuelve el objeto Company correspondiente a la empresa    
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static Company SearchCompany(string name)
        {
            if (CompanyRegister.CompanyList != null)
            {
                foreach (Company x in CompanyRegister.CompanyList)
                {
                   if (x.Name.Equals(name)) 
                   {
                       return x;
                   }
                }
            }
            return null; //CAMBIAR POR EXCEPCION
        }
    }
}