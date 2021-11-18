using System.Text;

namespace ClassLibrary
{
    /// <summary>
    /// Handler encargado de delegar la accion de mostrar las ofertas compradas por un emprendedor
    /// </summary>
    public class ShowBoughtOffersHandler : AbstractHandler
    {
        /// <summary>
        /// Constructor de objetos ShowBoughtOffersHandler.
        /// </summary>
        /// <param name="channel"></param>
        public ShowBoughtOffersHandler(IMessageChannel channel)
        {
            this.messageChannel = channel;
        }
        public override bool InternalHandle(IMessage input)
        {
            if (CanHandle(input))
            {
                StringBuilder commandsStringBuilder = new StringBuilder($"Elige el parámetro a utilizar para buscar el recibo de una oferta:\n")
                                                                            .Append("/BuscarOfertaPorId\n")
                                                                            .Append("/BuscarOfertaPorPalabrasClave\n")
                                                                            .Append("/BuscarOfertaPorUbicación\n")
                                                                            .Append("/BuscarOfertaPorMaterial\n");
                this.messageChannel.SendMessage(commandsStringBuilder.ToString());
                this.nextHandler.Handle(this.messageChannel.ReceiveMessage());
                return true;
            }
            return false;
        }
    }
}