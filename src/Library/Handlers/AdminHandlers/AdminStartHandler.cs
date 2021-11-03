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
        public override void Handle(IMessage input)
        {
            if (this.nextHandler != null)
            {
                StringBuilder bienvenida = new StringBuilder("Bienvenido Admin!\n")
                                                .Append("Que quieres hacer?\n")
                                                .Append("/UnirEmpresa\n")
                                                .Append("/GenerarTokenDeInvitacion\n")
                                                .Append("/EliminarUsuario\n")
                                                .Append("/EliminarEmpresa\n");
                this.messageChannel.SendMessage(bienvenida.ToString());
                this.nextHandler.Handle(this.messageChannel.ReceiveMessage());
            }
            else
            {
                this.nextHandler.Handle(input);
            }
            
        }
        
    }
}