using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary
{
    /// <summary>
    /// Esta clase  representa un registro de tokens
    /// </summary>
    public class CompanyServiceProvider
    {
        /// <summary>
        /// Añade un usuario a la lista de usuarios de la empresa de un objeto Company
        /// </summary>
        /// <param name="user"></param>
        /// <param name="company"></param>
        public static void AddUserToCompany(User user , Company company)
        {
            company.AddUser(user);
        }
        /// <summary>
        /// Remueve un usuario de la lista de usuarios de la empresa registrados
        /// </summary>
        /// <param name="Id"></param>
        public void RemoveUserFromCompany(int Id, Company company)
        {
            company.RemoveUser(Id, company);
        }
    }
}