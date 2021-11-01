using System;
using System.Collections.Generic;
using System.Linq;
namespace ClassLibrary
{
    /// <summary>
    /// Marcamos el formato del resto de handlers
    /// </summary>
    public abstract class AbstractHandler : IHandler
    {
        /// <summary>
        /// Canal por el cual se envian los mensajes
        /// </summary>
        protected IMessageChannel messageChannel;

        protected IHandler nextHandler;
        /// <summary>
        /// Se setea el próximo handler (nextHandler)
        /// </summary>
        /// <param name="handler"></param>
        /// <returns></returns>
        
        private string Command;
        public IHandler SetNext(IHandler handler)
        {
            this.nextHandler = handler;
            return handler;
        }
        /// <summary>
        /// Verifica si el comando recibido es el perteneciente a esta clase, y ejecuta el workflow, o le pasa al próximo handler
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public virtual void Handle(IMessage input)
        {

            if (this.nextHandler != null)
            {
                this.nextHandler.Handle(input);
            }
            else
            {
                messageChannel.SendMessage("Ni idea");
            }
        }
        protected virtual bool CanHandle(IMessage input)
        {
            if (this.Command == null || this.Command.Length == 0)
            {
                throw new InvalidOperationException("No hay palabras clave que puedan ser procesadas");
            }

            return this.Command.Equals(input.Text, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}