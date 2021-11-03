using System.Text;

namespace ClassLibrary
{
    public class ShowBoughtOffersHandler : AbstractHandler
    {
        public ShowBoughtOffersHandler(IMessageChannel channel)
        {
            this.messageChannel = channel;
        }

        public override void Handle(IMessage input)
        {
            if (this.nextHandler != null && (CanHandle(input)))
            {
                StringBuilder commandsStringBuilder = new StringBuilder($"Elige el parámetro a utilizar para buscar el recibo de una oferta:\n")
                                                                            .Append("/BuscarOfertaPorId\n")
                                                                            .Append("/BuscarOfertaPorPalabrasClave\n")
                                                                            .Append("/BuscarOfertaPorUbicación\n")
                                                                            .Append("/BuscarOfertaPorMaterial\n");
                this.messageChannel.SendMessage(commandsStringBuilder.ToString());
                this.nextHandler.Handle(this.messageChannel.ReceiveMessage());
            }

            else
            {
            this.nextHandler.Handle(input);
            }
        }
    }
}