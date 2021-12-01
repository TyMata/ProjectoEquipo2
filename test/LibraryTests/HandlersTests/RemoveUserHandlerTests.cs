using System;
using NUnit.Framework;
using ClassLibrary;

namespace Tests
{   
    /// <summary>
    /// Se prueba el handler para remover una company.
    /// </summary>
    [TestFixture]
    public class RemoveUserHandlerTests
    {
        private IMessage message;
        private RemoveUserHandler handler;
        private Company company;
        private LocationAdapter location;

        /// <summary>
        /// Se crea una company y un user, el cual se va a eliminar.
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            message = new TelegramBotMessage(1234,"/eliminarusuario");
            handler = new RemoveUserHandler();
            location = new LocationAdapter("Comandante Braga 2715", "Montevideo", "Montevideo");
            company =  CompanyRegister.Instance.CreateCompany("Nombre de la empresa", location, "headings", "company@gmail.com", "091919191");
            company.AddUser(1234);
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
            Assert.That(response, Is.EqualTo("¿Cuál es el Id del usuario que quieres eliminar?"));
            Assert.That(handler.State, Is.EqualTo(RemoveUserHandler.RemoveUserState.User));
        }
        /// <summary>
        /// Prueba que el InternalHandle se haga correctamente y cambie el estado del handler.
        /// Borrando a la company que se deseaba eliminar.
        /// </summary>
        [Test]
        public void HandleCompanyTest()
        {
            string response;
            bool result = this.handler.InternalHandle(message, out response);
            this.message.Text = "1234";
            result = this.handler.InternalHandle(message, out response);
            Assert.IsTrue(result);
            Assert.That(response, Is.EqualTo($"El usuario de Id \"{this.handler.Data.UserId}\" ha sido eliminado"));
            Assert.That(this.handler.State, Is.EqualTo(RemoveUserHandler.RemoveUserState.Start));
        }
        /// <summary>
        /// Prueba que no se realice el handler.
        /// </summary>
        [Test]
        public void DoesNotHandleTest()
        {
            message = new TelegramBotMessage(1234,"/eliminarempresa");
            string response;
            bool result = handler.InternalHandle(message, out response);
            Assert.IsFalse(result);
            Assert.IsEmpty(response);
            Assert.That(handler.State, Is.EqualTo(RemoveUserHandler.RemoveUserState.Start));
        }

    }
}