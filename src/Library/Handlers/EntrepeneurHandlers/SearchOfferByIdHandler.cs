using System.Text;

namespace ClassLibrary
{
    public class SearchOfferByIdHandler : AbstractHandler
    {   
        public SearchOfferByIdHandler(IMessageChannel channel)
        {
            this.Command = "/BuscarOfertaPorId";
            this.messageChannel = channel;
        }
        public override void Handle(IMessage input)
        {
            if (this.nextHandler != null && (CanHandle(input)))
            {
                this.messageChannel.SendMessage("Escriba la Id de la oferta a buscar");
            }
            else
            {
                this.nextHandler.Handle(input);
            }
        }
    }
}