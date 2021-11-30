using System;
using ClassLibrary;
using NUnit.Framework;

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
            location = new LocationAdapter("Comandante Braga 2715", "Montevideo", "Montevideo");
            this.company = new Company("empresa", location, "rubro", "company@gmail.com", "091919191");
            this.oferta = new Offer(1234567, new Material("Pallet","Madera","Residuo"), "habilitaciones", location, 30, 3000, this.company, true, new DateTime(), "continua");
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
            Assert.IsFalse(Market.Instance.ContainsActive(this.oferta));
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

        /// <summary>
        /// Prueba que se cree una oferta correctamente, se publique en el mercado y se agregue a la lista de ofertas de la empresa.
        /// </summary>
        [Test]
        public void CreateOffer()
        {
            Offer result = Market.Instance.CreateOffer(new Material("Pallet","Madera","Residuo"),"link", location, 15, 3000, this.company, true, "continua");
            Assert.IsTrue(Market.Instance.ContainsActive(result));
            Assert.IsTrue(this.company.OfferRegister.Contains(result));
        }

        [Test]
        public void BuyOffer()
        {
            UserRegister.Instance.CreateEntrepreneurUser(12345,"098098098","Emprendedor",location, "Rubro", "link");
            Users user = UserRegister.Instance.GetUserById(12345);
            Offer result = Market.Instance.CreateOffer(new Material("Pallet","Madera","Residuo"),"link", location, 15, 3000, this.company, true, "continua");
            Market.Instance.BuyOffer(result.Id, user);
            Assert.IsTrue((user.Role as EntrepreneurRole).Entrepreneur.BoughtList.Contains(result));
            Assert.IsTrue(company.SoldOffers.ContainsKey(result));
        }
    }
}