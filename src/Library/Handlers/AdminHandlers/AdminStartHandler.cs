using System;
using System.Collections.Generic;
using System.Text;
namespace ClassLibrary
{
    /// <summary>
    /// Marcamos el formato del resto de handlers
    /// </summary>
    public class AdminStartHandler : AbstractHandler
    {
        
        /// <summary>
        /// Handler para los usuarios no registrados.
        /// </summary>
        public AdminStartHandler(IMessageChannel channel)
        {
            this.messageChannel = channel;
        }
        /// <summary>
        /// Verifica si el usuario que emite el mensaje esta registrado
        /// y de no ser asi lo ayuda a registrarse
        /// </summary>
        /// <param name="input"></param>
        public override void Handle(IMessage input)
        {
            if (this.nextHandler != null && (CanHandle(input)) )
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