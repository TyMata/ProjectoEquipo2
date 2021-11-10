using System;
using System.Collections.Generic;

namespace ClassLibrary
{   
    /// <summary>
    /// Esta clase representa un registro de empresas
    /// </summary>
    public class CompanyRegister
    {   
        private List<Company> companyList = new List<Company>();
        /// <summary>
        /// Lista de empresas registrados
        /// </summary>
        /// <value></value>
        public  List<Company> CompanyList {get;}
        /// <summary>
        /// Por la ley de demeter
        /// </summary>
        /// <param name="company"></param>
        public void Add(Company company)
        {
            this.companyList.Add(company);
        }

        // public Company GetCompanyById(int id)
        // {

            
        // }
        
       
    }
}