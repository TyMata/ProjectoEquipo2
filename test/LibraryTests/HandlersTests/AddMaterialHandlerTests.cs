using System;
using ClassLibrary;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class AddMaterialHandlerTests
    {
        private IMessage message;
        private Company company;
        private LocationAdapter location;
        private AddMaterialHandler handler;

        [SetUp]
        public void SetUp()
        {
            message = new TelegramBotMessage(1234, "/agregarmaterial");
            location = new LocationAdapter("Comandante Braga 2715", "Montevideo", "Montevideo");
            this.company =  CompanyRegister.Instance.CreateCompany("Nombre de la empresa", location, "headings", "company@gmail.com", "091919191");
            this.company.AddUser(1234);
            handler = new AddMaterialHandler();
        }

        [Test]
        public void HandleStartTest()
        {
            string response;
            bool result = handler.InternalHandle(message, out response);
            Assert.IsTrue(result);
            Assert.That(response, Is.EqualTo("Ingrese el nombre del material a añadir. Ej: Pallet, cáscara, etc.\n")); 
            Assert.That(handler.State, Is.EqualTo(AddMaterialHandler.AddMaterialState.Name));
        }

        [Test]
        public void HandleNameTest()
        {
            string response;
            bool result = handler.InternalHandle(message, out response);
            message.Text = "Pallet";
            result = handler.InternalHandle(message, out response);
            Assert.IsTrue(result);
            Assert.That(response, Is.EqualTo("Ingrese el tipo:\n")); 
            Assert.That(handler.State, Is.EqualTo(AddMaterialHandler.AddMaterialState.Type));
        }

        [Test]
        public void HandleTypeTest()
        {
            string response;
            bool result = handler.InternalHandle(message, out response);
            message.Text = "Pallet";
            result = handler.InternalHandle(message, out response);
            message.Text = "Madera";
            result = handler.InternalHandle(message, out response);
            Assert.IsTrue(result);
            Assert.That(response, Is.EqualTo("Ingrese la clasificación:\n")); 
            Assert.That(handler.State, Is.EqualTo(AddMaterialHandler.AddMaterialState.Classification));
        }

        [Test]
        public void HandleClassificationTest()
        {
            string response;
            bool result = handler.InternalHandle(message, out response);
            message.Text = "Pallet";
            result = handler.InternalHandle(message, out response);
            message.Text = "Madera";
            result = handler.InternalHandle(message, out response);
            message.Text = "Residuo";
            result = handler.InternalHandle(message, out response);
            Assert.IsTrue(result);
            Assert.IsTrue(this.company.ProducedMaterials.Exists(material => material.Name == "Pallet" && material.Type == "Madera" && material.Classification == "Residuo"));
            Assert.That(response, Is.EqualTo("Se añadió el material")); 
            Assert.That(handler.State, Is.EqualTo(AddMaterialHandler.AddMaterialState.Start));
        }
    }

}