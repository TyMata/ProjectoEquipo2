using System;
using System.Collections.Generic;
using System.Text;
using Ucu.Poo.Locations.Client;

namespace ClassLibrary
{
    /// <summary>
    /// Esta clase se encarga de crear y modificar objetos Offer
    /// </summary>
    public class OfferServiceProvider
    {
        /// <summary>
        /// Crea y devuelve una nueva oferta
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
        /// <returns></returns>
        public static Offer CreateOffer(int id,string material,string habilitation, Location location,int quantityMaterial, double totalPrice, Company company,string keywords,bool availability)
        {
            string[] keyWords= keywords.Split(",");
            Offer nuevaOferta = new Offer(id, material, habilitation, location, quantityMaterial, totalPrice, company, keywords, availability, DateTime.Now);
            return nuevaOferta;
        }
    }
}