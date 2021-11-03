using System;
using System.Collections.Generic;

namespace ClassLibrary
{
    /// <summary>
    /// Esta clase es la encargada de los registros de usuarios y empresas
    /// </summary>
    public class Register
    {
        /// <summary>
        /// Devuelve el registro de usuarios UserRegister
        /// </summary>
        /// <value></value>        
        public UserRegister Users {get; private set;}
        /// <summary>
        /// Devuelve la lista de empresas registradas
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
        /// Verifica si una empresa ya esta registrada
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Remueve un usuario de la lista de usuarios registrados
        /// </summary>
        /// <param name="id"></param>
        public void RemoveUser(int id)
        {
            this.Users.RemoveUser(id);
        }
        /// <summary>
        /// Verifica si un usuario esta registrado
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Verifica si el usuario es Admin
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Verifica si el usuario es emprendedor
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Verifica si el usuario es un usuario empresa
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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