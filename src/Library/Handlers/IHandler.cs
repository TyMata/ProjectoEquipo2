using System;

namespace ClassLibrary
{
    /// <summary>
    /// Esta clase
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
        /// <param name="input"></param>
        IHandler Handle(IMessage input);
        
        /// <summary>
        /// Ejecución del proceso de Handler.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        bool InternalHandle(IMessage input);
    }
}