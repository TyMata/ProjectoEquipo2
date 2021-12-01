using System;
using System.Text;
using ClassLibrary;
using NUnit.Framework;

namespace Tests
{
    /// <summary>
    /// Prueba la clase <see cref="ShowCompanyOffersHandler"/>
    /// </summary>
    [TestFixture]
    public class ShowCompanyOffersHandlerTests
    {
        private Company company;
        private IMessage message;
        private LocationAdapter location;
        private Material material;
        private ShowCompanyOffersHandler handler;
        private Offer oferta;
        private DateTime dateTime;

        /// <summary>
        ///  Se crea una oferta de una empresa para las pruebas.
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            message = new TelegramBotMessage(1234, "/mostrarofertas");
            location = new LocationAdapter("Comandante Braga 2715", "city", "department");
            oferta = new Offer(1234567, new Material(), "habilitation", location, "kg", 3, "pesos", 3000, new Company("nombre", location, "rubro", "company@gmail.com", "091919191"), true, dateTime, "constante");
            material = new Material("Pallet", "Plastico", "Residuo");
            company =  CompanyRegister.Instance.CreateCompany("Nombre de la empresa", location, "headings", "company@gmail.com", "091919191");
            company.AddUser(1234);
            company.AddOffer(oferta);
            handler = new ShowCompanyOffersHandler();
        }

        /// <summary>
        /// Prueba que se procese el mensaje y que se muestren las ofertas actuales de la empresa.
        /// </summary>
        [Test]
        public void HandleStartTest()
        {
            string response;
            bool result = handler.InternalHandle(message, out response);
            StringBuilder offers = new StringBuilder("Estas son tus ofertas actuales:\n");
            foreach (Offer item in this.company.OfferRegister)
            {
                offers.Append($"Id de la oferta: {item.Id}.\n")
                                .Append($"Material de la oferta: {item.Material.Name} de {item.Material.Type}.\n")
                                .Append($"Unidad de medida: {item.UnitOfMeasure}.\n")
                                .Append($"Cantidad: {item.QuantityMaterial}.\n")
                                .Append($"Divisa: {item.Currency}.\n")
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
                offers.Append($"\n-----------------------------------------------\n\n");
            }
            Assert.IsTrue(result);
            Assert.That(response, Is.EqualTo(offers.ToString())); 
        }

        /// <summary>
        /// Prueba que no se procese el mensaje ya que no es el correcto.
        /// </summary>
        [Test]
        public void DoesNotHandleTest()
        {
            string response;
            IHandler result = handler.Handle(new ConsoleMessage("/modificarcanitdad"),out response);
            Assert.IsNull(result);
            Assert.IsEmpty(response);
        }
    }
}