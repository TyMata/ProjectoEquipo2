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
        public List<User> UserList{get;set;}
        /// <summary>
        /// Lista de companias registradas
        /// </summary>
        /// <value></value>
        public List<Company> CompanyList{get;set;}
        /// <summary>
        /// Añade un usuario a la lista de usuarios registrados
        /// </summary>
        /// <param name="user"></param>
        public void AddUser(User user)
        {
            if(UserList.Contains(user)==false)
            {
                this.UserList.Add(user);
            }

        }
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
        /// Remueve un usuario de la lista de usuarios registrados
        /// </summary>
        /// <param name="Id"></param>
        public void RemoveUser(int Id)
        {
            foreach (User x in this.UserList)
            {
                if(x.Id==Id)
                {
                    this.UserList.Remove(x);
                }
            }
        }
       
        /// <summary>
        /// Remueve una empresa de la lista de empresas registradas
        /// </summary>
        /// <param name="Id"></param>
        public void RemoveCompany(int Id)
        {
            foreach (User x in this.UserList)
            {
                if(x.Id==Id)
                {
                    this.UserList.Remove(x);
                }
            }
        }
    }
}