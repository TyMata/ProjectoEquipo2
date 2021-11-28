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
        private LocationAdapter location;

        /// <summary>
        /// Se crea una instancia de offer.
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            location = new LocationAdapter("address", "city", "department");
            this.company = new Company("empresa", location, "rubro", "company@gmail.com", "091919191");
            this.oferta = new Offer(1234567, new Material(), "habilitaciones", location, 30, 3000, this.company, true, new DateTime(), "constante");
        }

        /// <summary>
        /// Prueba que la Oferta se publique.
        /// </summary>
        [Test]
        public void PublishOfferTest()
        {
            Market.Instance.PublishOffer(this.oferta);
            Assert.IsNotEmpty(Market.Instance.ActualOfferList);
        }

        /// <summary>
        /// Prueba que se remueva una oferta del registro de ofertas.
        /// </summary>
        [Test]
        public void RemoveOfferTest()
        {
            Market.Instance.RemoveOffer(1234567);
            Assert.IsEmpty(Market.Instance.ActualOfferList);
        }

        /// <summary>
        /// Prueba que se suspenda o pause la oferta y se cambie de lista, de las actuales a las pausadas.
        /// </summary>
        [Test]
        public void SuspendOfferTest()
        {
            Offer nuevaOferta = new Offer(7654321, new Material(), "habilitaciones", location, 30, 3000, this.company, true, new DateTime(), "constante");
            Market.Instance.PublishOffer(nuevaOferta);
            Market.Instance.SuspendOffer(7654321);
            Assert.IsTrue(Market.Instance.ContainsSuspended(nuevaOferta));
            Assert.IsFalse(Market.Instance.ContainsActive(nuevaOferta));
        }

        /// <summary>
        /// Prueba que se despause una oferta y se cambie de lista, de las suspendidas a las actuales.
        /// </summary>
        [Test]
        public void ResumeOfferTest()
        {
            Market.Instance.SuspendedOfferList.Add(this.oferta);
            Market.Instance.ResumeOffer(1234567);
            Assert.IsTrue(Market.Instance.ContainsActive(this.oferta));
            Assert.IsFalse(Market.Instance.ContainsSuspended(this.oferta));
        }
    }
}