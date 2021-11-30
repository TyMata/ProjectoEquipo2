using System;
using System.Text;
using ClassLibrary;
using NUnit.Framework;

namespace Tests
{
    /// <summary>
    /// /// Prueba el handler para registrar un company user.
    /// </summary>
    [TestFixture]
    public class UnregisteredCompanyUserHandlerTests
    {
        private UnregisteredCompanyUserHandler handler;
        private LocationAdapter location;
        private IMessage message;
        private Company company;
        /// <summary>
        /// Se setea el company user.
        /// </summary>
        [SetUp]
        public void SetUP()
        {
            message = new TelegramBotMessage(1234, "/usuarioempresanoregistrado");
            location = new LocationAdapter("Comandante Braga 2715", "Montevideo", "Montevideo");
            company =  CompanyRegister.Instance.CreateCompany("Nombre de la empresa", location, "headings", "company@gmail.com", "091919191");
            handler = new UnregisteredCompanyUserHandler();
        }
        /// <summary>
        /// /// Prueba que el InternalHandle se haga correctamente y cambie el estado del handler.
        /// </summary>
        [Test]
        public void HandleStartTest()
        {
            string response;
            bool result = handler.InternalHandle(message, out response);
            StringBuilder datos = new StringBuilder("¡Así que eres usuario de una empresa!\n")
                                                .Append("Para poder registrarte vamos a necesitar el código de invitación.\n")
                                                .Append("Ingrese el código de invitación.");
            Assert.IsTrue(result);
            Assert.That(response, Is.EqualTo(datos.ToString())); 
            Assert.That(handler.State, Is.EqualTo(UnregisteredCompanyUserHandler.UnregisteredCompanyUserState.Token));
        }
        /// <summary>
        /// Prueba que el InternalHandle se haga correctamente y cambie el estado del handler.
        /// </summary>
        [Test]
        public void HandleTokenTest()
        {
            string response;
            bool result = handler.InternalHandle(message, out response);
            message.Text = company.InvitationToken;
            result = handler.InternalHandle(message, out response);
            Assert.IsTrue(result);
            Assert.IsTrue(company.CompanyUsers.Contains(UserRegister.Instance.GetUserById(1234)));
            Assert.That(response, Is.EqualTo($"Se verificó el Token ingresado y se ha creado su usuario perteneciente a la empresa {handler.Data.Unregistered.Name}.\nIngrese /menu para ver los comandos nuevamente.")); 
            Assert.That(handler.State, Is.EqualTo(UnregisteredCompanyUserHandler.UnregisteredCompanyUserState.Start));
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