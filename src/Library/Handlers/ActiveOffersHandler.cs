using System.Text;

namespace ClassLibrary
{
    public class ActiveOfferHandler : AbstractHandler, IHandler
    {
        private IHandler nextHandler1;
        private IHandler nextHandler2;
        private IHandler nextHandler3;
        private IHandler nextHandler4;

        public ActiveOfferHandler(IMessageChannel channel)
        {
            this.messageChannel = channel;
            this.nextHandler1 = new SearchOfferByIdHandler(this.messageChannel);
            this.nextHandler2 = new SearchOfferByKeywordsHandler(this.messageChannel);
            this.nextHandler3 = new SearchOfferByLocationHandler(this.messageChannel);
            this.nextHandler4 = new SearchOfferByMaterialsHandler(this.messageChannel);

            this.nextHandler1.SetNext(this.nextHandler2);
            this.nextHandler2.SetNext(this.nextHandler3);
            this.nextHandler3.SetNext(this.nextHandler4);

        }
    }
}