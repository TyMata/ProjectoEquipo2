using System;
using ClassLibrary;
using NUnit.Framework;
using Ucu.Poo.Locations.Client;

namespace Tests
{
    /// <summary>
    /// Prueba de la clase <see cref="Offer"/>.
    /// </summary>
    [TestFixture]
    public class OfferTest
    {
        // public LocationApiClient client;
        private int id;
        private string material;
        private string habilitation;
        private Location location;
        private int quantityMaterial;
        private Company company;
        private string keywords;
        private bool availability;
        private DateTime publicationDate;
        private double totalPrice;

        /// <summary>
        ///  Set up del test de Offer.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            this.id = 55234;
            this.material = "quimico, inorganico";
            this.habilitation = "ingeniero quimico";
            string pais = "Uruguay";
            string departamento = "Montevideo";
            string ciudad = "Montevideo";
            string direccion = "Avenida 8 de Octubre";
            Location ubi = LocationServiceProvider.client.GetLocationAsync(pais, departamento, ciudad, direccion).Result;
            this.location = new Location();
            this.quantityMaterial = 15;
            this.company = new Company("farmashop", ubi, "farmacia", "acido");
            this.keywords = "Acido";
            this.availability = true;
            this.publicationDate = DateTime.Today;
            this.totalPrice = 50000;
        }

        /// <summary>
        /// Prueba para crear una oferta.
        /// </summary>
        [Test]
        public void CreateOfferTest()
        {
            Offer oferta = new Offer(this.id, this.material, this.habilitation, this.location, this.quantityMaterial, this.totalPrice, this.company, this.availability, this.publicationDate);
            Assert.AreEqual(this.id, oferta.Id);
            Assert.AreEqual(this.material, oferta.Material);
            Assert.AreEqual(this.habilitation, oferta.Habilitation);
            Assert.AreEqual(this.location, oferta.Location);
            Assert.AreEqual(this.quantityMaterial, oferta.QuantityMaterial);
            Assert.AreEqual(this.company, oferta.Company);
            Assert.AreEqual(this.availability, oferta.Availability);
            Assert.AreEqual(this.publicationDate, oferta.PublicationDate);
            Assert.AreEqual(this.totalPrice, oferta.TotalPrice);
        }
    }
}
