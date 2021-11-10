using System;
using System.Collections.Generic;

namespace ClassLibrary
{   
    /// <summary>
    /// Esta clase representa un registro de empresas
    /// </summary>
    public class CompanyRegister
    {   
        private List<Company> companyList= new List<Company>();
        /// <summary>
        /// Lista de empresas registrados
        /// </summary>
        /// <value></value>
        public static List<Company> CompanyList { get; private set; }
        
       
    }
}