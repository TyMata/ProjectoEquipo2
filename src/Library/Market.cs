using System;
using System.Collections.Generic;
namespace ClassLibrary
{
    /// <summary>
    /// Esta clase 
    /// </summary>
    public class Market
    {
        public List<Offer> ActualOfferList {get;set;}
        public void AddOffer(Offer NewOffer)
        
        {
            if(ActualOfferList.Contains(NewOffer)==false)
            {
                this.ActualOfferList.Add(NewOffer);
            }
            
            
        }
        public void RemoveOffer(int Id)
        {
            foreach (User x in this.ActualOfferList)
            {
                if(x.Id==Id)
                {
                    this.ActualOfferList.Remove(x);
                }
            }
        
        }
        public List<Offer> SearchOffer(/*param*/)
        {
            List<Offer> ValidOffer = new List<Offer>();
            foreach (Offer x in ActualOfferList)
            {
                if (x./*algo*/ = /*param*/)
                {
                    ValidOffer.Add(x);
                }
                
            }
            return ValidOffer;
            
        }
    }
}
