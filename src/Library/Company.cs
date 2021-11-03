using System;
using System.Collections.Generic;

namespace ClassLibrary
{
    /// <summary>
    /// Esta clase
    /// </summary>
    public class Company
    {
        private int id;
        /// <summary>
        /// Id de la empresa
        /// </summary>
        /// <value></value>
        public int Id{get; private set;}
        private string name;
        /// <summary>
        /// Nombre de la empresa
        /// </summary>
        /// <value></value>
        public string Name{get;private set;}
        private List<Location> locations =new List<Location>();
        /// <summary>
        /// Ubicacion/es de la empresa
        /// </summary>
        /// <value></value>
        public List<Location> Locations{get; private set;}
        private List<User> companyUsers = new List<User>();
        /// <summary>
        /// Lista de usuarios pertenecientes a la empresa
        /// </summary>
        /// <value></value>
        public List<User> CompanyUsers {get;private set;}
        private string headings;
        /// <summary>
        /// Rubro al que pertenece la empresa
        /// </summary>
        /// <value></value>
        public string Headings {get; private set;}
        private List<Offer> offerRegister = new List<Offer>();
        /// <summary>
        /// Ofertas realizadas por la empresa
        /// </summary>
        /// <value></value>
        public List<Offer> OfferRegister {get; private set;}
        private List<Material> producedMaterials = new List<Material>();
        /// <summary>
        /// Materiales producidos por la empresa
        /// </summary>
        /// <value></value>
        public List<Material> ProducedMaterials {get; private set;}
        /// <summary>
        /// Constructor de objetos Company
        /// </summary>
        /// <param name="name"></param>
        /// <param name="ubi"></param>
        /// <param name="headings"></param>
        /// <param name="materials"></param>
        public Company(string name, Location ubi, string headings, Material materials)
        {
            this.name = name;
            this.Locations.Add(ubi);
            this.Headings = headings;
            this.ProducedMaterials.Add(materials);
        }
        /// <summary>
        /// Añade un usuario a la lista de usuarios pertenecientes a la empresa
        /// </summary>
        /// <param name="userPar"></param>
        public void AddUser(User userPar)
        {
            if (!this.CompanyUsers.Contains(userPar))
            {
                this.CompanyUsers.Add(userPar);
            }
        }
       }
}