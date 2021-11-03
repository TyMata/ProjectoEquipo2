using System;
using Ucu.Poo.Locations.Client;

namespace ClassLibrary
{
    /// <summary>
    /// Esta clase 
    /// </summary>
    public class Offer
    {
        private int id;
        /// <summary>
        /// Id de la Oferta
        /// </summary>
        /// <value></value>
        public int Id{get;set;}
        private Material material;
        /// <summary>
        /// Material que se vende en la oferta
        /// </summary>
        /// <value></value>
        public Material Material{get;set;}
        private string habilitation;
        /// <summary>
        /// Habilitaciones necesarias para poder manejar el producto en venta
        /// </summary>
        /// <value></value>
        public string Habilitation{get;set;}
        private string location;
        /// <summary>
        /// Ubicacion en donde se encuentran el producto a vender
        /// </summary>
        /// <value></value>
        public Location Location{get;set;}
        private int quantityMaterial;
        /// <summary>
        /// Cantidad de material a vender
        /// </summary>
        /// <value></value>
        public int QuantityMaterial{get;set;}
        private Company company1;    
        /// <summary>
        /// Precio total del producto
        /// </summary>
        /// <value></value>
        private double totalPrice{get;set;}
        /// <summary>
        /// Empresa que vende el producto
        /// </summary>
        /// <value></value>
        public Company Company{get;set;}
        private string keywords;
        /// <summary>
        /// Palabras claves asignadas
        /// </summary>
        /// <value></value>
        public string Keywords{get;set;}
        private bool availability;
        /// <summary>
        /// Disponibilidad de la oferta
        /// </summary>
        /// <value></value>
        public bool Availability{get;set;}
        private DateTime publicationDate;
        /// <summary>
        /// Fecha de publicacion de la oferta
        /// </summary>
        /// <value></value>
        public DateTime PublicationDate{get;set;}
        private int term;
        /// <summary>
        /// Plazo de la oferta
        /// </summary>
        /// <value></value>
        public int Term{get;set;}

        /// <summary>
        /// Constructor de Offer
        /// </summary>
        /// <param name="id"></param>
        /// <param name="material"></param>
        /// <param name="habilitation"></param>
        /// <param name="location"></param>
        /// <param name="quantityMaterial"></param>
        /// <param name="totalPrice"></param>
        /// <param name="company"></param>
        /// <param name="keywords"></param>
        /// <param name="availability"></param>
        /// <param name="dateTime"></param>
        /// <param name="term"></param>
        public Offer(int id,Material material,string habilitation,Location location,int quantityMaterial, double totalPrice,Company company,string keywords,bool availability,DateTime dateTime,int term)
    {
        this.Id = id;
        this.Material=material;
        this.Habilitation=habilitation;
        this.Location=location;
        this.QuantityMaterial=quantityMaterial;
        this.Company=company;
        this.Keywords=keywords;
        this.Availability=availability;
        this.PublicationDate=dateTime;
        this.Term=term;
        this.totalPrice = totalPrice;
    }
  }
}
