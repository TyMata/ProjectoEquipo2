using System.Text;

namespace ClassLibrary
{
    /// <summary>
    /// Handler encargado de delegar la accion de buscar ofertas por su ubicación
    /// </summary>
    public class SearchOfferByLocationHandler : AbstractHandler
    {   
        /// <summary>
        /// Constructor de objetos SearchOfferByLocationHandler
        /// </summary>
        /// <param name="channel"></param>
        public SearchOfferByLocationHandler(IMessageChannel channel)
        {
            this.Command = "/buscarofertaporubicación";
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