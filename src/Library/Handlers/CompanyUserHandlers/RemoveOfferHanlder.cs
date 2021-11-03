using System;

namespace ClassLibrary
{
    public class RemoveOfferHandler: AbstractHandler, IHandler
    {
        private IHandler NextHandler;
        private string Command;
        
        public RemoveOfferHandler(IMessageChannel channel, IHandler next)
        {
            this.messageChannel = channel ;
            this.NextHandler = next ;
            this.Command = "/Remover oferta";
        }
        public override void Handle(IMessage input)
        {
            if(this.CanHandle(input))
            {
                if("Company.OfferRegister" != null)
                {
                    this.messageChannel.SendMessage($"Estas son tus ofertas actuales: Company.ActualOffers./n ¿Cual es el Id de la que quiere retirar?");
                    string id = this.messageChannel.ReceiveMessage().Text;
                    this.messageChannel.SendMessage($"La oferta Oferta se retiro del mercado");
                }
                else 
                {
                    this.messageChannel.SendMessage("No hay ninguna oferta publicada bajo el nombre de esta empresa");
                }

            }
             else if (NextHandler != null)
            {
                NextHandler.Handle(input);
            }
        }
        
       
    }
}