using System;
using System.Collections.Generic;
namespace ClassLibrary
{
    /// <summary>
    /// Esta clase 
    /// </summary>
    public class ModifyOffer
    {
         public void Quantity(Offer NewOffer,int NewQuantity )
        {
             NewOffer.QuantityMaterial=NewQuantity;
            
        }
        public void Keywords(Offer NewOffer,string NewKeyword )
        {
             NewOffer.Keywords=NewKeyword;
            
        }
        public void Disponibility(Offer NewOffer,bool NewDisponibility )
        {
             NewOffer.Disponibility=NewDisponibility;
            
        }
        public void Term(Offer NewOffer,int NewExtendTerm )
        {
             NewOffer.Term=NewExtendTerm;
            
        }
    }
}
 
