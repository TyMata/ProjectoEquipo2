using System.Text;

namespace ClassLibrary
{
    /// <summary>
    /// HAndler encargado de 
    /// </summary>
    public class ActiveOfferHandler : AbstractHandler
    {
        /// <summary>
        /// Constructor de objetos ActiveOfferHandler.
        /// </summary>
        /// <param name="channel"></param>
        public ActiveOfferHandler(IMessageChannel channel)
        {
            this.messageChannel = channel;
        }
        public override bool InternalHandle(IMessage input)
        {
            if ((CanHandle(input)))
            {
                StringBuilder commandsStringBuilder = new StringBuilder($"Elige el parámetro a utilizar para buscar una oferta:\n")
                                                                            .Append("/BuscarOfertaPorId\n")
                                                                            .Append("/BuscarOfertaPorPalabrasClave\n")
                                                                            .Append("/BuscarOfertaPorUbicación\n")
                                                                            .Append("/BuscarOfertaPorMaterial\n");
                this.messageChannel.SendMessage(commandsStringBuilder.ToString());
                return true;
                //this.nextHandler.Handle(this.messageChannel.ReceiveMessage());
            }
            return false;
        }
    }
}   