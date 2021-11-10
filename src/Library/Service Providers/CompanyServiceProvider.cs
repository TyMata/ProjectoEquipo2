using System;
using System.Collections.Generic;
using System.Text;
using Ucu.Poo.Locations.Client;

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
    }
}