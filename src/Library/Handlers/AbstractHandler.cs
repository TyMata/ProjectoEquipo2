using System;
using System.Collections.Generic;
namespace ClassLibrary
{
    /// <summary>
    /// Marcamos el formato del resto de handlers
    /// </summary>
    public abstract class AbstractHandler : IHandler
    {
        private IHandler nextHandler;
        /// <summary>
        /// Se setea el próximo handler (nextHandler)
        /// </summary>
        /// <param name="handler"></param>
        /// <returns></returns>
        public IHandler SetNext(IHandler handler)
        {
            this.nextHandler = handler;
            return handler;
        }
        /// <summary>
        /// Verifica si el comando recibido es el perteneciente a esta clase, y ejecuta el workflow, o le pasa al próximo handler
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public virtual object Handle(string request)
        {
            if (this.nextHandler != null)
            {
                return this.nextHandler.Handle(request);
            }
            else
            {
                return null;
            }
        }
    }
}