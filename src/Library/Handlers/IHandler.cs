using System;

namespace ClassLibrary
{
    /// <summary>
    /// Esta clase representa los handlers.
    /// </summary>
    public interface IHandler
    {
        /// <summary>
        /// Pasa el mensaje al proximo Handler.
        /// </summary>
        /// <param name="handler"></param>
        IHandler SetNext(IHandler handler);

        /// <summary>
        /// Verifica si se realiza el proceso o se lo manda al next handler.
        /// </summary>
        IHandler Handle(IMessage input, out string response);
        
        /// <summary>
        /// Ejecución del proceso de Handler.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="response"></param>
        /// <returns></returns>
        bool InternalHandle(IMessage input, out string response);
    }
}