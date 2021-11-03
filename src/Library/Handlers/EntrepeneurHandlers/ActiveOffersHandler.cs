using System.Text;

namespace ClassLibrary
{
    /// <summary>
    /// Handler para las ofertas activas
    /// </summary>
    public class ActiveOfferHandler : AbstractHandler
    {
        public ActiveOfferHandler(IMessageChannel channel)
        {
            this.messageChannel = channel;
        }

        public override void Handle(IMessage input)
        {
            if (this.nextHandler != null && (CanHandle(input)))
            {
                StringBuilder commandsStringBuilder = new StringBuilder($"Elige el parámetro a utilizar para buscar una oferta:\n")
                                                                            .Append("/BuscarOfertaPorId\n")
                                                                            .Append("/BuscarOfertaPorPalabrasClave\n")
                                                                            .Append("/BuscarOfertaPorUbicación\n")
                                                                            .Append("/BuscarOfertaPorMaterial\n");
                this.messageChannel.SendMessage(commandsStringBuilder.ToString());
                //this.nextHandler.Handle(this.messageChannel.ReceiveMessage());
            }
            else
            {
            this.nextHandler.Handle(input);
            }
        }
    }
}