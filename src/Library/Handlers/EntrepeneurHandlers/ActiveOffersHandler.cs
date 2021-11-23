using System.Text;

namespace ClassLibrary
{
    /// <summary>
    /// HAndler encargado de 
    /// </summary>
    public class ActiveOfferHandler : AbstractHandler
    {
        /// <summary>
        /// Constructor de objetos ActiveOfferHandler.
        /// </summary>
        /// <param name="channel"></param>
        public ActiveOfferHandler()
        {
        }
        /// <summary>
        /// Le avisa al usuario que el comando no se reconocio y 
        /// va hacia el siguiente handler de la cadena.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="response"></param>
        public override bool InternalHandle(IMessage input, out string response)
        {
            if ((CanHandle(input)))
            {
                StringBuilder commandsStringBuilder = new StringBuilder($"Elige las palabras claves para buscar una oferta:\n")
                                                                            .Append("/BuscarOfertaPorPalabrasClave\n");
                response = commandsStringBuilder.ToString();
                return true;
                //this.nextHandler.Handle(this.messageChannel.ReceiveMessage());
            }
            response = string.Empty;
            return false;
        }
    }
}   