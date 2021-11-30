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
            message = new TelegramBotMessage(1234, "/menu");
            this.handler = new StartHandler();
        }
        /// <summary>
        /// Prueba que el InternalHandle se haga correctamente y cambie el estado del handler.
        /// </summary>
        [Test]
        public void HandleStartTest()
        {
            this.handler.SetNext(new EndHandler(null));
            string response;
            bool result = this.handler.InternalHandle(message, out response);
            StringBuilder menu = new StringBuilder("Bienvenido\n");
                menu.Append("Usuario No Registrado:\n")
                    .Append("   /usuarioempresanoregistrado\n")
                    .Append("   /emprendedornoregistrado\n\n")
                    .Append("Usuario Admin:\n")
                    .Append("   /registrarempresa\n")
                    .Append("   /eliminarusuario\n")
                    .Append("   /eliminarempresa\n\n")
                    .Append("Usuario de una Empresa:\n")
                    .Append("   /agregarmaterial\n")
                    .Append("   /publicaroferta\n")
                    .Append("   /retiraroferta\n")
                    .Append("   /suspenderoferta\n")
                    .Append("   /reanudaroferta\n")
                    .Append("   /mostrarofertas\n\n")
                    .Append("   Para modificar alguna oferta:\n")
                    .Append("       /modificarhabilitaciones\n")
                    .Append("       /modificarprecio\n")
                    .Append("       /modificarcantidad\n\n")
                    .Append("Usuario Emprendedor:\n")
                    .Append("   /buscaroferta");
            Assert.IsTrue(result);
            Assert.That(response, Is.EqualTo(menu.ToString())); 
            Assert.That(this.handler.State, Is.EqualTo(StartHandler.StartState.NotFirstTime));
        }
        /// <summary>
        /// Prueba que el InternalHandle se haga correctamente y cambie el estado del handler.
        /// </summary>
        [Test]
        public void HandleNotFirstTime()
        {
            string response;
            bool result = this.handler.InternalHandle(this.message, out response);
            this.message.Text = "/menu";
            result = this.handler.InternalHandle(message, out response);
            StringBuilder menu = new StringBuilder("Bienvenido\n");
                menu.Append("Usuario No Registrado:\n")
                    .Append("   /usuarioempresanoregistrado\n")
                    .Append("   /emprendedornoregistrado\n\n")
                    .Append("Usuario Admin:\n")
                    .Append("   /registrarempresa\n")
                    .Append("   /eliminarusuario\n")
                    .Append("   /eliminarempresa\n\n")
                    .Append("Usuario de una Empresa:\n")
                    .Append("   /agregarmaterial\n")
                    .Append("   /publicaroferta\n")
                    .Append("   /retiraroferta\n")
                    .Append("   /suspenderoferta\n")
                    .Append("   /reanudaroferta\n")
                    .Append("   /mostrarofertas\n\n")
                    .Append("   Para modificar alguna oferta:\n")
                    .Append("       /modificarhabilitaciones\n")
                    .Append("       /modificarprecio\n")
                    .Append("       /modificarcantidad\n\n")
                    .Append("Usuario Emprendedor:\n")
                    .Append("   /buscaroferta");
            Assert.IsTrue(result);
            Assert.That(response, Is.EqualTo(menu.ToString())); 
        }

        [Test]
        public void DoesNotHandleTest()
        {
            string response;
            IHandler result = this.handler.Handle(new ConsoleMessage("/nada"), out response);
            Assert.IsNull(result);
            Assert.IsEmpty(response);
        }
    }
}