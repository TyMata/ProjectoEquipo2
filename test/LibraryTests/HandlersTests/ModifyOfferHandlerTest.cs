using System;
using NUnit.Framework;
using ClassLibrary;
using Ucu.Poo.Locations.Client;


namespace Tests
{
    /// <summary>
    /// Prueba el handler para que la empresa pueda modificar la oferta
    /// </summary>
    [TestFixture]
    public class ModifyOfferHandlerTests
    {
        private IMessageChannel channel;
        private IHandler handler;
        private Offer oferta;
        private Location location;
        private IMessage consoleMessage;



        /// <summary>
        /// Se crean instancias de IMessageChannel, el handler a testear y una nueva oferta para lsd pruebas
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            this.channel = new ConsoleMessageChannel();
            this.handler = new ModifyOfferHandler(this.channel);
            this.oferta = new Offer(1234567,"material","habilitaciones", location, 100,1000, new Company("nombre", new Location(), "rubro", "material"),true, new DateTime());
        }

        /// <summary>
        /// Prueba el Handle del handler
        /// </summary>
        [Test]
        public void HandleTest()
        {
            IMessage consoleMessage= new ConsoleMessage("/modificaroferta");
            handler.Handle(consoleMessage);
            string response;

            //Assert.That(handler.Handle(consoleMessage), Is.);
            //Assert.That(response, Is.EqualTo("¡Hola! ¿Cómo estás?"));
       
        }
    }
}