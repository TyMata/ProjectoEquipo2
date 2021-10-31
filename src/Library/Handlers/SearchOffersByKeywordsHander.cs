namespace ClassLibrary
{
    class SearchOfferByKeywordsHandler : AbstractHandler, IHandler
    {
        public SearchOfferByKeywordsHandler(IMessageChannel channel)
        {
            this.messageChannel = channel;
        }

        public override void Handle(IMessage input)
        {
            if (input.Text.ToLower().Trim() == "/searchOfferByKeywordsHandler")
            {
                this.messageChannel.SendMessage("Inserte Material a buscar");
                string material = this.messageChannel.ReceiveMessage().Text;
               
            }
            else
            {
                this.nextHandler.Handle(input);
            }
        }
    }
}