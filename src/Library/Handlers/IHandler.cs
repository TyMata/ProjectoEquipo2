using System;

namespace ClassLibrary
{
    /// <summary>
    /// Esta clase
    /// </summary>
    public interface IHandler
    {
        /// <summary>
        /// Ejecuta el comando
        /// </summary>
        void Execute();
        /// <summary>
        /// Pasa el mensaje al proximo Handler
        /// </summary>
        /// <param name="message"></param>
        void NextHandler(string message);
    }
}