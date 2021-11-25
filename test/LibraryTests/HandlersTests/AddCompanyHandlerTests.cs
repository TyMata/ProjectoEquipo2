using System;
using NUnit.Framework;
using ClassLibrary;

namespace Tests
{
    /// <summary>
    /// Prueba el handler <see cref="AddCompanyHandler"/>
    /// </summary>
    [TestFixture]
    public class AddCompanyHandlerTests
    {
        private AddCompanyHandler handler;
        private IMessage message;

        /// <summary>
        /// Se crea el handler a probar y un IMessage.
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            handler = new AddCompanyHandler();
            message = new TelegramBotMessage(1234,"/registrarempresa");
        }

        /// <summary>
        /// Prueba que el InternalHandle se haga correctamente y cambie el estado del handler.
        /// </summary>
        [Test]
        public void HandleStartTest()
        {
            string response;
            bool result = handler.InternalHandle(message, out response);
            Assert.IsTrue(result);
            Assert.That(response, Is.EqualTo("Para poder registrar una empresa vamos a necesitar algunos datos de esta.\n\nIngrese el nombre de la empresa:\n"));
            Assert.That(handler.State, Is.EqualTo(AddCompanyHandler.AddCompanyState.Name));
        }

        /// <summary>
        /// Prueba que el InternalHandle se haga correctamente y cambie el estado del handler.
        /// </summary>
        [Test]
        public void HandleCompanyTest()
        { 
            string response;
            bool result = handler.InternalHandle(message, out response);
            message.Text = "nombre de la empresa";
            result = handler.InternalHandle(message, out response);
            Assert.IsTrue(result);
            Assert.That(response, Is.EqualTo("Ingrese el pais:\n"));
            Assert.That(handler.State, Is.EqualTo(AddCompanyHandler.AddCompanyState.Country));
        }

        /// <summary>
        /// Prueba que el InternalHandle se haga correctamente y cambie el estado del handler.
        /// </summary>
        [Test]
        public void HandleCountryTest()
        {
            string response;
            bool result = handler.InternalHandle(message, out response);
            message.Text = "nombre de la empresa";
            result = handler.InternalHandle(message, out response);
            message.Text = "Uruguay";
            result = handler.InternalHandle(message, out response);
            Assert.IsTrue(result);
            Assert.That(response, Is.EqualTo("Ingrese el departamento:\n"));
            Assert.That(handler.State, Is.EqualTo(AddCompanyHandler.AddCompanyState.State));
        }

        /// <summary>
        /// Prueba que el InternalHandle se haga correctamente y cambie el estado del handler.
        /// </summary>
        [Test]
        public void HandleDepartmentTest()
        {
            string response;
            bool result = handler.InternalHandle(message, out response);
            message.Text = "nombre de la empresa";
            result = handler.InternalHandle(message, out response);
            message.Text = "Uruguay";
            result = handler.InternalHandle(message, out response);
            message.Text = "Montevideo";
            result = handler.InternalHandle(message, out response);
            Assert.IsTrue(result);
            Assert.That(response, Is.EqualTo("Ingrese la ciudad:\n"));
            Assert.That(handler.State, Is.EqualTo(AddCompanyHandler.AddCompanyState.City));
        }

        /// <summary>
        /// Prueba que el InternalHandle se haga correctamente y cambie el estado del handler.
        /// </summary>
        [Test]
        public void HandleAddressTest()
        {
            string response;
            bool result = handler.InternalHandle(message, out response);
            message.Text = "nombre de la empresa";
            result = handler.InternalHandle(message, out response);
            message.Text = "Uruguay";
            result = handler.InternalHandle(message, out response);
            message.Text = "Montevideo";
            result = handler.InternalHandle(message, out response);
            message.Text = "Montevideo";
            result = handler.InternalHandle(message, out response);
            Assert.IsTrue(result);
            Assert.That(response, Is.EqualTo("Ingrese la direccion:\n"));
            Assert.That(handler.State, Is.EqualTo(AddCompanyHandler.AddCompanyState.Address));
        }

        /// <summary>
        /// Prueba que el InternalHandle se hag correctamente y cambie el estado del handler.
        /// </summary>
        [Test]
        public void HandleHeadingsTest()
        {
            string response;
            bool result = handler.InternalHandle(message, out response);
            message.Text = "nombre de la empresa";
            result = handler.InternalHandle(message, out response);
            message.Text = "Uruguay";
            result = handler.InternalHandle(message, out response);
            message.Text = "Montevideo";
            result = handler.InternalHandle(message, out response);
            message.Text = "Montevideo";
            result = handler.InternalHandle(message, out response);
            message.Text = "Av. 8 de Octubre 2738";
            result = handler.InternalHandle(message, out response);
            Assert.IsTrue(result);
            Assert.That(response, Is.EqualTo("Ingrese su rubro:\n"));
            Assert.That(handler.State, Is.EqualTo(AddCompanyHandler.AddCompanyState.Headings));
        }

        /// <summary>
        /// Prueba que el InternalHandle se hag correctamente, cambie el estado del handler y que se haya creado la empresa.
        /// </summary>
        [Test]
        public void HandleFinalTest()
        {
            string response;
            bool result = handler.InternalHandle(message, out response);
            message.Text = "nombre de la empresa";
            result = handler.InternalHandle(message, out response);
            message.Text = "Uruguay";
            result = handler.InternalHandle(message, out response);
            message.Text = "Montevideo";
            result = handler.InternalHandle(message, out response);
            message.Text = "Montevideo";
            result = handler.InternalHandle(message, out response);
            message.Text = "Av. 8 de Octubre 2738";
            result = handler.InternalHandle(message, out response);
            message.Text = "rubro de la empresa";
            result = handler.InternalHandle(message, out response);
            Assert.IsTrue(result);
            Assert.IsTrue(CompanyRegister.Instance.Contains(handler.Data.company));
            Assert.That(response, Is.EqualTo($"Ya se creo la empresa. El token de invitacion es {handler.Data.company.InvitationToken}"));
            Assert.That(handler.State, Is.EqualTo(AddCompanyHandler.AddCompanyState.Start));
        }

        /// <summary>
        /// Prueba que el InternalHandle no se haga ya que no se envia el mensaje correcto y qu eno cambie el estado del handler.
        /// </summary>
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