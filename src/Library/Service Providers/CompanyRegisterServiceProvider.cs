using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary
{
    /// <summary>
    /// Esta clase se encarga de añadir, eliminar, suspender y buscar empresas
    /// </summary>
    public class CompanyRegisterServiceProvider
    {
        /// <summary>
       /// Se añande  empresa a la lista de empresas
       /// </summary>
       /// <param name="company"></param>
        public static void AddCompanyToCompanyRegister(Company company)
        {
            CompanyRegister.CompanyList.Add(company);
        }
        /// <summary>
        /// Remueve una empresa de la lista de empresa registrados
        /// </summary>
        /// <param name="Id"></param>
        public static void RemoveCompany(int id)
        {
            if (CompanyRegister.CompanyList != null)
            {
                foreach (Company x in CompanyRegister.CompanyList)
                {
                   if (x.id.Equals(id)) 
                   {
                       CompanyRegister.CompanyList.Remove(x);
                   }
                }
            }
        }
        /// <summary>
        /// Busca una oferta por el nombre y devuelve el objeto Company correspondiente a la empresa y un bool
        /// True si la encuentra, False si no la encuentra
        /// </summary>
        /// <param name="name"></param>
        /// <param name="response"></param>
        /// <returns></returns>
        public static Company SearchCompany(string name, out bool response)
        {
            
            if (CompanyRegister.CompanyList != null)
            {
                foreach (Company x in CompanyRegister.CompanyList)
                {
                   if (x.Name.Equals(name)) 
                   {
                       response = true;
                       return x;
                   }
                }
            }
            response = false;
            return null; //CAMBIAR POR EXCEPCION
        }
    }
}