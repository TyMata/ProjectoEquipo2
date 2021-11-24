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
        private ModifyQuantityHandler handler;
        private LocationAdapter location;

        /// <summary>
        /// SetUp de la clase ModifyQuantityHandlerTest.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            location = new LocationAdapter("address", "city", "department");
            this.oferta = new Offer(1234567, new Material(), "habilitation", location, 3, 3000, new Company("nombre", location, "rubro"), true, dateTime);
            this.material = new Material("material", "type", "clasificacion");
            this.handler = new ModifyQuantityHandler();
        }

        /// <summary>
        /// Prueba que se realice el handle.
        /// </summary>
        [Test]
        public void InternalHandleTest()
        {
            string response;
           bool result = this.handler.InternalHandle(new ConsoleMessage("/modificarcantidad"), out response);
            Assert.IsTrue(result);
            //Assert.That(response, Is.EqualTo("Para poder registrar una empresa vamos a necesitar algunos datos de esta.\n\nIngrese el nombre de la empresa:\n"));
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