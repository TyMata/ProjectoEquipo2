using System;

namespace ClassLibrary
{
    /// <summary>
    /// Esta clase 
    /// </summary>
    public class Offer
    {
        public int Id{get;set;}
        public Material Material{get;set;}
        public string Habilitation{get;set;}
        public Location Location{get;set;}
        public int QuantityMaterial{get;set;}
        public Company Company{get;set;}
        public string Keywords{get;set;}
        public bool Disponibility{get;set;}
        public string PublicationDate{get;set;}
        public int Term{get;set;}


        public Offer(int id,Material material,string habilitation,Location location,int cuantityMaterial,Company company,string keywords,bool disponibility,string publicationDate,int term)
    {
        this.Id = id;
        this.Material=material;
        this.Habilitation=habilitation;
        this.Location=location;
        this.QuantityMaterial=cuantityMaterial;
        this.Company=company;
        this.Keywords=keywords;
        this.Disponibility=disponibility;
        this.PublicationDate=publicationDate;
        this.Term=term;
    }
  }
}
