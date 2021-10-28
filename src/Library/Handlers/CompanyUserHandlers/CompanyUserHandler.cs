using System;
using System.Text;
namespace ClassLibrary
{
    /// <summary>
    ///  HAndler
    /// </summary>
    public class CompanyUserHandler : AbstractHandler, IHandler
    {
        private IHandler NextHandler1;
        private IHandler nextHandler2;
        private IHandler nextHandler3;
        private IHandler nextHandler4;
        private IHandler nextHandler5;
        private IHandler nextHandler6;

        public CompanyUserHandler(IMessageChannel channel)
        {
            this.messageChannel = channel;
            this.nextHandler2 = new PublishOfferHandler(this.messageChannel);
            this.nextHandler3 = new RemoveOfferHandler(this.messageChannel);
          
            this.nextHandler4 = new SuspendOfferHandler(this.messageChannel);
            this.nextHandler5 = new UnsuspendOfferHandler(this.messageChannel);
            this.nextHandler6 = new ModifyOfferHandler(this.messageChannel);
            
            this.nextHandler2.SetNext(this.nextHandler3);
            this.nextHandler3.SetNext(this.nextHandler4);
            this.nextHandler4.SetNext(this.nextHandler5);
            this.nextHandler5.SetNext(this.nextHandler6);

        }
        public override void Handle(IMessage input)
        {
            
            if (input.Text.ToLower().Trim() == "/Empresa")
            {
                StringBuilder commandsStringBuilder = new StringBuilder($"Bienvendio Company.Name.\n Que desea hacer?:\n")
                                                                            .Append("/Publicar Oferta\n")
                                                                            .Append("/Retirar Oferta\n")
                                                                            .Append("/Suspender Oferta\n")
                                                                            .Append("/Anular Suspension Oferta\n")
                                                                            .Append("/Modificar Oferta\n")
                                                                            .Append("/Buscar Oferta\n");
                this.messageChannel.SendMessage(commandsStringBuilder.ToString());
                input = this.messageChannel.ReceiveMessage();
                this.nextHandler2.Handle(input);
            }
            else if (NextHandler1 != null)
            {
                this.NextHandler1.Handle(input);
            }
        }
       
        


    }
}