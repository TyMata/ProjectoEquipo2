using System;
using System.Collections.Generic;
namespace ClassLibrary
{
    /// <summary>
    /// Esta clase es la encargada de modificar una oferta
    /// </summary>
    public class ModifyOffer
    {
      /// <summary>
      /// Modifica la cantidad de materiales en una oferta
      /// </summary>
      /// <param name="newOffer"></param>
      /// <param name="newQuantity"></param>
      public void Quantity(Offer newOffer,int newQuantity )
      {
        newOffer.QuantityMaterial=newQuantity;

      }
      /// <summary>
      /// Modifica las palabras claves de una oferta
      /// </summary>
      /// <param name="newOffer"></param>
      /// <param name="newKeyword"></param>
      public void Keywords(Offer newOffer,string newKeyword )
      {
        newOffer.Keywords=newKeyword;

      }
      /// <summary>
      /// Modifica la disponibilidad de una oferta
      /// </summary>
      /// <param name="newOffer"></param>
      /// <param name="newAvailability"></param>
      public void Availability(Offer newOffer,bool newAvailability )
      {
        newOffer.Availability=newAvailability;

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
          /// <summary>
          /// Modifica la Location de una oferta
          /// </summary>
          /// <param name="NewOffer"></param>
          /// <param name="newAdress"></param>
          /// <param name="newCity"></param>
          /// <param name="newDepartment"></param>
          public void Location(Offer NewOffer, string newAdress, string newCity, string newDepartment)
          {
            /*NewOffer.Location = newAddress;
            NewOffer.Location = newCity;
            NewOffer.Location = newDepartment;*/         
          }

      }
      /// <summary>
      /// Modifica la Location de una oferta
      /// </summary>
      /// <param name="NewOffer"></param>
      /// <param name="newAdress"></param>
      /// <param name="newCity"></param>
      /// <param name="newDepartment"></param>
      public void Location(Offer NewOffer, string newAdress, string newCity, string newDepartment)
      {
        /*NewOffer.Location = newAdress;
        NewOffer.Location = newCity;
        NewOffer.Location = newDepartment;*/         
      }

    }
}
 
