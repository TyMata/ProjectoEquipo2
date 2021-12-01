using System;
using System.Text;
using ClassLibrary;
using NUnit.Framework;

namespace Tests
{
    /// <summary>
    /// Prueba de la clase <see cref="StartHandler"/>
    /// </summary>
    [TestFixture]
    public class StartHandlerTests
    {
        private StartHandler handler;
        private IMessage message;
         
        /// <summary>
        /// Se crea una instancia del handler a probar.
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            this.handler = new StartHandler();
        }

        /// <summary>
        /// Prueba que el InternalHandle se haga correctamente y cambie el estado del handler.
        /// </summary>
        [Test]
        public void HandleUnregisteredTest()
        {
            message = new TelegramBotMessage(12345, "/menu");
            this.handler.SetNext(new ModifyHabilitationsHandler());
            string response;
            bool result = this.handler.InternalHandle(message, out response);
            StringBuilder menu = new StringBuilder("Bienvenido\n");
                menu.Append("Usuario No Registrado:\n")
                        .Append("   /usuarioempresanoregistrado\n")
                        .Append("   /emprendedornoregistrado\n\n");
            Assert.IsTrue(result);
            Assert.That(response, Is.EqualTo(menu.ToString())); 
            Assert.That(handler.State, Is.EqualTo(StartHandler.StartState.NotFirstTime));
        }

        /// <summary>
        /// Prueba que el mensaje se procese y que se muestre el menu de comandos de el usuario empresa.
        /// </summary>
        [Test]
        public void HandleCompanyUserTest()
        {
            message = new TelegramBotMessage(12348, "/menu");
            handler.SetNext(new ModifyHabilitationsHandler());
            UserRegister.Instance.CreateCompanyUser(12348, new Company());
            string response;
            bool result = handler.InternalHandle(message, out response);
            StringBuilder menu = new StringBuilder("Bienvenido\n");
            menu.Append("Usuario de una Empresa:\n")
                        .Append("   /agregarmaterial\n")
                        .Append("   /publicaroferta\n")
                        .Append("   /retiraroferta\n")
                        .Append("   /suspenderoferta\n")
                        .Append("   /reanudaroferta\n")
                        .Append("   /mostrarofertas\n")
                        .Append("   /mostrarofertasvendidas\n\n")
                        .Append("   Para modificar alguna oferta:\n")
                        .Append("       /modificarhabilitaciones\n")
                        .Append("       /modificarprecio\n")
                        .Append("       /modificarcantidad\n\n");
            Assert.IsTrue(result);
            Assert.That(response, Is.EqualTo(menu.ToString())); 
            Assert.That(handler.State, Is.EqualTo(StartHandler.StartState.NotFirstTime));
        }

        /// <summary>
        /// Prueba que se procese el mensaje, que se muestre los comandos del usuario emprendedor y que se procese el mensaje.
        /// </summary>
        [Test]
        public void HandleEntrepreneurUserTest()
        {
            message = new TelegramBotMessage(1235, "/menu");
            handler.SetNext(new ModifyHabilitationsHandler());
            UserRegister.Instance.CreateEntrepreneurUser(1235,"091919191","Emprendedor", new LocationAdapter("Comandante Braga 2715", "Montevideo", "Montevideo"),"Rubro", "link");
            string response;
            bool result = handler.InternalHandle(message, out response);
            StringBuilder menu = new StringBuilder("Bienvenido\n");
            menu.Append("Usuario Emprendedor:\n")
                .Append("   /buscaroferta");
            Assert.IsTrue(result);
            Assert.That(response, Is.EqualTo(menu.ToString())); 
            Assert.That(this.handler.State, Is.EqualTo(StartHandler.StartState.NotFirstTime));
        }

        /// <summary>
        /// Prueba que el InternalHandle se haga correctamente y cambie el estado del handler.
        /// </summary>
        [Test]
        public void HandleUnregisteredNotFirstTimeTest()
        {
            message = new TelegramBotMessage(12345, "/menu");
            handler.SetNext(new ModifyHabilitationsHandler());
            string response;
            bool result = handler.InternalHandle(message, out response);
            this.message.Text = "/menu";
            result = handler.InternalHandle(message, out response);
            StringBuilder menu = new StringBuilder();
                menu.Append("Usuario No Registrado:\n")
                        .Append("   /usuarioempresanoregistrado\n")
                        .Append("   /emprendedornoregistrado\n\n");
            Assert.IsTrue(result);
            Assert.That(response, Is.EqualTo(menu.ToString())); 
            Assert.That(handler.State, Is.EqualTo(StartHandler.StartState.NotFirstTime));
        }

        /// <summary>
        /// Prueba que se procese el mensaje yq eu se muestra el menu de los comandos del usuario empresa.
        /// </summary>
        [Test]
        public void HandleCompanyNotFirstTimeTest()
        {
            message = new TelegramBotMessage(12346, "/menu");
            handler.SetNext(new ModifyHabilitationsHandler());
            UserRegister.Instance.CreateCompanyUser(12346, new Company());
            string response;
            bool result = handler.InternalHandle(message, out response);
            message.Text = "/menu";
            result = handler.InternalHandle(message, out response);
            StringBuilder menu = new StringBuilder();
            menu.Append("Usuario de una Empresa:\n")
                        .Append("   /agregarmaterial\n")
                        .Append("   /publicaroferta\n")
                        .Append("   /retiraroferta\n")
                        .Append("   /suspenderoferta\n")
                        .Append("   /reanudaroferta\n")
                        .Append("   /mostrarofertas\n")
                        .Append("   /mostrarofertasvendidas\n\n")
                        .Append("   Para modificar alguna oferta:\n")
                        .Append("       /modificarhabilitaciones\n")
                        .Append("       /modificarprecio\n")
                        .Append("       /modificarcantidad\n\n");
            Assert.IsTrue(result);
            Assert.That(response, Is.EqualTo(menu.ToString())); 
            Assert.That(handler.State, Is.EqualTo(StartHandler.StartState.NotFirstTime));
        }

        /// <summary>
        /// Prueba que se procese el mensaje y muestra el menu de los comando del usuario emprendedor.
        /// </summary>
        [Test]
        public void HandleEntrepreneurNotFirstTimeTest()
        {
            message = new TelegramBotMessage(12357, "/menu");
            handler.SetNext(new ModifyHabilitationsHandler());
            UserRegister.Instance.CreateEntrepreneurUser(12357,"091919191","Emprendedor", new LocationAdapter("Comandante Braga 2715", "Montevideo", "Montevideo"),"Rubro", "link");
            string response;
            bool result = handler.InternalHandle(message, out response);
            message.Text = "/menu";
            result = handler.InternalHandle(message, out response);
            StringBuilder menu = new StringBuilder();
            menu.Append("Usuario Emprendedor:\n")
                .Append("   /buscaroferta");
            Assert.IsTrue(result);
            Assert.That(response, Is.EqualTo(menu.ToString())); 
            Assert.That(handler.State, Is.EqualTo(StartHandler.StartState.NotFirstTime));
        }

        /// <summary>
        /// Prueba que se procese el mensaje y que muestre el menu de los comandos del usuario admin.
        /// </summary>
        [Test]
        public void HandleAdminNotFirstTimeTest()
        {
            message = new TelegramBotMessage(1234, "/menu");
            handler.SetNext(new ModifyHabilitationsHandler());
            Users user = new Users(1234, new AdminRole());
            UserRegister.Instance.DataUsers.Add(user);
            string response;
            bool result = handler.InternalHandle(message, out response);
            message.Text = "/menu";
            result = handler.InternalHandle(message, out response);
            StringBuilder menu = new StringBuilder();
            menu.Append("Usuario Admin:\n")
                        .Append("   /registrarempresa\n")
                        .Append("   /eliminarusuario\n")
                        .Append("   /eliminarempresa\n\n");
            Assert.IsTrue(result);
            Assert.That(response, Is.EqualTo(menu.ToString())); 
            Assert.That(handler.State, Is.EqualTo(StartHandler.StartState.NotFirstTime));
        }

        /// <summary>
        /// Prueba que no se procese el mensaje y que no se cambie el estado de este ya que no es el mensaje correcto.
        /// </summary>
        [Test]
        public void DoesNotHandleTest()
        {
            message = new TelegramBotMessage(1234, "/menu");
            string response;
            IHandler result = this.handler.Handle(new ConsoleMessage("/nada"), out response);
            Assert.IsNull(result);
            Assert.IsEmpty(response);
        }
    }
}