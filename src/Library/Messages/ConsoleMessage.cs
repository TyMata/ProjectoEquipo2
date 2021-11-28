using System;

namespace ClassLibrary
{
    /// <summary>
    /// Esta clase representa los mensajes.
    /// </summary>
    public class ConsoleMessage : IMessage
    {
        private int id;
        /// <summary>
        /// Devuelve el Id.
        /// </summary>
        /// <value></value>
        public int Id{get;}
        private string text;
        /// <summary>
        /// Devuelve el Message.
        /// </summary>
        /// <value></value>
        public string Text{
            get
            {
                return this.text;
            }
            set
            {
                this.Text = value;
            }
            }
            
        /// <summary>
        /// Constructor de ConsoleMessage.
        /// </summary>
        /// <param name="mensaje"></param>
        public ConsoleMessage(string mensaje)
        {
            this.text = mensaje;
            this.id = 0;
        }
    }
}