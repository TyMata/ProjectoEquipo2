namespace ClassLibrary
{
    class SearchCompanyOffersByIdHandler : AbstractHandler, IHandler
    {
        public SearchCompanyOffersByIdHandler(IMessageChannel channel)
        {
            this.Command = "/buscarofertaporid";
            this.messageChannel = channel;
        }

        public override bool InternalHandle(IMessage input)
        {
            if (CanHandle(input))
            {
                this.messageChannel.SendMessage("Inserte las keywords que desea utilizar, separadas por una coma, para buscar una oferta");
                string[] keywords = this.messageChannel.ReceiveMessage().Text.Split(",");
                return true;
                
               
            }
            return false;
        }
    }
}