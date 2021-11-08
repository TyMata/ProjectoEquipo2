using System.Text;

namespace ClassLibrary
{
    /// <summary>
    /// Handler encargado de delegar la accion de buscar ofertas por keywords
    /// </summary>
    public class SearchOfferByKeyWordsHandler : AbstractHandler
    {   
        /// <summary>
        /// Constructor de objetos SearchOfferByKeyWordsHandler
        /// </summary>
        /// <param name="channel"></param>
        public SearchOfferByKeyWordsHandler(IMessageChannel channel)
        {
            this.Command = "/BuscarOfertaPorKeyWords";
            this.messageChannel = channel;
        }
        public override void Handle(IMessage input)
        {
            if (this.nextHandler != null && (CanHandle(input)))
            {
                this.messageChannel.SendMessage("Escriba las palabras claves de la oferta a buscar");
            }
            else
            {
                this.nextHandler.Handle(input);
            }
        }
    }
}