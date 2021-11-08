using System;
using System.Collections.Generic;
using System.Text;
namespace ClassLibrary
{
    /// <summary>
    /// Handler encargado de darle final a la CoR
    /// </summary>
    public class EndHandler : AbstractHandler
    {
        
        /// <summary>
        /// Constructor de objetos EndHandler
        /// </summary>
        public EndHandler(IMessageChannel channel, IHandler handler)
        {
            this.messageChannel = channel;
        }
        /// <summary>
        /// Le avisa al usuario que el comando no se reconocio y 
        /// va denuevo al primer handler de la cadena
        /// </summary>
        /// <param name="input"></param>
        public override void Handle(IMessage input)
        {
            this.messageChannel.SendMessage("No se reconocio el comando, intente denuevo");
            this.nextHandler.Handle(input); //Setear primer Handler de la cadena
        }
    }
}