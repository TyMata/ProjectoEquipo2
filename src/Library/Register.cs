using System;
using System.Collections.Generic;

namespace ClassLibrary
{
    /// <summary>
    /// Esta clase 
    /// </summary>
    public class Register
    {
        /// <summary>
        /// Lista de usuarios registrados
        /// </summary>
        /// <value></value>        
        public UserRegister Users {get; private set;}
        /// <summary>
        /// Lista de companias registradas
        /// </summary>
        /// <value></value>
        public List<Company> CompanyList{get;set;}        
        /// <summary>
        /// Añade una empresa a la lista de empresas registradas
        /// </summary>
        /// <param name="company"></param>
        public void AddCompany(Company company)
        
        {
            if(CompanyList.Contains(company)==false)
            {
                this.CompanyList.Add(company);
            }
            
        }
       
        /// <summary>
        /// Remueve una empresa de la lista de empresas registradas
        /// </summary>
        /// <param name="Id"></param>
        public void RemoveCompany(int Id)
        {
            foreach (Company x in this.CompanyList)
            {
                if(x.Id==Id)
                {
                    this.CompanyList.Remove(x);
                }
            }
        }
    }
}