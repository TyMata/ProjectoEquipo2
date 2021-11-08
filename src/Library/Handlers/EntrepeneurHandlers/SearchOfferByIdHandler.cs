using System.Text;

namespace ClassLibrary
{
    /// <summary>
    /// Handler encargado de delegar la accion de buscar ofertas por su Id
    /// </summary>
    public class SearchOfferByIdHandler : AbstractHandler
    {   
        /// <summary>
        /// Constructor de objetos SearchOfferByIdHandler
        /// </summary>
        /// <param name="channel"></param>
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