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

        private Location location;
        /// <summary>
        /// Ubicacion del emprendedor
        /// </summary>
        /// <value></value>
        public Location Location
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

        private List<Offer> boughtList;
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
                    this.boughtList = value;
                }
            }
        }
        
        /// <summary>
        /// Agrega una oferta comprada a la lista de oferta compradas
        /// </summary>
        /// <param name="offer"></param>
        public void AddBoughtOffer(Offer offer)
        {
            this.BoughtList.Add(offer);
        }

        /// <summary>
        /// Constructor de objetos Entrepreneur
        /// </summary>
        /// <param name="name"></param>
        /// <param name="location"></param>
        /// <param name="heading"></param>
        /// <param name="habilitation"></param>
        public Entrepreneur(string name, Location location, string heading, string habilitation)
        {
            this.Name = name;
            this.Location = location;
            this.Heading = heading;
            this.Habilitation = habilitation;
        }
    }
}
        

       
       
