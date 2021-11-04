//--------------------------------------------------------------------------------
// <copyright file="TrainTests.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//--------------------------------------------------------------------------------

using ClassLibrary;
using NUnit.Framework;
using Ucu.Poo.Locations.Client;
using System;

namespace Tests
{
    /// <summary>
    /// Prueba de la clase <see cref="Offer"/>.
    /// </summary>
    [TestFixture]
    public class OfferTest
    {
        //public LocationApiClient client;
        private int id;
        private string material;
        private string habilitation;
        private Location location;
        private int quantityMaterial;
        private Company company;
        private string keywords;
        private bool availability;
        private int term;
        private DateTime publicationDate;
        private double totalPrice;

    
        [SetUp]
        public void Setup()
        {
        //this.client = new LocationApiClient();
        
        this.id = 55234;
        this.material= "quimico, inorganico";
        this.habilitation="ingeniero quimico";
        string pais = "Uruguay" ;
        string departamento = "Montevideo" ;
        string ciudad = "Montevideo";
        string direccion = "Avenida 8 de Octubre";
        Location ubi = LocationServiceProvider.client.GetLocationAsync(pais, departamento, ciudad, direccion).Result;
        this.location=new Location();
        this.quantityMaterial=15;
        this.company=new Company("farmashop", ubi,"farmacia","acido");
        this.keywords="Acido";
        this.availability=true ;
        this.publicationDate= DateTime.Today;
        this.totalPrice= 50000;
        }
        [Test]

        public void CreateOfferTest()
        {
            Offer ofertaCreado = new Offer(this.id,this.material,this.habilitation,this.location,this.quantityMaterial,this.totalPrice,this.company,this.keywords,this.availability,this.publicationDate);
            Assert.AreEqual(this.id,ofertaCreado.Id);
            Assert.AreEqual(this.material,ofertaCreado.Material);
            Assert.AreEqual(this.habilitation,ofertaCreado.Habilitation);
            Assert.AreEqual(this.location,ofertaCreado.Location);
            Assert.AreEqual(this.quantityMaterial,ofertaCreado.QuantityMaterial);
            Assert.AreEqual(this.company,ofertaCreado.Company);
            Assert.AreEqual(this.keywords,ofertaCreado.Keywords);
            Assert.AreEqual(this.availability,ofertaCreado.Availability);
            Assert.AreEqual(this.publicationDate,ofertaCreado.PublicationDate);
            Assert.AreEqual(this.totalPrice,ofertaCreado.TotalPrice);
        }


    }
}
