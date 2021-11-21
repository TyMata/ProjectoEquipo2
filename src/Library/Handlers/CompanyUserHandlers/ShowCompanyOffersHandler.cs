namespace ClassLibrary
{
    class ShowCompanyOffersHandler : AbstractHandler, IHandler
    {
        public ShowCompanyOffersHandler(IMessageChannel channel)
        {
            this.Command = "/mostrarofertas";
            this.messageChannel = channel;
        }

        public override bool InternalHandle(IMessage input, out string response)
        {

            if (CanHandle(input))
            {
                response="Inserte las keywords que desea utilizar, separadas por una coma, para buscar una oferta";
                string[] keywords = this.messageChannel.ReceiveMessage().Text.Split(",");
                return true;
            }
            else
            {
                response = string.Empty;
                return false;
            }
            
        }
    }
}