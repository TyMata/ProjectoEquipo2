namespace ClassLibrary
{
    class SearchOfferByIdHandler : AbstractHandler, IHandler
    {
        private string Command;
        public SearchOfferByIdHandler(IMessageChannel channel)
        {
            this.messageChannel = channel;
        }

        public override void Handle(IMessage input)
        {
            if (input.Text.ToLower().Trim() == "/searchOfferByIdHandler")
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