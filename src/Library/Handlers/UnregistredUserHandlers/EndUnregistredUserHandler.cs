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
            this.nextHandler = handler;
        }
        /// <summary>
        /// Le avisa al usuario que el comando no se reconocio y 
        /// va denuevo al primer handler de la cadena
        /// </summary>
        /// <param name="input"></param>
        public override bool InternalHandle(IMessage input)
        {
            this.messageChannel.SendMessage("No se reconocio el comando, intente denuevo");
            //Setear primer Handler de la cadena
            return false;
        }
    }
}