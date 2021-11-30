using System;
using System.Collections.Generic;
using Ucu.Poo.Locations.Client;

namespace ClassLibrary
{
    /// <summary>
    /// Esta clase.
    /// </summary>
    public class Entrepreneur
    {
        private string name;
        /// <summary>
        /// Nombre del emprendedor.
        /// </summary>
        /// <value></value>
        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                if (!String.IsNullOrEmpty(value))
                {
                    this.name = value;
                }
            }
        }

        private string contactPhone;

        public string ContactPhone
        {
            get
            {
                return this.contactPhone;
            }
            set
            {
                if (!String.IsNullOrEmpty(value))
                {
                    this.contactPhone = value;
                }
            }
        }

        private string heading;
        /// <summary>
        /// Rubro del emprendedor
        /// </summary>
        /// <value></value>
        public string Heading
        {
            get
            {
                return this.heading;
            }
            set
            {
                if (!String.IsNullOrEmpty(value))
                {
                    this.heading = value;
                }
            }
        }
     
        private  string habilitation;
        /// <summary>
        /// Habilitaciones del emprendedor.
        /// </summary>
        /// <value></value>
        public string Habilitation
        {
            get
            {
                return this.habilitation;
            }
            set
            {
                if (!String.IsNullOrEmpty(value))
                {
                    this.habilitation = value;
                }
            }
        }

        private LocationAdapter location;
        /// <summary>
        /// Ubicacion del emprendedor
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
            }
        }

        private List<Offer> boughtList  = new List<Offer>();
        /// <summary>
        /// Lista de compras pasadas del emprendedor.
        /// </summary>
        /// <value></value>
        public List<Offer> BoughtList
        {
            get
            {
                return this.boughtList;
            }
            set
            {
                if (value != null)
                {
                    this.boughtList = value ;
                }
            }
        }

        /// <summary>
        /// Constructor de objetos Entrepreneur
        /// </summary>
        /// <param name="name"></param>
        /// <param name="phone"></param>
        /// <param name="location"></param>
        /// <param name="heading"></param>
        /// <param name="habilitation"></param>
        public Entrepreneur(string name, string phone, LocationAdapter location, string heading, string habilitation)
        {
            this.Name = name;
            this.ContactPhone = phone;
            this.Location = location;
            this.Heading = heading;
            this.Habilitation = habilitation;
        }

        /// <summary>
        /// Agrega una oferta comprada a la lista de oferta compradas
        /// </summary>
        /// <param name="offer"></param>
        public void AddBoughtOffer(Offer offer)
        {
            this.BoughtList.Add(offer);             //TODO FALTA GREGAR AL HANDLER AL MOMENTO DE HACER LA COMPRA
        }
    }
}
        

       
       
