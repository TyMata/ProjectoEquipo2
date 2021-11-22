using System;
using System.Collections.Generic;
using System.Linq;
namespace ClassLibrary
{
    /// <summary>
    /// Marcamos el formato del resto de handlers.
    /// </summary>
    public abstract class AbstractHandler : IHandler
    {
        
        /// <summary>
        /// Contiene al siguiente Handler.
        /// </summary>
        protected IHandler nextHandler;
        /// <summary>
        /// Palabra clave de Handler(comando).
        /// </summary>
        protected string Command;
        
        /// <summary>
        /// Se setea el próximo handler (nextHandler).
        /// </summary>
        /// <param name="handler"></param>
        /// <returns></returns>
        public IHandler SetNext(IHandler handler)
        {
            this.nextHandler = handler;
            return handler;
        }
        /// <summary>
        /// Verifica si el comando recibido es el perteneciente a esta clase, y ejecuta el workflow, o le pasa al próximo handler.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="response"></param>
        /// <returns></returns>
        public virtual bool InternalHandle(IMessage input, out string response)
        {
            throw new Exception();
            // if (this.nextHandler != null)
            // {
            //     this.nextHandler.Handle(input);
            // }
            // else
            // {
            //     messageChannel.SendMessage("Ni idea");
            // }
        }

        /// <summary>
        /// Verifica si el mensaje que recibe es igual al del comando.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        protected virtual bool CanHandle(IMessage input)
        {
            if (this.Command == null || this.Command.Length == 0)
            {
                throw new InvalidOperationException("No hay palabras clave que puedan ser procesadas");
            }
            return this.Command.Equals(input.Text.ToLower().Trim());
        }

        /// <summary>
        /// Procesa el mensaje o lo manda al siguiente handler si no lo puede procesar.
        /// </summary>
        /// <param name="message">El mensaje a procesar</param>
        /// <param name="response">Respuesta a enviar</param>
        /// <returns></returns>
        public IHandler Handle(IMessage message, out string response)
        {
            if (this.InternalHandle(message, out response))
            {
                return this;
            }
            else if (this.nextHandler != null)
            {
                return this.nextHandler.Handle(message, out response);
            }
            else
            {
                return null;
            }
        }
        protected virtual void InternalCancel()
        {

        }
    }   
}