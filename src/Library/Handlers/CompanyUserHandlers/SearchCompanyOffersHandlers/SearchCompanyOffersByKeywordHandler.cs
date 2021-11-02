namespace ClassLibrary
{
    class SearchCompanyOffersByKeywordsHandler : AbstractHandler, IHandler
    {
        public SearchCompanyOffersByKeywordsHandler(IMessageChannel channel)
        {
            this.messageChannel = channel;
        }

        public override void Handle(IMessage input)
        {
            if (input.Text.ToLower().Trim() == "/searchOfferByKeywordsHandler")
            {
                this.messageChannel.SendMessage("Inserte las keywords que desea utilizar, separadas por una coma, para buscar una oferta");
                string[] keywords = this.messageChannel.ReceiveMessage().Text.Split(",");
                
               
            }
            else
            {
                this.nextHandler.Handle(input);
            }
        }
    }
}