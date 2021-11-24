using System;
using NUnit.Framework;
using ClassLibrary;

namespace Tests
{
    [TestFixture]
    public class RemoveCompanyHandlerTests
    {
        private IMessage message;
        private Company company;
        private RemoveCompanyHandler handler;

        [SetUp]
        public void SetUp()
        {
            company = new Company("company",new LocationAdapter("address","city","departemnt"),"seadings");
            handler = new RemoveCompanyHandler();
        }

        [Test]
        public void HandleTest()
        {           
            message = new TelegramBotMessage(1234,"/eliminarempresa");
            string response;
            bool result = handler.InternalHandle(message, out response);
            Assert.IsTrue(result);
            Assert.That(response, Is.EqualTo("¿Cual es el Id de la empresa que quieres eliminar?"));
            Assert.That(handler.State, Is.EqualTo(RemoveCompanyHandler.RemoveCompanyState.Company));
        }

        [Test]
        public void DoesNotHandleTest()
        {
            message = new TelegramBotMessage(1234,"/eliminaremprendedor");
            string response;
            bool result = handler.InternalHandle(message, out response);
            Assert.IsFalse(result);
            Assert.IsEmpty(response);
            Assert.That(handler.State, Is.EqualTo(RemoveCompanyHandler.RemoveCompanyState.Start));
        }

    }
}