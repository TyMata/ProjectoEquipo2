using System;
using System.Collections.Generic;
using System.Text;
namespace ClassLibrary
{
    /// <summary>
    /// Marcamos el formato del resto de handlers
    /// </summary>
    public class UnregisteredUserHandler : AbstractHandler
    {
        /// <summary>
        /// Handler para los usuarios no registrados.
        /// </summary>
        public UnregisteredUserHandler(IMessageChannel channel)
        {
            this.messageChannel = channel;
            // this.nextHandler2 = new UnregisteredEntrepeneurUserHandler(this.messageChannel);
            // this.nextHandler2.SetNext(new UnregisteredCompanyUserHandler(this.messageChannel));
        }
        /// <summary>
        /// Verifica si el usuario que emite el mensaje esta registrado
        /// y de no ser asi lo ayuda a registrarse
        /// </summary>
        /// <param name="input"></param>
        public override void Handle(IMessage input)
        {
            if (this.nextHandler != null)
            {
                StringBuilder bienvenida = new StringBuilder("Bienvenido a el bot del Equipo 2\n")
                                                    .Append("Por lo que veo no estas registrado\n")
                                                    .Append("¿Eres usuario de una empresa o eres emprendedor?\n");
                this.messageChannel.SendMessage(bienvenida.ToString());
                this.nextHandler.Handle(this.messageChannel.ReceiveMessage());
            }
            else
            {
                this.nextHandler.Handle(input); // ?????????????
            }
        }
    }
}