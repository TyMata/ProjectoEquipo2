using System;
using System.Collections.Generic;
namespace ClassLibrary
{
    /// <summary>
    /// Esta clase 
    /// </summary>
    public class ModifyOffer
    {
         /// <summary>
         /// Modifica la cantidad de materiales en una oferta
         /// </summary>
         /// <param name="NewOffer"></param>
         /// <param name="NewQuantity"></param>
          public void Quantity(Offer NewOffer,int NewQuantity )
          {
              NewOffer.QuantityMaterial=NewQuantity;

          }
          /// <summary>
          /// Modifica las palabras claves de una oferta
          /// </summary>
          /// <param name="NewOffer"></param>
          /// <param name="NewKeyword"></param>
          public void Keywords(Offer NewOffer,string NewKeyword )
          {
               NewOffer.Keywords=NewKeyword;

          }
          /// <summary>
          /// Modifica la disponibilidad de una oferta
          /// </summary>
          /// <param name="NewOffer"></param>
          /// <param name="NewDisponibility"></param>
          public void Disponibility(Offer NewOffer,bool NewDisponibility )
          {
              NewOffer.Disponibility=NewDisponibility;

          }
          /// <summary>
          /// Modifica el plazo de una oferta
          /// </summary>
          /// <param name="NewOffer"></param>
          /// <param name="NewExtendTerm"></param>
          public void Term(Offer NewOffer,int NewExtendTerm )
          {
               NewOffer.Term=NewExtendTerm;

          }
    }
}
 
