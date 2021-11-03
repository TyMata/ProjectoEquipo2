using System;

namespace ClassLibrary
{
    /// <summary>
    /// h
    /// </summary>
    public class ResumeOfferHandler : AbstractHandler
    {
        /// <summary>
        /// Reanuda una oferta determinada
        /// </summary>
        /// <param name="channel"></param>
        public ResumeOfferHandler(IMessageChannel channel)
        {
            this.Command = "/ReanudarOferta";
            this.messageChannel = channel;
        }
        public override void Handle(IMessage input)
        {
            if(this.CanHandle(input))
            {
                if("Company.ActualOffers" != null)
                {
                    this.messageChannel.SendMessage($"Estas son tus ofertas pausadas: Company.PausedOffers./n ¿Cual es el Id de la que quiere despausar?");
                    string id =  this.messageChannel.ReceiveMessage().Text;
                    this.messageChannel.SendMessage($"La oferta Oferta se ha reanudado.");
                }
                else
                {
                    this.messageChannel.SendMessage("No hay ninguna oferta publicada bajo el nombre de esta empresa.");
                }
            }
            else
            {
                nextHandler.Handle(input);
            }
        }   
    }
}