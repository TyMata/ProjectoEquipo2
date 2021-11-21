using System;

namespace ClassLibrary
{
    /// <summary>
    /// Esta clase representa los mensajes.
    /// </summary>
    public class TelegramBotMessage : IMessage
    {
        /// <summary>
        /// Devuelve el Id
        /// </summary>
        /// <value></value>
        public int Id{get;}

        /// <summary>
        /// Devuelve el Message
        /// </summary>
        /// <value></value>
        public string Text{get;}

        public TelegramBotMessage(int id, string text)
        {
            this.Id = id;
            this.Text = text;
        }
    }
}