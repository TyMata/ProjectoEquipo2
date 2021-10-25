using System;

namespace ClassLibrary
{
    /// <summary>
    /// Esta clase
    /// </summary>
    public interface IHandler
    {
        /// <summary>
        /// Se ocupa de hacer el Handle
        /// </summary>
        void Handle();
        /// <summary>
        /// Setea el proximo Handler
        /// </summary>
        /// <param name="IHandler"></param>
        void SetNextHandler(EventHandler IHandler);
    }
}