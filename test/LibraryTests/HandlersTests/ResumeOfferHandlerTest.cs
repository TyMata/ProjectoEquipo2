using System;
using ClassLibrary;
using System.Text;
using NUnit.Framework;
using Ucu.Poo.Locations.Client;

namespace Tests
{
    /// <summary>
    /// Prueba el handler para reanudar una oferta.
    /// </summary>
    [TestFixture]
    public class ResumeOfferHandlerTest
    {
        private Offer oferta;
        private Material material;
        private DateTime dateTime;
        private ResumeOfferHandler handler;
        private LocationAdapter location;
        private IMessage message;
        private Company company;

        /// <summary>
        /// SetUp de la clase ModifyQuantityHandlerTest.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            message = new TelegramBotMessage(1234, "/reanudaroferta");
            location = new LocationAdapter("address", "city", "department");
             company =  CompanyRegister.Instance.CreateCompany("Nombre de la empresa", location, "headings", "company@gmail.com", "091919191");
            company.AddUser(1234);
            oferta = Market.Instance.CreateOffer(new Material(), "habilitation", location, 3, 3000,company, true, "constante");
            material = new Material("material", "type", "clasificacion");
           Market.Instance.SuspendOffer(oferta.Id);
            handler = new ResumeOfferHandler();
        }

        /// <summary>
        /// Prueba que el InternalHandle se haga correctamente y cambie el estado del handler..
        /// </summary>
        [Test]
        public void HandleStartTest()
        {
            string response;
            bool result = handler.InternalHandle(message, out response);
            StringBuilder offers = new StringBuilder();
            foreach (Offer item in this.company.OfferRegister)
                    {
                        if(!item.Availability)
                        {
                            offers.Append($"Id de la oferta: {item.Id}\n")
                                .Append($"Material de la oferta: {item.Material}\n")
                                .Append($"Cantidad: {item.QuantityMaterial}\n")
                                .Append($"Fecha de publicacion: {item.PublicationDate}\n")
                                .Append($"\n-----------------------------------------------\n\n");
                        }
                    }
                    offers.Append("¿Cual es el Id de la que quiere activar?\n");
            Assert.IsTrue(result);
            Assert.That(response, Is.EqualTo(offers.ToString())); 
            Assert.That(handler.State, Is.EqualTo(ResumeOfferHandler.ResumeOfferState.SuspendedOfferIdState));
        }

        /// <summary>
        /// Prueba que el InternalHandle se haga correctamente y cambie el estado del handler.
        /// </summary>
        [Test]
        public void HandleSuspendOfferTest()
        {
            string response;
            bool result = handler.InternalHandle(message, out response);
            message.Text = oferta.Id.ToString();
            result = handler.InternalHandle(message, out response);
            Assert.IsTrue(result);
            Assert.That(response, Is.EqualTo("La oferta se ha activado nuevamente\n")); //TODO arreglar  HandleSuspendOfferTest
            Assert.That(handler.State, Is.EqualTo(ResumeOfferHandler.ResumeOfferState.Start));
            Assert.IsTrue(Market.Instance.ActualOfferList.Contains(oferta));
        }

        /// <summary>
        /// Prueba que no se realice el handler.
        /// </summary>
        [Test]
        public void InternalNotHandleTest()
        {string response;
            IHandler result = handler.Handle(new ConsoleMessage("/modificarprecio"),out response);
            Assert.IsNull(result);
            Assert.IsEmpty(response);
        }
    }
}