using System;
using ClassLibrary;
using System.Text;
using NUnit.Framework;


namespace Tests
{
    /// <summary>
    /// Prueba el handler para modificar las habilitaciones en una oferta.
    /// </summary>
    [TestFixture]
    public class ModifyHabilitationsHandlerTests
    {
        private Offer oferta;
        private Material material;
        private DateTime dateTime;
        private ModifyHabilitationsHandler handler;
        private LocationAdapter location;
        private IMessage message;
        private Company company;

        /// <summary>
        /// SetUp de la clase ModifyQuantityHandlerTest.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            message = new TelegramBotMessage(1234, "/modificarhabilitaciones");
            location = new LocationAdapter("address", "city", "department");
            oferta = new Offer(1234567, new Material(), "habilitation", location, 3, 3000, new Company("nombre", location, "rubro", "company@gmail.com", "091919191"), true, dateTime, "constante");
            material = new Material("material", "type", "clasificacion");
            company =  CompanyRegister.Instance.CreateCompany("Nombre de la empresa", location, "headings", "company@gmail.com", "091919191");
            company.AddUser(1234);
            company.AddOffer(oferta);
            
            handler = new ModifyHabilitationsHandler();
        }

        /// <summary>
        /// Prueba que el InternalHandle se haga correctamente y cambie el estado del handler.
        /// </summary>
        [Test]
        public void HandleStartTest()
        {
            string response;
            bool result = handler.InternalHandle(message, out response);
            StringBuilder sb = new StringBuilder("Que oferta desea modificar?\n");
            foreach (Offer x in company.OfferRegister)
            {
                sb.Append($"Id: {x.Id}\n")
                        .Append($"Material: {x.Material}\n")
                        .Append($"Cantidad: {x.QuantityMaterial}\n")
                        .Append($"Fecha de publicacion: {x.PublicationDate}\n")
                        .Append($"Precio: {x.TotalPrice}\n")
                        .Append($"\n-----------------------------------------------\n\n");  
            }
            sb.Append("Ingrese el Id de la oferta a modificar:\n");
            Assert.IsTrue(result);
            Assert.That(response, Is.EqualTo(sb.ToString())); 
            Assert.That(handler.State, Is.EqualTo(ModifyHabilitationsHandler.ModifyState.OfferList));
        }

        /// <summary>
        /// Prueba que el InternalHandle se haga correctamente y cambie el estado del handler.
        /// </summary>
        [Test]
        public void HandleOfferListTest()
        {
            string response;
            bool result = handler.InternalHandle(message, out response);
            message.Text = "1234567";
            result = handler.InternalHandle(message, out response);
            Assert.IsTrue(result);
            Assert.That(response, Is.EqualTo("Pase por aquí el link que lleva a sus habilitaciones\n")); 
            Assert.That(handler.State, Is.EqualTo(ModifyHabilitationsHandler.ModifyState.Modification));
        }

        /// <summary>
        /// Prueba que el InternalHandle se haga correctamente, que cambie el estado del handler al estado inicial
        ///  y que se cambien las habilitaciones del material de la oferta correctamente.
        /// </summary>
        [Test]
        public void HandleFinaleTest()
        {
            string response;
            bool result = handler.InternalHandle(message, out response);
            message.Text = "1234567";
            result = handler.InternalHandle(message, out response);
            message.Text = "link";
            result = handler.InternalHandle(message, out response);
            Assert.IsTrue(result);
            Assert.AreEqual("link",oferta.Habilitation);
            Assert.That(response, Is.EqualTo("Las habilitaciones se han modificado")); 
            Assert.That(handler.State, Is.EqualTo(ModifyHabilitationsHandler.ModifyState.Start));
        }

        /// <summary>
        /// Prueba que no se realice el handler.
        /// </summary>
        [Test]
        public void InternalNotHandleTest()
        {
            string response;
            IHandler result = handler.Handle(new ConsoleMessage("/modificarprecio"),out response);
            Assert.IsNull(result);
            Assert.IsEmpty(response);
        }
    }
}
