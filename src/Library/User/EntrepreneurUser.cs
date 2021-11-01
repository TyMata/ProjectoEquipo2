using System;
using System.Collections.Generic;



namespace ClassLibrary
{
    /// <summary>
    /// Esta clase 
    /// </summary>
    public class EntrepeneurUser
    {
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
    }
}
        

       
       
