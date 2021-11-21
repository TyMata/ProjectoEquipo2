namespace ClassLibrary
{
    /// <summary>
    /// Handler encargado de delegar la accion de buscar ofertas por keywords.
    /// </summary>
    public class SearchOfferByKeyWordsHandler : AbstractHandler
    {   
        /// <summary>
        /// Constructor de objetos SearchOfferByKeyWordsHandler.
        /// </summary>
        /// <param name="channel"></param>
        public SearchOfferByKeyWordsHandler(IMessageChannel channel)
        {
            this.Command = "/buscarofertaporkeyWords";
            this.messageChannel = channel;
        }

        public override bool InternalHandle(IMessage input)
        {
            if (CanHandle(input))
            {
                this.messageChannel.SendMessage("Escriba las palabras claves de la oferta a buscar");
                return true;
            }
            return false;
        }
    }
}