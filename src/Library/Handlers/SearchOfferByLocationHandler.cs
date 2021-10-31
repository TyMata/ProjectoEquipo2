namespace ClassLibrary
{
    class SearchOfferByLocationHandler : AbstractHandler, IHandler
    {
        public SearchOfferByLocationHandler(IMessageChannel channel)
        {
            this.messageChannel = channel;
        }

        public override void Handle(IMessage input)
        {
            if (input.Text.ToLower().Trim() == "/searchOfferByLocationHandler")
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