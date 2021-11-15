using System.Text;

namespace ClassLibrary
{
    /// <summary>
    /// Handler encargado de delegar la accion de buscar ofertas por su Id
    /// </summary>
    public class SearchOfferByIdHandler : AbstractHandler
    {   
        /// <summary>
        /// Constructor de objetos SearchOfferByIdHandler.
        /// </summary>
        /// <param name="channel"></param>
        public SearchOfferByIdHandler(IMessageChannel channel)
        {
            this.Command = "/buscarofertaporid";
            this.messageChannel = channel;
        }
        public override bool InternalHandle(IMessage input)
        {
            if ((CanHandle(input)))
            {
                this.messageChannel.SendMessage("Escriba la Id de la oferta a buscar");
                return true;
            }
            return false;
        }
    }
}