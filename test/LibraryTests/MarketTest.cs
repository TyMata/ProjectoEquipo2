using System;
using NUnit.Framework;
using ClassLibrary;
using Ucu.Poo.Locations.Client;

namespace Tests
{

    /// <summary>
    /// Prueba  la clase <see cref="Market"/>
    /// </summary>
    [TestFixture]
    public class MarketTests
    {
        private Offer oferta;

        /// <summary>
        /// se crea una instancia de offer
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            this.oferta = new Offer(1234567,"material","habilitaciones", new Location(),30,3000, new Company(),true, new DateTime());
        }

        /// <summary>
        /// Prueba que la Oferta se publique
        /// </summary>
        [Test]
        public void PublishOfferTest()
        {
            Singleton<Market>.Instance.PublishOffer(this.oferta);
            Assert.IsNotEmpty(Singleton<Market>.Instance.ActualOfferList);

        }
        
        /// <summary>
        /// Prueba que se remueva una oferta del registro de ofertas
        /// </summary>
        [Test]
        public void RemoveOfferTest()
        {
            Singleton<Market>.Instance.RemoveOffer(1234567);
            Assert.IsEmpty(Singleton<Market>.Instance.ActualOfferList);
        }

        /// <summary>
        /// Prueba que se suspenda o pause la oferta y se cambie de lista, de las actuales a las pausadas
        /// </summary>
        [Test]
        public void SuspendOfferTest()
        {
            Singleton<Market>.Instance.ActualOfferList.Add(this.oferta);
            Singleton<Market>.Instance.SuspendOffer(1234567);
            Assert.IsNotEmpty( Singleton<Market>.Instance.SuspendedOfferList);
            Assert.IsEmpty( Singleton<Market>.Instance.ActualOfferList);
        }

        /// <summary>
        /// Prueba que se despause una oferta y se cambie de lista, de las suspendidas a las actuales
        /// </summary>
        [Test]
        public void ResumeOfferTest()
        {
            Singleton<Market>.Instance.SuspendedOfferList.Add(this.oferta);
            Singleton<Market>.Instance.ResumeOffer(1234567);
            Assert.IsNotEmpty(Singleton<Market>.Instance.ActualOfferList);
            Assert.IsEmpty(Singleton<Market>.Instance.SuspendedOfferList);

        }
    }
}