using System.Text;

namespace ClassLibrary
{
    /// <summary>
    /// Handler encargado de delegar la accion de buscar ofertas por material
    /// </summary>
    public class SearchOfferByMaterialsHandler : AbstractHandler
    {   
        /// <summary>
        /// Constructor de objetos SearchOfferByMaterialsHandler.
        /// </summary>
        /// <param name="channel"></param>
        public SearchOfferByMaterialsHandler(IMessageChannel channel)
        {
            this.Command = "/buscarofertapormaterial";
            this.messageChannel = channel;
        }
        public override bool InternalHandle(IMessage input)
        {
            if ((CanHandle(input)))
            {
                this.messageChannel.SendMessage("Escriba el material de la oferta a buscar");
                return true;
            }
            return false;
        }
    }
}