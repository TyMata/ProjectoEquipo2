using System;
using System.Collections.Generic;

namespace ClassLibrary
{   
    /// <summary>
    /// Esta clase representa un registro de empresas
    /// </summary>
    public class CompanyRegister
    {   
        /// <summary>
        /// Lista de empresas registrados
        /// </summary>
        /// <value></value>
        public List<Company> CompanyList { get; private set; }
        
       /// <summary>
       /// Se añande  empresa a la lista de empresas
       /// </summary>
       /// <param name="company"></param>
        public void AddCompany(Company company)
        {
            this.CompanyList.Add(company);
        }
        /// <summary>
        /// Remueve una empresa de la lista de empresa registrados
        /// </summary>
        /// <param name="Id"></param>
        public void RemoveCompany(int Id)
        {
            if (this.CompanyList != null)
            {
                foreach (Company x in CompanyList)
                {
                   if (x.id.Equals(Id)) 
                   {
                       this.CompanyList.Remove(x);
                   }
                }
            }
        }
    }
}