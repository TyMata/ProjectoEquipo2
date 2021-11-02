namespace ClassLibrary
{
    class SearchCompanyOffersByKeywordsHandler : AbstractHandler, IHandler
    {
        public SearchCompanyOffersByKeywordsHandler(IMessageChannel channel)
        {
            this.Command = "/buscarofertaporpalabrasclave";
            this.messageChannel = channel;
        }

        public override void Handle(IMessage input)
        {
            if (this.nextHandler != null && (CanHandle(input)) )
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