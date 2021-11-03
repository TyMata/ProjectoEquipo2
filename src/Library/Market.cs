using System;
using System.Collections.Generic;
namespace ClassLibrary
{
    /// <summary>
    /// Esta clase 
    /// </summary>
    public class Market
    {   
        /// <summary>
        /// Lista de ofertas actuales
        /// </summary>
        /// <value></value>
        public List<Offer> ActualOfferList {get;set;}
        /// <summary>
        /// Añade una nueva oferta a la lista de ofertas actuales
        /// </summary>
        /// <param name="NewOffer"></param>
        public void AddOffer(Offer NewOffer)
        
        {
            if(ActualOfferList.Contains(NewOffer)==false)
            {
                this.ActualOfferList.Add(NewOffer);
            }
            
            
        }
        /// <summary>
        /// Retira la oferta de la lista de ofertas actuales
        /// </summary>
        /// <param name="Id"></param>
        public void RemoveOffer(int Id)
        {
            foreach (Offer x in this.ActualOfferList)
            {
                if(x.Id==Id)
                {
                    this.ActualOfferList.Remove(x);
                }
            }
        
        }
        /// <summary>
        /// Devuelve una lista de ofertas que cumplan con un parametro de busqueda
        /// </summary>
        /// <returns></returns>
        public List<Offer> SearchOffer(/*param*/)
        {
            List<Offer> ValidOffer = new List<Offer>();
            foreach (Offer x in ActualOfferList)
            {
                if (/*x.algo == param*/ true)
                {
                    ValidOffer.Add(x);
                }
                
            }
            return ValidOffer;
            
        }
    }
}
