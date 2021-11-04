using System.Text;

namespace ClassLibrary
{
    public class SearchOfferByLocationHandler : AbstractHandler
    {   
        
        public SearchOfferByLocationHandler(IMessageChannel channel)
        {
            this.Command = "/BuscarOfertaPorUbicación";
            this.messageChannel = channel;
        }

        public override void Handle(IMessage input)
        {
            if (this.nextHandler != null && (CanHandle(input)))
            {
                this.messageChannel.SendMessage("Escriba la ubicación de la oferta a buscar");     
            }
            else
            {
                this.nextHandler.Handle(input);
            }
        }
    }
}