using System;

namespace ClassLibrary
{
    /// <summary>
    /// Esta clase representa los mensajes.
    /// </summary>
    public interface IMessage
    {
        /// <summary>
        /// Devuelve el Id
        /// </summary>
        /// <value></value>
        string Id{get;}
        /// <summary>
        /// Devuelve el Message
        /// </summary>
        /// <value></value>
        string Text{get;}
    }
}