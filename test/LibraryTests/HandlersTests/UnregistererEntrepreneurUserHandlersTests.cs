using System;
using System.Text;
using ClassLibrary;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    /// <summary>
    /// Prueba el handler <see cref="UnregisteredEntrepreneurUserHandler"/>
    /// </summary>
    public class UnregisteredEntrepreneurUserHandlerTests //TODO Object reference not set to an instance of an object. ver cual es el error este
    {
        private UnregisteredEntrepreneurUserHandler handler;
        private LocationAdapter location;
        private IMessage message;
        private Company company;
        /// <summary>
        /// Se setea el usuario Entrepreneur.
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            message = new TelegramBotMessage(1234, "/emprendedornoregistrado");
            location = new LocationAdapter("address", "city", "department");
            handler = new UnregisteredEntrepreneurUserHandler();
        }

        [Test]
        public void HandleStartTest()
        {
            string response;
            bool result = handler.InternalHandle(message, out response);
            StringBuilder datos = new StringBuilder("Asi que eres un Emprendedor!\n")
                                                .Append("Para poder registrarte vamos a necesitar algunos datos personales\n")
                                                .Append("Ingrese su nombre completo\n");
            Assert.IsTrue(result);
            Assert.That(response, Is.EqualTo(datos.ToString())); 
            Assert.That(handler.State, Is.EqualTo(UnregisteredEntrepreneurUserHandler.UnregisteredEntrepreneurUserState.Name));
        }

        [Test]
        public void HandleTokenTest()
        {
            string response;
            bool result = handler.InternalHandle(message, out response);
            message.Text = company.InvitationToken;
            result = handler.InternalHandle(message, out response);
            message.Text = "Nombre del emprendedor";
            result = handler.InternalHandle(message, out response);
            Assert.IsTrue(result);
            Assert.That(response, Is.EqualTo("Ingrese su dirección:\n")); 
            Assert.That(handler.State, Is.EqualTo(UnregisteredEntrepreneurUserHandler.UnregisteredEntrepreneurUserState.Address));
        }

         [Test]
        public void HandleAddressTest()
        {
            string response;
            bool result = handler.InternalHandle(message, out response);
            message.Text = company.InvitationToken;
            result = handler.InternalHandle(message, out response);
            message.Text = "Nombre del emprendedor";
            result = handler.InternalHandle(message, out response);
            message.Text = "Av. 8 de Octubre 2738";
            result = handler.InternalHandle(message, out response);
            Assert.IsTrue(result);
            Assert.That(response, Is.EqualTo("Ingrese la ciudad:\n")); 
            Assert.That(handler.State, Is.EqualTo(UnregisteredEntrepreneurUserHandler.UnregisteredEntrepreneurUserState.City));
        }
        [Test]
        public void HandleCityTest()
        {
            string response;
            bool result = handler.InternalHandle(message, out response);
            message.Text = company.InvitationToken;
            result = handler.InternalHandle(message, out response);
            message.Text = "Nombre del emprendedor";
            result = handler.InternalHandle(message, out response);
            message.Text = "Av. 8 de Octubre 2738";
            result = handler.InternalHandle(message, out response);
            message.Text = "Montevideo";
            result = handler.InternalHandle(message, out response);
            Assert.IsTrue(result);
            Assert.That(response, Is.EqualTo("Ingrese el departamento:\n")); 
            Assert.That(handler.State, Is.EqualTo(UnregisteredEntrepreneurUserHandler.UnregisteredEntrepreneurUserState.Department));
        }
         [Test]
        public void HandleDepartmentTest()
        {
            string response;
            bool result = handler.InternalHandle(message, out response);
            message.Text = company.InvitationToken;
            result = handler.InternalHandle(message, out response);
            message.Text = "Nombre del emprendedor";
            result = handler.InternalHandle(message, out response);
            message.Text = "Av. 8 de Octubre 2738";
            result = handler.InternalHandle(message, out response);
            message.Text = "Montevideo";
            result = handler.InternalHandle(message, out response);
            message.Text = "Montevideo";
            result = handler.InternalHandle(message, out response);
            Assert.IsTrue(result);
            Assert.That(response, Is.EqualTo( "Ingrese sus habilitaciones\n")); 
            Assert.That(handler.State, Is.EqualTo(UnregisteredEntrepreneurUserHandler.UnregisteredEntrepreneurUserState.Habilitations));
        }

        public void HandleHabilitationsTest()
        {
            string response;
            bool result = handler.InternalHandle(message, out response);
            message.Text = company.InvitationToken;
            result = handler.InternalHandle(message, out response);
            message.Text = "Nombre del emprendedor";
            result = handler.InternalHandle(message, out response);
            message.Text = "Av. 8 de Octubre 2738";
            result = handler.InternalHandle(message, out response);
            message.Text = "Montevideo";
            result = handler.InternalHandle(message, out response);
            message.Text = "Montevideo";
            result = handler.InternalHandle(message, out response);
            message.Text = "Link";
            result = handler.InternalHandle(message, out response);
            Assert.IsTrue(result);
            Assert.That(response, Is.EqualTo("Ingrese su rubro\n")); 
            Assert.That(handler.State, Is.EqualTo(UnregisteredEntrepreneurUserHandler.UnregisteredEntrepreneurUserState.Headings));
        }

        
        public void HandleHeadingsTest()
        {
            string response;
            bool result = handler.InternalHandle(message, out response);
            message.Text = company.InvitationToken;
            result = handler.InternalHandle(message, out response);
            message.Text = "Nombre del emprendedor";
            result = handler.InternalHandle(message, out response);
            message.Text = "Av. 8 de Octubre 2738";
            result = handler.InternalHandle(message, out response);
            message.Text = "Montevideo";
            result = handler.InternalHandle(message, out response);
            message.Text = "Montevideo";
            result = handler.InternalHandle(message, out response);
            message.Text = "Link";
            result = handler.InternalHandle(message, out response);
            message.Text = "Rubro";
            result = handler.InternalHandle(message, out response);
            Assert.IsTrue(result);
            Assert.That(response, Is.EqualTo("Gracias por sus datos, se esta creando su usuario\n")); 
            Assert.IsTrue(company.CompanyUsers.Contains(UserRegister.Instance.GetUserById(1234)));
            Assert.That(handler.State, Is.EqualTo(UnregisteredEntrepreneurUserHandler.UnregisteredEntrepreneurUserState.Start));
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