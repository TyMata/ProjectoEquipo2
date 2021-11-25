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
            message = new TelegramBotMessage(1234,"/eliminarempresa");
            company = CompanyRegister.Instance.CreateCompany("company", new LocationAdapter("address","city","departemnt"),"Headings");
            handler = new RemoveCompanyHandler();
        }

        [Test]
        public void HandleStartTest()
        {           
            string response;
            bool result = handler.InternalHandle(message, out response);
            Assert.IsTrue(result);
            Assert.That(response, Is.EqualTo("¿Cual es el Id de la empresa que quieres eliminar?"));
            Assert.That(handler.State, Is.EqualTo(RemoveCompanyHandler.RemoveCompanyState.Company));
        }

        [Test]
        public void HandleCompanyTest()
        {
            string response;
            bool result = handler.InternalHandle(message, out response);
            message.Text = company.Id.ToString();
            result = handler.InternalHandle(message, out response);
            Assert.IsTrue(result);
            Assert.That(response, Is.EqualTo($"La empresa {company.Name} ha sido eliminada"));
            Assert.That(handler.State, Is.EqualTo(RemoveCompanyHandler.RemoveCompanyState.Start));
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