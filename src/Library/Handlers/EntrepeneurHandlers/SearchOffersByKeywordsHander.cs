using System.Text;

namespace ClassLibrary
{
    public class SearchOfferByKeyWordsHandler : AbstractHandler
    {   
        
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