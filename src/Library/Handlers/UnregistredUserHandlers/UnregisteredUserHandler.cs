using System;
using System.Collections.Generic;
using System.Text;
namespace ClassLibrary
{
    /// <summary>
    /// HAndler encargado de darle la bienvenida a un usuario no registrado.
    /// </summary>
    public class UnregisteredUserHandler : AbstractHandler
    {
        /// <summary>
        /// Constructor de objetos UnregistredUserHandler.
        /// </summary>
        public UnregisteredUserHandler(IMessageChannel channel)
        {
            this.messageChannel = channel;
        }
        /// <summary>
        /// Se encarga de darle la bienvenida al usuario no registrado y preguntarle
        /// si es un emprendedor o ubn usuario empresa
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