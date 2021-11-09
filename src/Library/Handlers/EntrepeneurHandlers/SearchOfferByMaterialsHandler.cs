using System.Text;

namespace ClassLibrary
{
    /// <summary>
    /// Handler encargado de delegar la accion de buscar ofertas por material
    /// </summary>
    public class SearchOfferByMaterialsHandler : AbstractHandler
    {   
        /// <summary>
        /// Constructor de objetos SearchOfferByMaterialsHandler
        /// </summary>
        /// <param name="channel"></param>
        public SearchOfferByMaterialsHandler(IMessageChannel channel)
        {
            this.Command = "/buscarofertapormaterial";
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