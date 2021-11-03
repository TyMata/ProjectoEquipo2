//--------------------------------------------------------------------------------
// <copyright file="TrainTests.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//--------------------------------------------------------------------------------

using ClassLibrary;
using NUnit.Framework;

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
        private Material material;
        private string habilitation;
        private Location location;
        private int quantityMaterial;
        private Company company;
        private string keywords;
        private bool availability;
        private string publicationDate;
        private int term;

    
        [SetUp]
        public void Setup()
        {
        //this.client = new LocationApiClient();
        this.id = 55234;
        this.material=new Material("quimico","inorganico");
        this.habilitation="ingeniero quimico";
        this.location=new Location(true,"here",1, -1);
        this.quantityMaterial=15;
        this.company=new Company("farmashop",location,"farmacia","acido");
        this.keywords="Acido";
        this.availability=true ;
        this.publicationDate="22 de marzo";
        this.term=22;
        }
        [Test]

        public void CreateOfferTest()
        {
            Offer ofertaCreado = new Offer(this.id,this.material,this.habilitation,this.location,this.quantityMaterial,this.company,this.keywords,this.availability,this.publicationDate,this.term);
            Assert.AreEqual(this.id,ofertaCreado.Id);
            Assert.AreEqual(this.material,ofertaCreado.Material);
            Assert.AreEqual(this.habilitation,ofertaCreado.Habilitation);
            Assert.AreEqual(this.location,ofertaCreado.Location);
            Assert.AreEqual(this.quantityMaterial,ofertaCreado.QuantityMaterial);
            Assert.AreEqual(this.company,ofertaCreado.Company);
            Assert.AreEqual(this.keywords,ofertaCreado.Keywords);
            Assert.AreEqual(this.availability,ofertaCreado.Availability);
            Assert.AreEqual(this.publicationDate,ofertaCreado.PublicationDate);
            Assert.AreEqual(this.term,ofertaCreado.Term);
        }
    }
}
