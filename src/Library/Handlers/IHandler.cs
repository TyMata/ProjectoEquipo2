using System;

namespace ClassLibrary
{
    /// <summary>
    /// Esta clase
    /// </summary>
    public interface IHandler
    {
        /// <summary>
        /// Pasa el mensaje al proximo Handler
        /// </summary>
        /// <param name="handler"></param>
        IHandler SetNext(IHandler handler);
        /// <summary>
        /// Ejecución del proceso de Handler
        /// </summary>
        object Handle(string request);

    }
}