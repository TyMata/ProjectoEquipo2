using System;
using NUnit.Framework;
using ClassLibrary;

namespace Tests
{   
    /// <summary>
    /// Se prueba el handler para remover una company.
    /// </summary>
    [TestFixture]
    public class RemoveCompanyHandlerTests
    {
        private IMessage message;
        private Company company;
        private RemoveCompanyHandler handler;

        /// <summary>
        /// Se crean Instancias par apoder realizar el caso de prueba para poder remover una empresa.
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            message = new TelegramBotMessage(1234,"/eliminarempresa");
            company = CompanyRegister.Instance.CreateCompany("Empresa", new LocationAdapter("Comandante Braga 2715", "Montevideo", "Montevideo"),"Headings", "company@gmail.com", "091919191");
            handler = new RemoveCompanyHandler();
        }
        /// <summary>
        ///  Prueba que el InternalHandle se haga correctamente y cambie el estado del handler..
        /// </summary>
        [Test]
        public void HandleStartTest()
        {           
            string response;
            bool result = handler.InternalHandle(message, out response);
            Assert.IsTrue(result);
            Assert.That(response, Is.EqualTo("¿Cuál es el nombre de la empresa que quieres eliminar?"));
            Assert.That(handler.State, Is.EqualTo(RemoveCompanyHandler.RemoveCompanyState.Company));
        }
        /// <summary>
        /// Prueba que el InternalHandle se haga correctamente y cambie el estado del handler.
        /// Borrando a la company que se deseaba eliminar.
        /// </summary>
        [Test]
        public void HandleCompanyTest()
        {
            string response;
            bool result = handler.InternalHandle(message, out response);
            message.Text = company.Name;
            result = handler.InternalHandle(message, out response);
            Assert.IsTrue(result);
            Assert.That(response, Is.EqualTo($"La empresa {handler.Data.CompanyName} ha sido eliminada"));
            Assert.That(handler.State, Is.EqualTo(RemoveCompanyHandler.RemoveCompanyState.Start));
        }
        /// <summary>
        /// Prueba que no se realice el handler.
        /// </summary>
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