using System.Text;

namespace ClassLibrary
{
    public class ActiveOfferHandler : AbstractHandler, IHandler
    {
        private IHandler nextHandler1;
        private IHandler nextHandler2;
        private IHandler nextHandler3;
        private IHandler nextHandler4;
        private IHandler nextHandler5;

        public ActiveOfferHandler(IMessageChannel channel)
        {
            this.messageChannel = channel;
            this.nextHandler2 = new SearchOfferByIdHandler(this.messageChannel);
            this.nextHandler3 = new SearchOfferByKeywordsHandler(this.messageChannel);
            this.nextHandler4 = new SearchOfferByLocationHandler(this.messageChannel);
            this.nextHandler5 = new SearchOfferByMaterialsHandler(this.messageChannel);

            this.nextHandler1.SetNext(this.nextHandler3);
            this.nextHandler2.SetNext(this.nextHandler4);
            this.nextHandler3.SetNext(this.nextHandler5);
        }

        public override void Handle(IMessage input)
        {
            if (input.Text.ToLower().Trim()  == "/Emprendedor")
            {
                StringBuilder commandsStringBuilder = new StringBuilder($"Elige que parámetro utilizar para buscar la oferta:\n")
                                                                            .Append("/Buscar oferta por Id\n")
                                                                            .Append("/Buscar oferta por palabras clave\n")
                                                                            .Append("/Buscar oferta por Ubicación\n")
                                                                            .Append("/Buscar oferta por Material\n");
                this.messageChannel.SendMessage(commandsStringBuilder.ToString());
                input = this.messageChannel.ReceiveMessage();
                this.nextHandler2.Handle(input);
            }

            else if (nextHandler1 != null)
            {
            this.nextHandler1.Handle(input);
            }
        }
    }
}