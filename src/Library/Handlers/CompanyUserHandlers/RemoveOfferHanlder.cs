using System;

namespace ClassLibrary
{
    public class RemoveOfferHandler: AbstractHandler
    {   
        public RemoveOfferHandler(IMessageChannel channel)
        {
            this.Command = "/RetirarOferta";            
            this.messageChannel = channel ;
        }
        public override void Handle(IMessage input)
        {
            if(this.nextHandler != null && (CanHandle(input)))
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
            else
            {
                this.nextHandler.Handle(input);
            }
        }
    }
}