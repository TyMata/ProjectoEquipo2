using System;
using ClassLibrary;
using System.Text;
using NUnit.Framework;
using Ucu.Poo.Locations.Client;

namespace Tests
{
    /// <summary>
    /// Prueba el handler para publicar una oferta.
    /// </summary>
    [TestFixture]
    public class PublishOfferHandlerTest
    {
        private Offer oferta;
        private Material material;
        private DateTime dateTime;
        private PublishOfferHandler handler;
        private LocationAdapter location;
        private IMessage message;
        private Company company;

        /// <summary>
        /// SetUp de la clase ModifyQuantityHandlerTest.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            message = new TelegramBotMessage(1234, "/publicaroferta");
            location = new LocationAdapter("Comandante Braga 2715", "Montevideo", "Montevideo");
            material = new Material("material", "type", "clasificacion");
            company =  CompanyRegister.Instance.CreateCompany("Nombre de la empresa", location, "headings", "company@gmail.com", "091919191");
            company.AddUser(1234);
            company.ProducedMaterials.Add(material);
           
            
            handler = new PublishOfferHandler();
        }

        /// <summary>
        /// Prueba que el InternalHandle se haga correctamente y cambie el estado del handler..
        /// </summary>
        [Test]
        public void HandleStartTest()
        {
            string response;
            bool result = handler.InternalHandle(message, out response);
            StringBuilder materials = new StringBuilder("Estos son los materiales de tu empresa:\n\n");
            foreach (Material item in this.company.ProducedMaterials)
            {
                materials.Append($"Nombre del Material: {item.Name}\n")
                         .Append($"Tipo: {item.Type}\n")
                         .Append($"Clasificación: {item.Classification}\n")
                         .Append($"\n-----------------------------------------------\n\n");
            }
            materials.Append($"¿Qué material desea vender?\n")
                     .Append($"Ingrese el nombre.");
            response = materials.ToString();
            Assert.IsTrue(result);
            Assert.That(response, Is.EqualTo(materials.ToString())); 
            Assert.That(handler.State, Is.EqualTo(PublishOfferHandler.OfferState.Material));
        }

        /// <summary>
        /// Prueba que el InternalHandle se haga correctamente y cambie el estado del handler.
        /// </summary>
        [Test]
        public void HandleMaterialTest()
        {
            string response;
            bool result = handler.InternalHandle(this.message, out response);
            this.message.Text = "material";
            result = this.handler.InternalHandle(message, out response);
            Assert.IsTrue(result);
            Assert.That(response, Is.EqualTo("Ingrese la cantidad de material.")); 
            Assert.That(this.handler.State, Is.EqualTo(PublishOfferHandler.OfferState.Quantity));
        }

        /// <summary>
        /// Prueba que el InternalHandle se haga correctamente, que cambie el estado del handler al estado inicial
        ///  y que se cambie la cantidad del material de la oferta correctamente.
        /// </summary>
        [Test]
        public void HandleQuantityTest()
        {
            string response;
            bool result = handler.InternalHandle(message, out response);
            message.Text = "material";
            result = handler.InternalHandle(message, out response);
            message.Text = "12";
            result = handler.InternalHandle(message, out response);
            Assert.IsTrue(result);
            Assert.That(response, Is.EqualTo("¿Cuál va a ser el precio total?")); 
            Assert.That(handler.State, Is.EqualTo(PublishOfferHandler.OfferState.Price));

        }
        /// <summary>
        /// Prueba que el InternalHandle se haga correctamente, que cambie el estado del handler al estado inicial 
        /// y que se modifique el precio de la oferta correctamente.
        /// </summary>
        [Test]
        public void HandlePriceTest()
        {
            string response;
            bool result = handler.InternalHandle(message, out response);
            message.Text = "material";
            result = handler.InternalHandle(message, out response);
            message.Text = "12";
            result = handler.InternalHandle(message, out response);
            message.Text="8000";
            result = handler.InternalHandle(message, out response);
            StringBuilder location = new StringBuilder("Estas son las locaciones de tu empresa:\n");
            foreach (LocationAdapter item in this.company.Locations) 
            {
                location.Append($"Departamento: {item.Department}\n") 
                        .Append($"Ciudad: {item.City}\n")
                        .Append($"Dirección: {item.Address}\n")   
                        .Append($"-----------------------------------------------\n\n");
            }
            location.Append("Ingresa la dirección de esta:\n");
            Assert.IsTrue(result);
            Assert.That(response, Is.EqualTo(location.ToString()));
            Assert.That(handler.State, Is.EqualTo(PublishOfferHandler.OfferState.Location));

        }
        /// <summary>
        /// Prueba que el InternalHandle se haga correctamente, que cambie el estado del handler al estado inicial 
        /// y que se modifique la location de la oferta correctamente.
        /// </summary>
        [Test]
        public void HandleLocationTest()
        {
            string response;
            bool result = handler.InternalHandle(message, out response);
            message.Text = "material";
            result = handler.InternalHandle(message, out response);
            message.Text = "12";
            result = handler.InternalHandle(message, out response);
            message.Text="8000";
            result = handler.InternalHandle(message, out response);
            message.Text = "Comandante Braga 2715";
            result = handler.InternalHandle(message, out response);
            Assert.IsTrue(result);
            Assert.That(response, Is.EqualTo("¿Que habilitaciones son necesarias para poder manipular este material?")); 
            Assert.That(handler.State, Is.EqualTo(PublishOfferHandler.OfferState.Habilitations));

        }
        /// <summary>
        ///  /// Prueba que el InternalHandle se haga correctamente, que cambie el estado del handler al estado inicial 
        /// y que se modifique las habilitaciones de la oferta correctamente.
        /// </summary>
        [Test]
        public void HandleHabilitationTest()
        {
            string response;
            bool result = handler.InternalHandle(message, out response);
            message.Text = "material";
            result = handler.InternalHandle(message, out response);
            message.Text = "12";
            result = handler.InternalHandle(message, out response);
            message.Text="8000";
            result = handler.InternalHandle(message, out response);
            message.Text = "Comandante Braga 2715";
            result = handler.InternalHandle(message, out response);
            message.Text = "Link";
            result = handler.InternalHandle(message, out response);
            Assert.IsTrue(result);
            Assert.That(response, Is.EqualTo($"Para determinar la continuidad de la oferta, escriba \"continua\" si es continua, o \"puntual\" si es puntual.")); 
            Assert.That(handler.State, Is.EqualTo(PublishOfferHandler.OfferState.Continuity));
            
        

        }

           [Test]
        public void HandleContinuityTest()
        {
            string response;
            bool result = handler.InternalHandle(message, out response);
            message.Text = "material";
            result = handler.InternalHandle(message, out response);
            message.Text = "12";
            result = handler.InternalHandle(message, out response);
            message.Text = "8000";
            result = handler.InternalHandle(message, out response);
            message.Text = "Comandante Braga 2715";
            result = handler.InternalHandle(message, out response);
            message.Text = "Link";
            result = handler.InternalHandle(message, out response);
            message.Text = "continua";
            result = handler.InternalHandle(message, out response);
            Assert.IsTrue(result);
            Assert.That(response, Is.EqualTo("La oferta ha sido creada y publicada en el mercado.")); 
            Assert.That(handler.State, Is.EqualTo(PublishOfferHandler.OfferState.Start));
            Assert.IsNotNull(Market.Instance.ActualOfferList);
            

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
