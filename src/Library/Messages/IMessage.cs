using System;

namespace ClassLibrary
{
    /// <summary>
    /// Esta clase representa los mensajes.
    /// </summary>
    public interface IMessage
    {
        /// <summary>
        /// Devuelve el .
        /// </summary>
        /// <value></value>
        int Id{get;}
        /// <summary>
        /// Devuelve el Message.
        /// </summary>
        /// <value></value>
        string Text{get; set;}
    }
}