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
        public bool IsRegisteredCompany(int id)
        {
            foreach(Company x in this.CompanyList)
            {
                if(x.Id == id)
                {
                  return true;  
                }
            }
            return false;
        }
       
        /// <summary>
        /// Remueve una empresa de la lista de empresas registradas
        /// </summary>
        /// <param name="id"></param>
        public void RemoveCompany(int id)
        {
            foreach (Company x in this.CompanyList)
            {
                if(x.Id==id)
                {
                    this.CompanyList.Remove(x);
                }
            }
        }
        public void RemoveUser(int id)
        {
            this.Users.RemoveUser(id);
        }
        public bool IsRegistered(int id)
        {

            foreach(User x in this.Users.DataUsers)
            {
                if(x.Id==id)
                {
                  return true;  
                }
            }
            return false;

        }
        public bool IsAdmin(int id)
        {
            foreach(User x in this.Users.DataUsers)
            {
                if(x.Id==id && (x.Role.TipoRol().Equals("admin")))
                {
                    return true;
                }
            }
            return false;
        }
        public bool IsEntrepreneur(int id)
        {
            foreach(User x in this.Users.DataUsers)
            {
                if(x.Id==id && (x.Role.TipoRol().Equals("entrepreneur")))
                {
                    return true;
                }
            }
            return false;
        }
        public bool IsCompany(int id)
        {
            foreach(User x in this.Users.DataUsers)
            {
                if(x.Id==id && (x.Role.TipoRol().Equals("company")))
                {
                    return true;
                }
            }
            return false;
        }
    }
}