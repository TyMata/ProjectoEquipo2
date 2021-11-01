using System;

namespace ClassLibrary
{
    public class RemoveOfferHandler: AbstractHandler, IHandler
    {
        private IHandler NextHandler;
        
        public RemoveOfferHandler(IMessageChannel channel)
        {
            this.messageChannel = channel ;
        }
        public override void Handle(IMessage input)
        {
            if(input.Text.ToLower().Trim() == "/Remover Oferta")
            {
                if("Company.ActualOffers" != null)
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