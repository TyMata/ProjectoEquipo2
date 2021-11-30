using System;
using ClassLibrary;
using System.Text;
using NUnit.Framework;


namespace Tests
{
    /// <summary>
    /// Prueba el handler para modificar la cantidad en una oferta.
    /// Cada test por separado funcionan bien, pero cuando los corremos todos juntos se rompe Handle3FinaleTest(). 
    /// Llegamos a la conclusion de que puede ser a acausa de que el SetUp se realice antes de cada test.
    /// </summary>
    [TestFixture]
    public class ModifyPriceHandlerTests
    {
        private Offer oferta ;
        private Material material;
        private DateTime dateTime;
        private ModifyPriceHandler handler;
        private LocationAdapter location;
        private IMessage message;
        private Company company;

        /// <summary>
        /// SetUp de la clase ModifyQuantityHandlerTest.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            message = new TelegramBotMessage(1234, "/modificarprecio");
            location = new LocationAdapter("Comandante Braga 2715", "Montevideo", "Montevideo");
            material = new Material("Pallet", "Madera", "Residuo");
            company =  CompanyRegister.Instance.CreateCompany("Nombre de la empresa", location, "headings", "company@gmail.com", "091919191");
            company.AddUser(1234);
            oferta = new Offer(1234567, material, "habilitation", location, 5, 3000, company, true, dateTime, "continua");
            company.AddOffer(oferta);
            handler = new ModifyPriceHandler();
        }

        /// <summary>
        /// Prueba que el InternalHandle se haga correctamente y cambie el estado del handler..
        /// </summary>
        [Test]
        public void Handle1StartTest()
        {
            string response;
            bool result = handler.InternalHandle(message, out response);
            StringBuilder sb = new StringBuilder("¿Qué oferta desea modificar?\n");
            foreach (Offer x in company.OfferRegister)
            {
                sb.Append($"Id: {x.Id}.\n")
                            .Append($"Material: {x.Material.Name} de {x.Material.Type}.\n")
                            .Append($"Cantidad: {x.QuantityMaterial}.\n")
                            .Append($"Fecha de publicación: {x.PublicationDate}.\n")
                            .Append($"Precio: {x.TotalPrice}.\n")
                            .Append($"\n-----------------------------------------------\n\n");
            }
            sb.Append("Ingrese el Id de la oferta a modificar.\n");
            Assert.IsTrue(result);
            Assert.That(response, Is.EqualTo(sb.ToString())); 
            Assert.That(handler.State, Is.EqualTo(ModifyPriceHandler.ModifyState.OfferList));
        }

        /// <summary>
        /// Prueba que el InternalHandle se haga correctamente y cambie el estado del handler.
        /// </summary>
        [Test]
        public void Handle2OfferListTest()
        {
            string response;
            bool result = handler.InternalHandle(message, out response);
            message.Text = oferta.Id.ToString();
            result = handler.InternalHandle(message, out response);
            Assert.IsTrue(result);
            Assert.That(response, Is.EqualTo("Ingrese el nuevo precio de la oferta.")); 
            Assert.That(handler.State, Is.EqualTo(ModifyPriceHandler.ModifyState.Modification));
        }

        /// <summary>
        /// Prueba que el InternalHandle se haga correctamente, que cambie el estado del handler al estado inicial
        ///  y que se cambie la cantidad del material de la oferta correctamente.
        /// </summary>
        [Test]
        public void Handle3FinaleTest()
        {
            string response;
            bool result = handler.InternalHandle(message, out response);
            message.Text = "1234567";
            result = handler.InternalHandle(message, out response);
            message.Text = "4500";
            result = handler.InternalHandle(message, out response);
            Assert.IsTrue(result);
            Assert.That(response, Is.EqualTo("El precio se ha modificado.")); 
            Assert.That(handler.State, Is.EqualTo(ModifyPriceHandler.ModifyState.Start));
            Assert.AreEqual(handler.Data.Result.Id, oferta.Id);
            Assert.AreEqual(4500, oferta.TotalPrice);

        }

        /// <summary>
        /// Prueba que no se realice el handler.
        /// </summary>
        [Test]
        public void InternalNotHandleTest()
        {
            string response;
            IHandler result = handler.Handle(new ConsoleMessage("/modificarquantity"),out response);
            Assert.IsNull(result);
            Assert.IsEmpty(response);
        }
    }
}
