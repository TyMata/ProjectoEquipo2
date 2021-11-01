using System;

namespace ClassLibrary
{   
    /// <summary>
    /// Handler para pausar una determinada oferta
    /// </summary>
    public class SuspendOfferHandler : AbstractHandler , IHandler
    {
        
        private IHandler NextHandler;
        private string Command;
        public SuspendOfferHandler(IMessageChannel channel, IHandler next)
        {
            this.messageChannel = channel;
            this.NextHandler = next;
            this.Command = "/pausar oferta";
        }

        public override void Handle(IMessage input)
        {
             if(this.CanHandle(input))
            {
                 if("Company.ActualOffers" != null)
                {
                    this.messageChannel.SendMessage($"Estas son tus ofertas actuales: Company.ActualOffers./n ¿Cual es el Id de la que quiere pausar?");
                    string id = this.messageChannel.ReceiveMessage().Text;
                    this.messageChannel.SendMessage($"La oferta Oferta ha sido pausada.");
                }
                else 
                {
                    this.messageChannel.SendMessage("No hay ninguna oferta publicada bajo el nombre de esta empresa.");
                }

            }
             else if (NextHandler != null)
            {
                NextHandler.Handle(input);
            }

        }
        
       
    }
}