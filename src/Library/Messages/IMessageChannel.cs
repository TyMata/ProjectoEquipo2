using System;

namespace ClassLibrary
{
    /// <summary>
    /// Esta clase representa los mensajes.
    /// </summary>
    public interface IMessageChannel
    {
        /// <summary>
        /// Recibe un mensaje y devuelve un IMessage a partir del mensaje recibido
        /// </summary>
        /// <returns></returns>
        IMessage ReceiveMessage();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mensaje"></param>
        void SendMessage(string mensaje);
    }
}