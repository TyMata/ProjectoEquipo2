using System;
using System.Text;
using ClassLibrary;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class StartHandlerTests
    {
        private StartHandler handler;
        private IMessage message;

        [SetUp]
        public void SetUp()
        {
            handler = new StartHandler();
        }

        [Test]
        public void HandleUnregisteredTest()
        {
            message = new TelegramBotMessage(12345, "/menu");
            handler.SetNext(new EndHandler(null));
            string response;
            bool result = handler.InternalHandle(message, out response);
            StringBuilder menu = new StringBuilder("Bienvenido\n");
                menu.Append("Usuario No Registrado:\n")
                        .Append("   /usuarioempresanoregistrado\n")
                        .Append("   /emprendedornoregistrado\n\n");
            Assert.IsTrue(result);
            Assert.That(response, Is.EqualTo(menu.ToString())); 
            Assert.That(handler.State, Is.EqualTo(StartHandler.StartState.NotFirstTime));
        }

        [Test]
        public void HandleCompanyUserTest()
        {
            message = new TelegramBotMessage(12348, "/menu");
            handler.SetNext(new EndHandler(null));
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

        [Test]
        public void HandleEntrepreneurUserTest()
        {
            message = new TelegramBotMessage(1235, "/menu");
            handler.SetNext(new EndHandler(null));
            UserRegister.Instance.CreateEntrepreneurUser(1235,"091919191","Emprendedor", new LocationAdapter("Comandante Braga 2715", "Montevideo", "Montevideo"),"Rubro", "link");
            string response;
            bool result = handler.InternalHandle(message, out response);
            StringBuilder menu = new StringBuilder("Bienvenido\n");
            menu.Append("Usuario Emprendedor:\n")
                .Append("   /buscaroferta");
            Assert.IsTrue(result);
            Assert.That(response, Is.EqualTo(menu.ToString())); 
            Assert.That(handler.State, Is.EqualTo(StartHandler.StartState.NotFirstTime));
        }

        [Test]
        public void HandleUnregisteredNotFirstTimeTest()
        {
            message = new TelegramBotMessage(12345, "/menu");
            handler.SetNext(new EndHandler(null));
            string response;
            bool result = handler.InternalHandle(message, out response);
            message.Text = "/menu";
            result = handler.InternalHandle(message, out response);
            StringBuilder menu = new StringBuilder();
                menu.Append("Usuario No Registrado:\n")
                        .Append("   /usuarioempresanoregistrado\n")
                        .Append("   /emprendedornoregistrado\n\n");
            Assert.IsTrue(result);
            Assert.That(response, Is.EqualTo(menu.ToString())); 
            Assert.That(handler.State, Is.EqualTo(StartHandler.StartState.NotFirstTime));
        }

        [Test]
        public void HandleCompanyNotFirstTimeTest()
        {
            message = new TelegramBotMessage(12346, "/menu");
            handler.SetNext(new EndHandler(null));
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

        [Test]
        public void HandleEntrepreneurNotFirstTimeTest()
        {
            message = new TelegramBotMessage(12357, "/menu");
            handler.SetNext(new EndHandler(null));
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

        [Test]
        public void HandleAdminNotFirstTimeTest()
        {
            message = new TelegramBotMessage(1234, "/menu");
            handler.SetNext(new EndHandler(null));
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

        [Test]
        public void DoesNotHandleTest()
        {
            message = new TelegramBotMessage(1234, "/menu");
            string response;
            IHandler result = handler.Handle(new ConsoleMessage("/nada"), out response);
            Assert.IsNull(result);
            Assert.IsEmpty(response);
        }
    }
}