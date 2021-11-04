using System;
using System.Collections.Generic;



namespace ClassLibrary
{
    /// <summary>
    /// Esta clase 
    /// </summary>
    public class Entrepreneur
    {
        /// <summary>
        /// Nombre del emprendedor
        /// </summary>
        /// <value></value>
        public string Name { get; set; }

        /// <summary>
        /// Rubro del emprendedor
        /// </summary>
        /// <value></value>
        public string Heading{get;set;}
        /// <summary>
        /// Habilitaciones del emprendedor
        /// </summary>
        /// <value></value>
        public string Habilitation{get;set;}
        /// <summary>
        /// Ubicacion del emprendedor
        /// </summary>
        /// <value></value>
        public Location Location{get;private set;}
        /// <summary>
        /// Lista de compras pasadas del emprendedor
        /// </summary>
        /// <value></value>
        public List<Offer> BoughtList{get;set;}

        Location location { get; set; }
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
        

       
       
