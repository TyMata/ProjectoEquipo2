using System;
using ClassLibrary;
using System.Text;
using NUnit.Framework;
using Ucu.Poo.Locations.Client;

namespace Tests
{
    /// <summary>
    /// Prueba el handler para reanudar una oferta.
    /// Cada test por separado funcionan bien, pero cuando los corremos todos juntos se rompe HandleSuspendOfferTest(). 
    /// Llegamos a la conclusion de que puede ser a acausa de que el SetUp se realice antes de cada test.
    /// </summary>
    [TestFixture]
    public class SuspendOfferHandlerTests
    {
        private Offer oferta;
        private Material material;
        private DateTime dateTime;
        private SuspendOfferHandler handler;
        private LocationAdapter location;
        private IMessage message;
        private IMessage message2;
        private Company company;

        /// <summary>
        /// SetUp de la clase ModifyQuantityHandlerTest.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            message = new TelegramBotMessage(12342, "/suspenderoferta");
            location = new LocationAdapter("Comandante Braga 2715", "Montevideo", "Montevideo");
            this.company =  CompanyRegister.Instance.CreateCompany("Nombre de la empresa", location, "headings", "company@gmail.com", "091919191");
            this.company.AddUser(12342);
            oferta = new Offer(234567, new Material("Pallet","Madera","Residuo"), "habilitation", location,"kg", 3, "pesos", 3000, this.company, true, new DateTime(), "continua");
            this.company.AddOffer(oferta);
            Market.Instance.PublishOffer(oferta);
            this.handler = new SuspendOfferHandler();
        }

        /// <summary>
        /// Prueba que el InternalHandle se haga correctamente y cambie el estado del handler..
        /// </summary>
        [Test]
        public void HandleStartTest()
        {
            string response;
            bool result = this.handler.InternalHandle(message, out response);
            StringBuilder offers = new StringBuilder("Estas son tus ofertas actuales:\n");
            foreach (Offer item in this.company.OfferRegister)
            {
                if(item.Availability)
                {
                    offers.Append($"Id de la oferta: {item.Id}.\n")
                                .Append($"Material de la oferta: {item.Material.Name} de {item.Material.Type}.\n")
                                .Append($"Unidad de medida: {item.UnitOfMeasure}.\n")
                                .Append($"Cantidad: {item.QuantityMaterial}.\n")
                                .Append($"Divisa: {item.Currency}.\n")
                                .Append($"Precio: {item.TotalPrice}.\n")
                                .Append($"Fecha de publicación: {item.PublicationDate}.\n")
                                .Append($"\n-----------------------------------------------\n\n");
                }
            }       
            offers.Append("¿Cuál es el Id de la oferta que quiere suspender?");
            Assert.IsTrue(result);
            Assert.That(response, Is.EqualTo(offers.ToString())); 
            Assert.That(this.handler.State, Is.EqualTo(SuspendOfferHandler.SuspendOfferState.ActiveOfferIdState));
            Assert.IsTrue(this.company.OfferRegister.Contains(oferta));
        }

        /// <summary>
        /// Prueba que el InternalHandle se haga correctamente y cambie el estado del handler.
        /// </summary>
        [Test]
        public void HandleSuspendOfferTest()
        {
            string response;
            bool result = handler.InternalHandle(message, out response);
            message.Text = "234567";
            result = this.handler.InternalHandle(message, out response);
            Assert.IsTrue(result);
            Assert.IsTrue(this.company.OfferRegister.Contains(oferta));
            Assert.That(response, Is.EqualTo("La oferta ha sido suspendida.")); 
            Assert.That(this.handler.State, Is.EqualTo(SuspendOfferHandler.SuspendOfferState.Start));
            Assert.IsTrue(Market.Instance.SuspendedOfferList.Contains(oferta));
        }

        /// <summary>
        /// Prueba que no se realice el handler.
        /// </summary>
        [Test]
        public void InternalNotHandleTest()
        {
            string response;
            IHandler result = this.handler.Handle(new ConsoleMessage("/modificarprecio"),out response);
            Assert.IsNull(result);
            Assert.IsEmpty(response);
        }
    }
}