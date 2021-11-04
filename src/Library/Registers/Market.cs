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
        public static List<Offer> ActualOfferList {get;set;}
        /// <summary>
        /// Lista de ofertas suspendidas
        /// </summary>
        /// <value></value>
        public static List<Offer> SuspendedOfferList {get;set;}
    }
}
