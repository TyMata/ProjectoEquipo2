using System;
using ClassLibrary;
using NUnit.Framework;
using Ucu.Poo.Locations.Client;

namespace Tests
{
    /// <summary>
    /// Prueba  la clase <see cref="Market"/>.
    /// </summary>
    [TestFixture]
    public class MarketTest
    {
        private Offer oferta;
        private Company company;

        /// <summary>
        /// Se crea una instancia de offer.
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            this.company = new Company("empresa", new Location(), "rubro", "materiales");
            this.oferta = new Offer(1234567, "material", "habilitaciones", new Location(), 30, 3000, this.company, true, new DateTime());
        }

        /// <summary>
        /// Prueba que la Oferta se publique.
        /// </summary>
        [Test]
        public void PublishOfferTest()
        {
            Singleton<Market>.Instance.PublishOffer(this.oferta);
            Assert.IsNotEmpty(Singleton<Market>.Instance.ActualOfferList);
        }

        /// <summary>
        /// Prueba que se remueva una oferta del registro de ofertas.
        /// </summary>
        [Test]
        public void RemoveOfferTest()
        {
            Singleton<Market>.Instance.RemoveOffer(1234567);
            Assert.IsEmpty(Singleton<Market>.Instance.ActualOfferList);
        }

        /// <summary>
        /// Prueba que se suspenda o pause la oferta y se cambie de lista, de las actuales a las pausadas.
        /// </summary>
        [Test]
        public void SuspendOfferTest()
        {
            Offer nuevaOferta = new Offer(7654321,"material","habilitaciones", new Location(),30,3000, this.company,true, new DateTime());
            Singleton<Market>.Instance.PublishOffer(nuevaOferta);
            Singleton<Market>.Instance.SuspendOffer(7654321);
            Assert.IsTrue( Singleton<Market>.Instance.ContainsSuspended(nuevaOferta));
            Assert.IsFalse( Singleton<Market>.Instance.ContainsActive(nuevaOferta));
        }
        
        /// <summary>
        /// Prueba que se despause una oferta y se cambie de lista, de las suspendidas a las actuales.
        /// </summary>
        [Test]
        public void ResumeOfferTest()
        {
            Singleton<Market>.Instance.SuspendedOfferList.Add(this.oferta);
            Singleton<Market>.Instance.ResumeOffer(1234567);
            Assert.IsTrue(Singleton<Market>.Instance.ContainsActive(this.oferta));
            Assert.IsFalse(Singleton<Market>.Instance.ContainsSuspended(this.oferta));
        }
    }
}