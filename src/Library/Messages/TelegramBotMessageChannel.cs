using System;

namespace ClassLibrary
{
    /// <summary>
    /// Esta clase representa los mensajes.
    /// </summary>
    public class TelegramBotMessageChannel : IMessageChannel
    {
        /// <summary>
        /// Recibe un mensaje y devuelve un IMessage a partir del mensaje recibido
        /// </summary>
        /// <returns></returns>
        public IMessage ReceiveMessage()
        {
            IMessage mensajeConsola = new TelegramBotMessage( 123 , "mensaje");
            return mensajeConsola;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mensaje"></param>
        public void SendMessage(string mensaje)
        {

        }
    }
}