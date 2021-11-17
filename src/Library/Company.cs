using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using Ucu.Poo.Locations.Client;

namespace ClassLibrary
{
    /// <summary>
    /// Esta clase representa una empresa
    /// </summary>
    public class Company
    {
        
        private int id;

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
                this.id = Singleton<CompanyRegister>.Instance.CompanyList.Count + 1;
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
        
        private string invitationToken;
        
        /// <summary>
        /// Token para que un ususario empresa pueda registrarse.
        /// </summary>
        /// <value></value>
        public string InvitationToken
        {
            get
            {
                return this.invitationToken;
            }
            set
            {
                if(value == "-1")
                {
                    throw new Exception();
                }
                this.invitationToken = value;
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
            this.id = 0;
            this.Headings = headings;
            this.ProducedMaterials.Add(materials);
            this.InvitationToken = this.GenerateToken(this);
        }
        
        /// <summary>
        /// Añade un usuario a la lista de usuarios pertenecientes a la empresa, CREATOR, crea user ya que tiene  una lista de users
        /// /// /// </summary>
        /// <param name="id"></param>
        public void AddUser(int id)
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
            if (!this.CompanyUsers.Exists(x => x.Id == id))
            {
                throw new Exception();
            } 
            User x = this.CompanyUsers.Find(offer => offer.Id == id);
            this.CompanyUsers.Remove(x);
        } 

        /// <summary>
        /// Añade una oferta al registro de la empresa
        /// /// </summary>
        /// <param name="offer"></param>
        public void AddOffer(Offer offer)
        {
            if (this.OfferRegister.Exists(x => x == offer))
            {
                throw new Exception();
            }   
            this.OfferRegister.Add(offer);
        }
        
        /// <summary>
        /// Remueve una oferta del registro de ofertas de la empresa
        /// </summary>
        /// <param name="id"></param>
        public void RemoveOffer(int id)
        {
            if (!this.OfferRegister.Exists(x => x.Id == id))
            {
                throw new Exception();
            } 
            Offer x = this.OfferRegister.Find(offer => offer.Id == id);
            this.OfferRegister.Remove(x);
        }
        /// <summary>
        /// Se genera un  token para una nueva empresa y se lo añade al diccionario
        /// </summary>
        /// <param name="company"></param>
        /// <returns></returns>
        public string GenerateToken(Company company)
        {
            Random rnd = new Random();
            StringBuilder token = new StringBuilder();
            for (int i = 0; i < 3; i++)         //Creo un nuevo token
            {
                int num = rnd.Next(10000, 100000);
                token.Append(num.ToString());
                if (i != 2) token.Append("-");
            }
            if (Singleton<TokenRegister>.Instance.Contains(token.ToString()))        //Me fijo si ya existe token y de ser asi le añado el Token y su empresa a el diccionario
            {
                throw new Exception(); //EL TOKEN YA EXISTE
            }
            Singleton<TokenRegister>.Instance.Add(token.ToString(), this);
            return token.ToString();
        }
    }
}
 