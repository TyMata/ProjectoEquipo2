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
      /// <summary>
      /// Se crea el constructor de TelegramBotMessage que tiene como parametros id y text.
      /// </summary>
      /// <param name="id"></param>
      /// <param name="text"></param>
        public TelegramBotMessage(int id, string text)
        {
            this.Id = id;
            this.Text = text;
        }
    }
}