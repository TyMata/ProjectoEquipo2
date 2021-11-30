using System;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace ClassLibrary
{
    /// <summary>
    /// Esta clase representa una empresa.
    /// </summary>
    public class Company : IJsonConvertible
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
                this.id = value;
            }
        }

        public string name;

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

        private List<LocationAdapter> locations = new List<LocationAdapter>();

        /// <summary>
        /// Ubicacion/es de la empresa.
        /// </summary>
        /// <value></value>
        [JsonInclude]
        public List<LocationAdapter> Locations
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

        private List<Users> companyUsers = new List<Users>();

        /// <summary>
        /// Lista de usuarios pertenecientes a la empresa.
        /// </summary>
        /// <value></value>
        [JsonInclude]
        public List<Users> CompanyUsers 
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
                    throw new NullReferenceException();
                }
            }
        }
        
        private string invitationToken;
        
        /// <summary>
        /// Token para que un ususuario empresa pueda registrarse.
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
        /// Rubro al que pertenece la empresa.
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
                    throw new NullReferenceException();
                }
            }
        }

        private List<Offer> offerRegister = new List<Offer>();
        /// <summary>
        /// Ofertas realizadas por la empresa.
        /// </summary>
        /// <value></value>
        [JsonInclude]
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
                    throw new NullReferenceException();
                }
            }
        }

        private Dictionary<Offer, Users> soldOffers = new Dictionary<Offer, Users>();
        
        [JsonInclude]
        public Dictionary<Offer, Users> SoldOffers
        {
            get
            {
                return this.soldOffers;
            }
            private set
            {
                if (value != null)
                {
                    this.soldOffers = value;
                }
                else
                {
                    throw new NullReferenceException();
                }
            }
        }

        
        private List<Material> producedMaterials = new List<Material>();

        /// <summary>
        /// Materiales producidos por la empresa.
        /// </summary>
        /// <value></value>
        [JsonInclude]
        public List<Material> ProducedMaterials
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
                    throw new NullReferenceException();
                }
            }
        }

        public string email;

        public string Email
        {
            get
            {
                return this.email;
            }
            private set
            {
                if (value != null)
                {
                    this.email = value;
                }
                else
                {
                   throw new NullReferenceException(); 
                }
            }
        }

        public string phoneNumber;

        public string PhoneNumber
        {
            get
            {
                return this.phoneNumber;
            }
            private set
            {
                if (value != null)
                {
                    this.phoneNumber = value;
                }
                else
                {
                   throw new NullReferenceException(); 
                }
            }
        }

        /// <summary>
        /// JsonConstructor de objetos Company.
        /// </summary>
        [JsonConstructor]
        public Company()
        {

        }

        /// <summary>
        /// Constructor de objetos Company.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="location"></param>
        /// <param name="headings"></param>
        public Company(string name, LocationAdapter location, string headings, string email, string phoneNumber)
        {
            this.Name = name;
            this.Locations.Add(location);
            this.Id = CompanyRegister.Instance.CompanyList.Count + 1;//TODO hacer lista de keywords this.name this.material (en offer)
            this.Headings = headings;
            this.Email = email;
            this.PhoneNumber = phoneNumber;
            this.InvitationToken = TokenRegister.Instance.GenerateToken(); 
        }

        public void AddMaterial(string name, string type, string classification)
        {
            if (this.producedMaterials.Count != 0 && this.producedMaterials.Exists(mat => mat.Name == name && mat.Type == type && mat.Classification == classification))
            {
                throw new Exception();
            }
            Material material = new Material(name, type, classification); 
            this.producedMaterials.Add(material);
        }
        
        /// <summary>
        /// Añade un usuario a la lista de usuarios pertenecientes a la empresa, CREATOR, crea user ya que tiene  una lista de users.
        /// </summary>
        /// <param name="id"></param>
        public void AddUser(int id)
        {
            if (this.companyUsers.Exists(user => user.Id == id))
            {
                throw new Exception();
            }

            IRole rol = new CompanyRole(this);
            Users user = new Users(id, rol);
            UserRegister.Instance.Add(user);
            this.CompanyUsers.Add(user);
        }
        
        /// <summary>
        /// Remueve  un usuario de la lista de usuarios pertenecientes a la empresa.
        /// </summary>
        /// <param name="id"></param>
        public void RemoveUser(int id)
        {
            if (!this.CompanyUsers.Exists(x => x.Id == id))
            {
                throw new Exception();
            } 
            Users x = this.CompanyUsers.Find(offer => offer.Id == id);
            this.CompanyUsers.Remove(x);
        } 

        /// <summary>
        /// Añade una oferta al registro de la empresa
        /// /// </summary>
        /// <param name="offer"></param>
        public void AddOffer(Offer offer)
        {
            if(this.OfferRegister.Exists(x => x == offer))
            {
                throw new Exception("Esta oferta ya esta creada.");
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
        /// TODO add token
        
        /// <summary>
        /// Convierte un objeto a texto en formato Json. El objeto puede ser reconstruido a partir del texto en formato
        /// Json utilizando JsonSerializer.Deserialize.
        /// </summary>
        /// <returns>El objeto convertido a texto en formato Json.</returns>
        public string ConvertToJson(JsonSerializerOptions options) 
        {
            JsonSerializerOptions option = new()
            {
                ReferenceHandler = MyReferenceHandler.Instance,
                WriteIndented = true
            };
            return JsonSerializer.Serialize(this, option);
        }
        
        /// <summary>
        /// Carga los datos del archivo en formato .json y reconstruye los objetos a partir de este
        /// </summary>
        /// <param name="json"></param>
        public void LoadFromJson(string json)
        {
            Company company = JsonSerializer.Deserialize<Company>(json);
            this.Name = company.Name;
            this.OfferRegister = company.OfferRegister;
            this.ProducedMaterials = company.ProducedMaterials;
            this.Locations = company.Locations;
            this.CompanyUsers = company.CompanyUsers;
            this.Headings = company.Headings;
            this.Id = company.Id;
            this.InvitationToken = company.InvitationToken;
            this.SoldOffers = company.SoldOffers;
            
        }

        /// <summary>
        /// Retorna un material segun el nombre de estes
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Material GetMaterial(string name)
        {
            Material material = this.ProducedMaterials.Find(material => material.Name.ToLower().Trim() == name.ToLower().Trim());
            if(material != null)
            {
            return material;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Devuelva una ubicacion dentro de la lista de ubicaciones de la empresa a partir de la dirección.
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public LocationAdapter GetLocation(string address)
        {
            LocationAdapter location = this.Locations.Find(location => location.Address.ToLower().Trim() == address.ToLower().Trim());
            if(location != null)
            {
                return location;
            }
            else
            {
                return null;
            }
        }

        public void OfferSold(Offer offer, Users user)
        {
            this.SoldOffers.Add(offer, user); //TODO HAcer esto bien, precondiciones preuntando si ya esta ingresada en el diccionario y falta agregar a la lista en el handler al momento de hacerse la compra
        }
    }
}
 