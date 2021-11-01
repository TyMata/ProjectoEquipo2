namespace ClassLibrary
{
    class SearchOfferByMaterialsHandler : AbstractHandler, IHandler
    {
        public SearchOfferByMaterialsHandler(IMessageChannel channel)
        {
            this.messageChannel = channel;
        }

        public override void Handle(IMessage input)
        {
            if (input.Text.ToLower().Trim() == "/searchOfferByMaterialsHandler")
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