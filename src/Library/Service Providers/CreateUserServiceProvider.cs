using System;
using System.Collections.Generic;
using System.Text;
using Ucu.Poo.Locations.Client;

namespace ClassLibrary
{
    /// <summary>
    /// Esta clase se encarga de crear usuarios de empresas y usuarios emprendedores
    /// </summary>
    public class CreateUserServiceProvider
    {
        /// <summary>
        /// Crea un usuario empresa
        /// </summary>
        /// <param name="input"></param>
        /// <param name="company"></param>
        public static void CreateCompanyUser(IMessage input,Company company)
        {
            IRole rol = new CompanyRole(company);
            User usuario = new User(input.Id, rol);
            CompanyServiceProvider.AddUserToCompany(usuario,  company);
        }
        /// <summary>
        /// Crea un usuario emprendedor
        /// </summary>
        /// <param name="input"></param>
        /// <param name="name"></param>
        /// <param name="location"></param>
        /// <param name="headings"></param>
        /// <param name="habilitations"></param>
        public static void CreateEntrepreneurUser(IMessage input,string name , Location location, string headings, string habilitations)
        {
            IRole rol = new EntrepreneurRole(name , location, headings, habilitations);
            User usuario = new User(input.Id, rol);
        }
    }
}