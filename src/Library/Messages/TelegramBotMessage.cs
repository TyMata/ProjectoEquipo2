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

        private string text;
        
        /// <summary>
        /// Devuelve el Message
        /// </summary>
        /// <value></value>
        public string Text
        {
            get
            {
                return this.text;
            } 
            set
            {
                this.text = value;
            }
            
        }

        public TelegramBotMessage(int id, string text)
        {
            this.Id = id;
            this.Text = text;
        }
    }
}