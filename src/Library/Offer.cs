using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Text.Json;
using Ucu.Poo.Locations.Client;

namespace ClassLibrary
{
    /// <summary>
    /// Esta clase representa una oferta.
    /// </summary>
    public class Offer : IJsonConvertible
    {
        private int id;

        /// <summary>
        /// Id de la oferta.
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
        private Material material;

        /// <summary>
        /// Material que se vende en la oferta.
        /// </summary>
        /// <value></value>
        public Material Material
        {
            get
            {
                return this.material;
            }
            private set
            {
                if (value != null)
                {
                    this.material = value;
                }
                else
                {
                    //EXCEPCION OBJETO NULO
                }
            }
        }

        private string habilitation;
        /// <summary>
        /// Habilitaciones necesarias para poder manejar el producto en venta.
        /// </summary>
        /// <value></value>
        public string Habilitation
        {
            get
            {
                return this.habilitation;
            }
            private set
            {
                if (value != null)
                {
                    this.habilitation = value;
                }
                else
                {
                    //EXCEPCION link invalido???????
                }
            }
        }
        private LocationAdapter location;

        /// <summary>
        /// Ubicacion en donde se encuentran el producto a vender.
        /// </summary>
        /// <value></value>
        public LocationAdapter Location
        {
            get
            {
                return this.location;
            }
            private set
            {
                if (value != null)
                {
                    this.location = value;
                }
                else
                {
                    //EXCEPCION OBJETO NULO
                }
            }
        }        
        private int quantityMaterial;

        /// <summary>
        /// Cantidad de material a vender.
        /// </summary>
        /// <value></value>
        public int QuantityMaterial
        {
            get
            {
                return this.quantityMaterial;
            }
            private set
            {
                this.quantityMaterial = value;
            }
        }
        private double totalPrice;

        /// <summary>
        /// Precio total del producto.
        /// </summary>
        /// <value></value>
        public double TotalPrice
        {
            get
            {
                return this.totalPrice;
            }
            private set
            {
                this.totalPrice = value;
            }
        }

        private Company company;  

        /// <summary>
        /// Empresa que vende el producto.
        /// </summary>
        /// <value></value>
        public Company Company
        {
            get
            {
                return this.company;
            }
            private set
            {
                if (value != null)
                {
                    this.company = value;
                }
                else
                {
                    //EXCEPCION OBJETO NULO
                }
            }
        }

        private List<string> keywords = new List<string>();

        
        /// <summary>
        /// Palabras claves asignadas.
        /// </summary>
        /// <value></value>
        [JsonInclude]
        public List<string> Keywords
        {
            get
            {
                return this.keywords;
            }
            private set
            {
                if (value != null)
                {
                    this.keywords = value;
                }
                else
                {
                    //EXCEPCION DE NOMBRE VACIO O NULO
                }
            }
        }

        private bool availability;
        
        /// <summary>
        /// Disponibilidad de la oferta.
        /// </summary>
        /// <value></value>
        public bool Availability
        {
            get
            {
                return this.availability;
            }
            private set
            {
                this.availability = value;
            }
        }

        private DateTime publicationDate;
        
        /// <summary>
        /// Fecha de publicacion.
        /// </summary>
        /// <value></value>
        public DateTime PublicationDate
        {
            get
            {
                return this.publicationDate;
            }
            private set
            {
                if (value != null)
                {
                    this.publicationDate = value;
                }
                else
                {
                    //EXCEPCION DE OBJETO VACIO O NULO
                }
            }
        }

        /// <summary>
        /// JsonConstructor para objetos Offer.
        /// </summary>
        [JsonConstructor]
        public Offer()
        {
            
        }

        /// <summary>
        /// Constructor de Offer.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="material"></param>
        /// <param name="habilitation"></param>
        /// <param name="location"></param>
        /// <param name="quantityMaterial"></param>
        /// <param name="totalPrice"></param>
        /// <param name="company"></param>
        /// <param name="availability"></param>
        /// <param name="publicationDate"></param>
        public Offer( int id, Material material, string habilitation, LocationAdapter location,int quantityMaterial, double totalPrice, Company company,bool availability, DateTime publicationDate)
        {
            this.Id = id;
            this.Material = material;
            this.Habilitation = habilitation;
            this.Location = location;
            this.QuantityMaterial = quantityMaterial;
            this.Company = company;
            this.Availability = availability;
            this.PublicationDate = publicationDate;
            this.TotalPrice = totalPrice;
        }

        /// <summary>
        /// Modifica la cantidad del material.
        /// </summary>
        /// <param name="quantity"></param>
        public void ChangeQuantity(int quantity)
        {
            this.QuantityMaterial = quantity;
        }

        /// <summary>
        /// Modifica el material
        /// </summary>
        /// <param name="material"></param>
        public void ChangeMaterial(Material material)
        {
            this.Material = material;
        }

        /// <summary>
        /// Modifica las habilitaciones
        /// </summary>
        /// <param name="habilitation"></param>
        public void ChangeHabilitation(string habilitation)
        {
            this.Habilitation = habilitation;
        }

        /// <summary>
        /// Modifica el precio
        /// </summary>
        /// <param name="price"></param>
        public void ChangePrice(int price)
        {
            this.TotalPrice = price;
        }

        /// <summary>
        /// Modifica el precio
        /// </summary>
        public void ChangeAvailability()
        {
            if(this.availability) this.availability = false;
            else this.availability = true;

        }

        /// <summary>
        /// Convierte el objeto a texto en formato Json. El objeto puede ser reconstruido a partir del texto en formato
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
        /// Carga la oferta que esta en formato json para reconstruir el objeto
        /// </summary>
        /// <param name="json"></param>
        public void LoadFromJson(string json)
        {
            Offer offer = JsonSerializer.Deserialize<Offer>(json);
            this.Material = offer.Material;
            this.Location = offer.Location;
            this.PublicationDate = offer.PublicationDate;
            this.QuantityMaterial = offer.QuantityMaterial;
            this.TotalPrice = offer.TotalPrice;
        }
    }
}
