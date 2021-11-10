using System;
using System.Collections.Generic;
using System.Linq;
using Ucu.Poo.Locations.Client;

namespace ClassLibrary
{
    /// <summary>
    /// Esta clase representa una empresa
    /// </summary>
    public class Company
    {
        
        private static int id;
        /// <summary>
        /// Id de la empresa
        /// </summary>
        /// <value></value> 
        public int Id
        {
            get
            {
                return id; 
            }
            private set
            { 
                this.Id = id;
            }
        }
        private string name;
        /// <summary>
        /// Nombre de la empresa
        /// </summary>
        /// <value></value>
        public string Name
        {
            get
            {
                return this.name;
            }
            private set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    this.name = value;
                }
            }
        }
        private Location locations;
        /// <summary>
        /// Ubicacion/es de la empresa
        /// </summary>
        /// <value></value>
        public Location Locations
        {
            get
            {
                return this.locations;
            }
            private set
            {
                if (value != null)
                {
                    this.locations = value;
                }
            }
        }
        private List<User> companyUsers = new List<User>();
        /// <summary>
        /// Lista de usuarios pertenecientes a la empresa
        /// </summary>
        /// <value></value>
        public List<User> CompanyUsers 
        {
            get
            {
                return this.companyUsers;
            }
            private set
            {
                if (value != null)
                {
                    this.companyUsers = value;
                }
                else
                {
                    //EXCEPCION OBJETO NULO
                }
            }
        }
        private string headings;
        /// <summary>
        /// Rubro al que pertenece la empresa
        /// </summary>
        /// <value></value>
        public string Headings {
            get
            {
                return this.headings;
            }
            private set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    this.headings = value;
                }
                else
                {
                    //EXCEPCION DE NOMBRE VACIO O NULO
                }
            }
        }
        private List<Offer> offerRegister = new List<Offer>();
        /// <summary>
        /// Ofertas realizadas por la empresa
        /// </summary>
        /// <value></value>
        public List<Offer> OfferRegister 
        {
            get
            {
                return this.offerRegister;
            }
            private set
            {
                if (value != null)
                {
                    this.offerRegister = value;
                }
                else
                {
                    //EXCEPCION OBJETO NULO
                }
            }
        }
        
        private List<string> producedMaterials = new List<string>();
        /// <summary>
        /// Materiales producidos por la empresa
        /// </summary>
        /// <value></value>
        public List<string> ProducedMaterials
        {
            get
            {
                return this.producedMaterials;
            }
            private set
            {
                if (value != null)
                {
                    this.producedMaterials = value;
                }
                else
                {
                    //EXCEPCION OBJETO NULO
                }
            }
        }
        static Company()
        {
            id = 0;
        }
        /// <summary>
        /// Constructor de Company sin parámetros que aumenta Id cada vez que se le llama
        /// </summary>
        public Company()
        {
            id++;
        }
        /// <summary>
        /// Constructor de objetos Company
        /// </summary>
        /// <param name="name"></param>
        /// <param name="ubi"></param>
        /// <param name="headings"></param>
        /// <param name="materials"></param>
        public Company(string name, Location ubi, string headings, string materials)
        {
            this.name = name;
            this.Locations = ubi;
            id = Id;
            this.Headings = headings;
            this.ProducedMaterials.Add(materials);
        }
        /// <summary>
        /// Añade un usuario a la lista de usuarios pertenecientes a la empresa, CREATOR, crea user ya que tiene  una lista de users
        /// </summary>
        /// <param name="id"></param>
        public  void AddUser(int id)
        {
            if (this.companyUsers.Exists(user => user.Id == id))
            {
                throw new Exception();
            }

            IRole rol = new CompanyRole(this);
            User user = new User(id, rol);
            Singleton<UserRegister>.Instance.Add(user);
            this.CompanyUsers.Add(user);
        }
        /// <summary>
        /// Remueve  un usuario de la lista de usuarios pertenecientes a la empresa
        /// </summary>
        /// <param name="id"></param>
        public void RemoveUser(int id)
        {
            bool exists = false;
            User aEliminar = null;
            foreach (User x in this.CompanyUsers)
            {
                if (x.Id == id)
                {
                    exists = true;
                    aEliminar = x;
                }
            }
            if (exists)
            {
                this.CompanyUsers.Remove(aEliminar);
            }
            //else     AGREGAR EXCEPCION
        }
    }
}
