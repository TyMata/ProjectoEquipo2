using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary
{
    /// <summary>
    /// Esta clase  representa un registro de tokens
    /// </summary>
    public class UserRegisterServiceProvider
    {
       /// <summary>
        /// Añade un usuario a la lista de usuarios registrados
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="role"></param>
        public void AddUser(int Id, IRole role)
        {
            UserRegister.DataUsers.Add(new User(Id, role));
        }
        /// <summary>
        /// Remueve un usuario de la lista de usuarios registrados
        /// </summary>
        /// <param name="Id"></param>
        public void RemoveUser(int Id)
        {
            if (UserRegister.DataUsers != null)
            {
                foreach (User x in UserRegister.DataUsers)
                {
                   if (x.Id.Equals(Id)) 
                   {
                        UserRegister.DataUsers.Remove(x);
                   }
                }
            }
        }
        /// <summary>
        /// Busca una oferta por el nombre y devuelve el objeto Company correspondiente a la empresa    
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static Company SearchCompany(string name)
        {
            if (CompanyRegister.CompanyList != null)
            {
                foreach (Company x in CompanyRegister.CompanyList)
                {
                   if (x.Name.Equals(name)) 
                   {
                       return x;
                   }
                }
            }
            return null; //CAMBIAR POR EXCEPCION
        }
    }
}