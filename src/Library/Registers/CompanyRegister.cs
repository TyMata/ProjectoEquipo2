using System;
using System.Collections.Generic;
using Ucu.Poo.Locations.Client;

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
        public  List<Company> CompanyList 
        {
            get
            {
                return companyList;
            }
        }

        /// <summary>
        /// Por la ley de demeter
        /// </summary>
        /// <param name="company"></param>
        public void Add(Company company)
        {
            this.companyList.Add(company);
        }
        
        /// <summary>
        /// Método para remover empresas de la lista de empresas. Se crea por la ley de Demeter.
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

        /// <summary>
        /// Devuelva una empresa segun el id del user.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Company GetCompanyByUserId(int id)
        {
            User x = Singleton<UserRegister>.Instance.GetUserById(id);
            return (x.Role as CompanyRole).Company;
        } 

        /// <summary>
        /// Por la ley de demeter
        /// </summary>
        /// <param name="company"></param>
        public bool Contains(Company company)
        {
            if(this.CompanyList.Contains(company))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Crea un objeto Company y lo añade a los registros. Se coloco aqui el metodo por el patron Creator.
        /// </summary>
        /// <param name="nombre"></param>
        /// <param name="ubi"></param>
        /// <param name="headings"></param>
        /// <param name="materials"></param>
        public Company CreateCompany(string nombre, Location ubi, string headings, string materials)
        {
            Company nuevaCompany = new Company(nombre, ubi, headings, materials);
            Singleton<CompanyRegister>.Instance.Add(nuevaCompany);
            Singleton<TokenRegister>.Instance.TokenList.Add("nuevo token",nuevaCompany);
            return nuevaCompany;
        }
    }
}