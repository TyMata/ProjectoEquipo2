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
            this.CompanyList.Add(company);
        }
        
        /// <summary>
        /// Método para remover empresas de la lista de empresas
        /// </summary>
        /// <param name="company"></param>
        public void Remove(Company company)
        {
            if(!this.CompanyList.Contains(company))
            {
                throw new Exception(); //CAMBIAR EXCEPTION
            }
            this.CompanyList.Remove(company);
        }

        public Company GetCompanyByUserId(int id)
        {
            
            User x = Singleton<UserRegister>.Instance.GetUserById(id);
            return (x.Role as CompanyRole).Company;
            // while (result == null && index < this.CompanyList.Count)
            // {
            //     if (this.CompanyList[index].CompanyUsers == id)
            //     {
            //         result = this.DataUsers[index];
            //     }
            // }

            // return result;
            
        }
        
       
    }
}