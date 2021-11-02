using System;
using System.Collections.Generic;
using System.Text;
namespace ClassLibrary
{
    /// <summary>
    /// Marcamos el formato del resto de handlers
    /// </summary>
    public class EndHandler : AbstractHandler
    {
        
        /// <summary>
        /// Handler para los usuarios no registrados.
        /// </summary>
        public EndHandler(IMessageChannel channel, IHandler handler)
        {
            this.messageChannel = channel;
        }
        /*private IHandler nextHandler;*/
        /// <summary>
        /// Verifica si el usuario que emite el mensaje esta registrado
        /// y de no ser asi lo ayuda a registrarse
        /// </summary>
        /// <param name="input"></param>
        public override void Handle(IMessage input)
        {
            this.messageChannel.SendMessage("No se reconocio el comando, intente denuevo");
            this.nextHandler.Handle(input); //Setear primer Handler de la cadena
        }
    }
}