using System;

namespace ClassLibrary
{
    /// <summary>
    /// h
    /// </summary>
    public class UnsuspendOfferHandler : AbstractHandler, IHandler
    {
        private IHandler NextHandler;
        private string Command;
        /// <summary>
        /// Despausa una oferta determinada
        /// </summary>
        /// <param name="channel"></param>
        public UnsuspendOfferHandler(IMessageChannel channel, IHandler next)
        {
            this.messageChannel = channel;
            this.NextHandler = next;
            this.Command = "/anular suspencion oferta";
        }
        public override void Handle(IMessage input)
        {
             if(this.CanHandle(input))
            {
                 if("Company.ActualOffers" != null)
                {
                    this.messageChannel.SendMessage($"Estas son tus ofertas pausadas: Company.PausedOffers./n ¿Cual es el Id de la que quiere despausar?");
                    string id =  this.messageChannel.ReceiveMessage().Text;
                    this.messageChannel.SendMessage($"La oferta Oferta ha sido despausada.");
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