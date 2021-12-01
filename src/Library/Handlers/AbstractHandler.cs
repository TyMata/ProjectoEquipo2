using System;
using System.Collections.Generic;
using System.Linq;
namespace ClassLibrary
{
    /// <summary>
    /// Esta es clase base para implementar el patrón Chain of Responsibility. En ese patrón se pasa un mensaje a través de una
    /// cadena de handlers que pueden procesar a este o no. Cada "handler" decide si procesa el mensaje, o si se lo
    /// pasa al siguiente. Esta clase implmementa la responsabilidad de recibir el mensaje y pasarlo al siguiente
    /// "handler" en caso que el mensaje no sea procesado. La responsabilidad de decidir si el mensaje se procesa o no, y
    /// de procesarlo, se delega a las clases sucesoras de esta clase base.
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
        /// Se establece el próximo handler (nextHandler).
        /// </summary>
        /// <param name="handler"></param>
        /// <returns></returns>
        public IHandler SetNext(IHandler handler)
        {
            this.nextHandler = handler;
            return handler;
        }

        /// <summary>
        /// Este debe de ser sobrescrito por las clases sucesoras, verifica si el mensaje recibido es el perteneciente a esta clase, y ejecuta el workflow, o le pasa al próximo handler.
        /// Retorna verdadeo o falso si lo puede procesar al mensaje o no.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="response"></param>
        /// <returns></returns>
        public virtual bool InternalHandle(IMessage input, out string response)
        {
            throw new Exception();
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

        /// <summary>
        /// Metodo encargado de resetear el State y la Data del Handler. Debe de ser sobreescrito por las clases sucesoras, estas procesan varios mensajes
        /// y deben de volver a su estado inicial, en este handler este metodo no hace nada.
        /// </summary>
        protected virtual void InternalCancel()
        {

        }
    }   
}