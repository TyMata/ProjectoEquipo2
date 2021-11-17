using System;
using ClassLibrary;
using NUnit.Framework;
using Ucu.Poo.Locations.Client;

namespace Tests
{
    /// <summary>
    /// Prueba el handler para modificar la cantidad en una oferta.
    /// </summary>
    [TestFixture]
    public class ModifyQuantityHandlerTest
    {
        private Offer oferta;
        private Material material;
        private DateTime dateTime;
        private IHandler handler;
        private Location location;

        /// <summary>
        /// SetUp de la clase ModifyQuantityHandlerTest.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            this.oferta = new Offer(1234567, "material", "habilitation", location, 3, 3000, new Company("nombre", new Location(), "rubro", "material"), true, dateTime);
            this.material = new Material("material", "type", "clasificacion");
            IMessageChannel messageChannel = new ConsoleMessageChannel();
            this.handler = new ModifyQuantityHandler(messageChannel);
        }

        /// <summary>
        /// Prueba que se realice el handle
        /// </summary>
        [Test]
        public void InternalHandleTest()
        {
            Assert.IsTrue(handler.InternalHandle(new ConsoleMessage("/modificarcantidad")));
        }

        /// <summary>
        /// Prueba que no se realice el handler
        /// </summary>
        [Test]
        public void InternalNotHandleTest()
        {
            Assert.IsFalse(handler.InternalHandle(new ConsoleMessage("/modificarprecio")));
        }
    }
}