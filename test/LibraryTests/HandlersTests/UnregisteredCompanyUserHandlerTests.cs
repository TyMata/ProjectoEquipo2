using System;
using System.Text;
using ClassLibrary;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class UnregisteredCompanyUserHandlerTests
    {
        private UnregisteredCompanyUserHandler handler;
        private LocationAdapter location;
        private IMessage message;
        private Company company;

        [SetUp]
        public void SetUP()
        {
            message = new TelegramBotMessage(1234, "/empresanoregistrada");
            location = new LocationAdapter("address", "city", "department");
            company =  CompanyRegister.Instance.CreateCompany("Nombre de la empresa", location, "headings", "company@gmail.com", "091919191");
            handler = new UnregisteredCompanyUserHandler();
        }

        [Test]
        public void HandleStartTest()
        {
            string response;
            bool result = handler.InternalHandle(message, out response);
            StringBuilder datos = new StringBuilder("Asi que eres usuario de una empresa!\n")
                                                .Append("Para poder registrarte vamos a necesitar el codigo de invitacion\n")
                                                .Append("Ingrese el codigo de invitacion\n");
            Assert.IsTrue(result);
            Assert.That(response, Is.EqualTo(datos.ToString())); 
            Assert.That(handler.State, Is.EqualTo(UnregisteredCompanyUserHandler.UnregisteredCompanyUserState.Token));
        }

        [Test]
        public void HandleTokenTest()
        {
            string response;
            bool result = handler.InternalHandle(message, out response);
            message.Text = company.InvitationToken;
            result = handler.InternalHandle(message, out response);
            Assert.IsTrue(result);
            Assert.IsTrue(company.CompanyUsers.Contains(UserRegister.Instance.GetUserById(1234)));
            Assert.That(response, Is.EqualTo( $"Se verifico el Token ingresado y se esta creando su usuario perteneciente a la empresa {company.Name}")); 
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