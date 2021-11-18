using System;
using System.Collections.Generic;
using System.Linq;
using Ucu.Poo.Locations.Client;

namespace ClassLibrary
{
    /// <summary>
    /// Esta clase representa el mercado con sus ofertas
    /// </summary>
    public class Market
    {   
        private List<Offer> actualOfferList = new List<Offer>();
        /// <summary>
        /// Lista de ofertas actuales
        /// </summary>
        /// <value></value>
        public List<Offer> ActualOfferList 
        {
            get
            {
                return this.actualOfferList;
            }
        }

        private List<Offer> suspendedOfferList = new List<Offer>();
        /// <summary>
        /// Lista de ofertas suspendidas
        /// </summary>
        /// <value></value>
        public List<Offer> SuspendedOfferList 
        {
            get
            {
                return this.suspendedOfferList;
            }
        }

        /// <summary>
        /// Crea y devuelve una nueva oferta. Creamos las ofertas aca por Creator
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
        public Offer CreateOffer(int id,string material,string habilitation, Location location,int quantityMaterial, double totalPrice, Company company,string keywords,bool availability)
        {
            string[] keyWords= keywords.Split(",");
            Offer nuevaOferta = new Offer(id, material, habilitation, location, quantityMaterial, totalPrice, company, availability, DateTime.Now);
            company.AddOffer(nuevaOferta);
            this.PublishOffer(nuevaOferta);
            return nuevaOferta;
        }

        /// <summary>
        /// Por la ley de demeter se crea ContainsActive
        /// </summary>
        /// <param name="offer"></param>
        /// <returns></returns>
        public bool ContainsActive(Offer offer)
        {
            if(this.actualOfferList.Contains(offer))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Por la ley de demeter se crea ContainsSuspended
        /// </summary>
        /// <param name="offer"></param>
        /// <returns></returns>
        public bool ContainsSuspended(Offer offer)
        {
            if(this.SuspendedOfferList.Contains(offer))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Añade una nueva oferta a la lista de ofertas actuales.
        /// </summary>
        /// <param name="newOffer"></param>
        public void PublishOffer(Offer newOffer)
        {
            if(this.ActualOfferList.Contains(newOffer))
            {
                throw new Exception(); //CAMBIAR EXCEPTION
            }
            this.ActualOfferList.Add(newOffer);
        }

        /// <summary>
        /// Retira la oferta de la lista de ofertas actuales.
        /// </summary>
        /// <param name="id"></param>
        public void RemoveOffer(int id)
        {
            if (!this.ActualOfferList.Exists(offer => offer.Id == id))
            {
                throw new NullReferenceException($"El Id de la oferta es incorrecto."); //CAMBIAR EXCEPTION
            }
            Offer x = this.ActualOfferList.Find(offer => offer.Id == id);
            this.ActualOfferList.Remove(x);
        }

        /// <summary>
        /// Devuelve una lista de ofertas que cumplan con un parametro de busqueda , ARREGLAR KEYWORDS ANTES
        /// </summary>
        /// <returns></returns>
        public List<Offer> SearchOffers(string keyword)
        {
            if (!this.ActualOfferList.Exists(offer => offer.Keywords.Contains(keyword)))
            {
                throw new NullReferenceException($"El Id de la oferta es incorrecto."); //CAMBIAR EXCEPTION
            }
            List<Offer> x = this.ActualOfferList.FindAll(offer => offer.Keywords.Contains(keyword));
            return x;
        }      

        /// <summary>
        /// Suspende una oferta actual
        /// </summary>
        /// <param name="id"></param>
        public void SuspendOffer(int id)
        {
            if (!this.ActualOfferList.Exists(offer => offer.Id == id))
            {
                throw new NullReferenceException($"El Id de la oferta es incorrecto."); //CAMBIAR EXCEPTION
            }
            Offer x = this.ActualOfferList.Find(offer => offer.Id == id);
            this.SuspendedOfferList.Add(x);
            this.ActualOfferList.Remove(x);
        }

        /// <summary>
        /// A una oferta suspendida la vuelve a activar.
        /// </summary>
        /// <param name="id"></param>
        public void ResumeOffer(int id)
        {
            if (!this.SuspendedOfferList.Exists(offer => offer.Id == id))
            {
                throw new NullReferenceException($"El Id de la oferta es incorrecto."); //CAMBIAR EXCEPTION
            }
            Offer x = this.SuspendedOfferList.Find(offer => offer.Id == id);
            this.ActualOfferList.Add(x);
            this.SuspendedOfferList.Remove(x);
        }
    }
}
