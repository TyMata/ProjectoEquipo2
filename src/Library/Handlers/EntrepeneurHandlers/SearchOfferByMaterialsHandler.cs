using System.Text;

namespace ClassLibrary
{
    class SearchOfferByMaterialsHandler : AbstractHandler
    {   
        public SearchOfferByMaterialsHandler(IMessageChannel channel)
        {
            this.Command = "/BuscarOfertaPorMaterial";
            this.messageChannel = channel;
        }
        public override void Handle(IMessage input)
        {
            if (this.nextHandler != null && (CanHandle(input)))
            {
                this.messageChannel.SendMessage("Escriba el material de la oferta a buscar");
            }
            else
            {
                this.nextHandler.Handle(input);
            }
        }
    }
}