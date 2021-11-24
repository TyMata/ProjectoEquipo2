using System;
using NUnit.Framework;
using ClassLibrary;

namespace Tests
{
    [TestFixture]
    public class AddCompanyHandlerTests
    {
        private AddCompanyHandler handler;
        private IMessage message;

        [SetUp]
        public void SetUp()
        {
            handler = new AddCompanyHandler();
            message = new TelegramBotMessage(1234,"/registrarempresa");
        }

        [Test]
        public void HandleStartTest()
        {
            string response;
            bool result = handler.InternalHandle(message, out response);
            Assert.IsTrue(result);
            Assert.That(response, Is.EqualTo("Para poder registrar una empresa vamos a necesitar algunos datos de esta.\n\nIngrese el nombre de la empresa:\n"));
            Assert.That(handler.State, Is.EqualTo(AddCompanyHandler.AddCompanyState.Name));
        }

        [Test]
        public void HandleCompanyTest()
        { 
            string response;
            bool result = handler.InternalHandle(message, out response);
            message.Text = "nombre de la empresa";
            handler.State = AddCompanyHandler.AddCompanyState.Name;
            result = handler.InternalHandle(message, out response);
            Assert.IsTrue(result);
            Assert.That(response, Is.EqualTo("Ingrese el pais:\n"));
            Assert.That(handler.State, Is.EqualTo(AddCompanyHandler.AddCompanyState.Country));
        }

        [Test]
        public void HandleCountryTest()
        {
            string response;
            bool result = handler.InternalHandle(message, out response);
            message.Text = "nombre de la empresa";
            handler.State = AddCompanyHandler.AddCompanyState.Name;
            result = handler.InternalHandle(message, out response);
            message.Text = "Uruguay";
            handler.State = AddCompanyHandler.AddCompanyState.Country;
            result = handler.InternalHandle(message, out response);
            Assert.IsTrue(result);
            Assert.That(response, Is.EqualTo("Ingrese el departamento:\n"));
            Assert.That(handler.State, Is.EqualTo(AddCompanyHandler.AddCompanyState.State));

        }

        [Test]
        public void DoesNotHandle()
        {
            message = new TelegramBotMessage(1234,"/registremprendedor");
            string response;
            bool result = handler.InternalHandle(message, out response);
            Assert.IsFalse(result);
            Assert.IsEmpty(response);
            Assert.That(handler.State, Is.EqualTo(AddCompanyHandler.AddCompanyState.Start));
        }
    }
}