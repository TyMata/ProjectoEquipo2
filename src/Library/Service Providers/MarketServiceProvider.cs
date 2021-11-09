using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary
{
    /// <summary>
    /// Esta clase se encarga de modificar los objetos Market
    /// </summary>
    public class MarketServiceProvider
    {
        /// <summary>
        /// Añade una nueva oferta a la lista de ofertas actuales
        /// </summary>
        /// <param name="newOffer"></param>
        public static void PublishOffer(Offer newOffer)
        {
            if(!Market.ActualOfferList.Contains(newOffer))
            {
                Market.ActualOfferList.Add(newOffer);
            }
            //else EXCEPCIÓN 
        }
        /// <summary>
        /// Retira la oferta de la lista de ofertas actuales
        /// </summary>
        /// <param name="Id"></param>
        public static void RemoveOffer(int Id)
        {
            foreach (Offer x in Market.ActualOfferList)
            {
                if(x.Id==Id)
                {
                    Market.ActualOfferList.Remove(x);
                }
            }
        
        }
        /// <summary>
        /// Devuelve una lista de ofertas que cumplan con un parametro de busqueda
        /// </summary>
        /// <returns></returns>
        public static List<Offer> SearchOffers(/*param*/)
        {
            List<Offer> ValidOffer = new List<Offer>();
            foreach (Offer x in Market.ActualOfferList)
            {
                if (/*x.algo == param*/ true)
                {
                    ValidOffer.Add(x);
                }
                
            }
            return ValidOffer;
        }
        /// <summary>
        /// Devuelve una lista con las ofertas actuales que hay en Market
        /// </summary>
        /// <returns></returns>
        public static List<Offer> GetActualOffers()
        {
            return Market.ActualOfferList;
        }

        /// <summary>
        /// Suspende una oferta actual
        /// </summary>
        /// <param name="Id"></param>
        public static void SuspendOffer(int Id)
        {
            Offer offerToSuspend = null;
            foreach (Offer x in Market.ActualOfferList)
            {
                if(x.Id==Id)
                {
                    offerToSuspend = x;
                }
            }
            if(offerToSuspend != null)
            {
                Market.ActualOfferList.Remove(offerToSuspend);
                Market.SuspendedOfferList.Add(offerToSuspend);
            }
        }
        /// <summary>
        /// A una oferta suspendida la vuelve a activar
        /// </summary>
        /// <param name="Id"></param>
        public static void ResumeOffer(int Id)
        {
            Offer offerToResume = null;
            foreach (Offer x in Market.SuspendedOfferList)
            {
                if(x.Id==Id)
                {
                    offerToResume = x;
                }
            }
            if(offerToResume != null)
            {
                Market.ActualOfferList.Add(offerToResume);
                Market.SuspendedOfferList.Remove(offerToResume);
            }
        }
    }
}