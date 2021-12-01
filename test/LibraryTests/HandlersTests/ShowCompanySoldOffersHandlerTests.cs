using System;
using System.Text;
using System.Linq;
using ClassLibrary;
using NUnit.Framework;

namespace Tests
{
    /// <summary>
    /// Prueba la clase <see cref="ShowCompanySoldOffersHandler"/>
    /// </summary>
    [TestFixture]
    public class ShowCompanySoldOffersHandlerTests
    {
        private IMessage message;
        private Company company;
        private LocationAdapter location;
        private ShowCompanySoldOffersHandler handler;
        private Offer oferta;
        private Material material;

        /// <summary>
        /// Se crean instancias de objetos necesarios para relizar las pruebas.
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            this.message = new TelegramBotMessage(1234, "/mostrarofertasvendidas");
            this.handler = new ShowCompanySoldOffersHandler();
            this.location = new LocationAdapter("Comandante Braga 2715", "Montevideo", "Montevideo");
            this.company = CompanyRegister.Instance.CreateCompany("Empresa", this.location, "Rubro", "company@gmail.com", "099099099");
            company.AddUser(1234);
            this.material = new Material("Pallet", "Madera", "Residuo");
            this.oferta = new Offer(13579, this.material, "link", this.location,"unidades", 30,"pesos", 3000, this.company, true, new DateTime(), "continua");
            Market.Instance.PublishOffer(this.oferta);
            EntrepreneurRole role = new EntrepreneurRole("Emprendedor", "099123456", location, "Rubro", "link" );
            Users user = new Users(2468, role);
            Market.Instance.BuyOffer(13579, user);
        }

        /// <summary>
        /// Prueba que se procese el mensaje que se muestra la lista de las ofertas vendidas de la empresa.
        /// </summary>
        [Test]
        public void HandleTest()
        {
            string response;
            bool result = this.handler.InternalHandle(message, out response);
            StringBuilder offers = new StringBuilder("Estas son tus ofertas actuales:\n");
            foreach (Offer item in this.company.SoldOffers.Keys.ToArray())
            {
                offers.Append($"Id de la oferta: {item.Id}.\n")
                                .Append($"Material de la oferta: {item.Material.Name} de {item.Material.Type}.\n")
                                .Append($"Cantidad: {item.QuantityMaterial}.\n")
                                .Append($"Precio: {item.TotalPrice}.\n")
                                .Append($"Fecha de publicación: {item.PublicationDate}.\n");
                if(item.Availability)
                {
                    offers.Append($"Disponibilidad: Activa.\n");
                }
                else
                {
                    offers.Append($"Disponibilidad: Suspendida.\n");
                }
                offers.Append($"Comprador:\n")
                      .Append(this.company.SoldOffers[item].Role.Data());
                offers.Append($"\n-----------------------------------------------\n\n");
            }
            Assert.IsTrue(result);
            Assert.That(response, Is.EqualTo(offers.ToString())); 
        }
    }
}