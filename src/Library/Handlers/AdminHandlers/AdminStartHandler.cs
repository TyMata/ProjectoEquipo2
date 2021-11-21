using System;
using System.Collections.Generic;
using System.Text;
namespace ClassLibrary
{
    /// <summary>
    /// Primer Handler de la CoR para los usuarios Admin.
    /// </summary>
    public class AdminStartHandler : AbstractHandler
    {
        /// <summary>
        /// Constructor de los objetos AdminStartHandler.
        /// </summary>
        public AdminStartHandler(IMessageChannel channel)
        {
            this.messageChannel = channel;
        }
        /// <summary>
        /// Le otorga por pantalla los comandos que puede utilizar el admin.
        /// </summary>
        /// <param name="input"></param>
        public override bool InternalHandle(IMessage input)
        {
            if (this.nextHandler != null)
            {
                StringBuilder bienvenida = new StringBuilder("Bienvenido Admin!\n")
                                                .Append("Que quieres hacer?\n")
                                                .Append("/RegistrarEmpresa\n")
                                                .Append("/EliminarUsuario\n")
                                                .Append("/EliminarEmpresa\n");
                this.messageChannel.SendMessage(bienvenida.ToString());
                IMessage input2 = this.messageChannel.ReceiveMessage();
                this.nextHandler.Handle(input2);
                return false;
            }
            return true;
        }
    }
}