using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary
{
    /// <summary>
    /// Esta clase se encarga de crear y modificar objetos Company
    /// </summary>
    public class CompanyServiceProvider
    {
        /// <summary>
        /// Crea un objeto Company
        /// </summary>
        /// <param name="nombre"></param>
        /// <param name="ubi"></param>
        /// <param name="headings"></param>
        /// <param name="materials"></param>
        public static Company CreateCompany(string nombre, Location ubi, string headings, string materials)
        {
            Company nuevaCompany = new Company(nombre, ubi, headings, materials);
            CompanyRegisterServiceProvider.AddCompanyToCompanyRegister(nuevaCompany);
            TokenRegisterServiceProvider.AddCompanyToTokenRegister(nuevaCompany);
            return nuevaCompany;
        }
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
        /// <param name="company"></param>
        public static void RemoveUserFromCompany(int Id, Company company)
        {
            company.RemoveUser(Id, company);
        }

    }
}