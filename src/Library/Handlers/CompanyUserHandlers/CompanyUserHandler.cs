using System;
using System.Text;
namespace ClassLibrary
{
    /// <summary>
    ///  
    /// </summary>
    public class CompanyUserHandler : AbstractHandler
    {
        // private IHandler NextHandler;
        // private IHandler nextHandler2;
        // private IHandler nextHandler3;
        // private IHandler nextHandler4;
        // private IHandler nextHandler5;
        // private IHandler nextHandler6;
        // private string  Command;
        /// <summary>
        /// Constructor de objetos CompanyUserHander
        /// </summary>
        /// <param name="channel"></param>
        public CompanyUserHandler(IMessageChannel channel)
        {
            this.Command = "/Empresa";
            this.messageChannel = channel;
            // this.nextHandler2 = new PublishOfferHandler(this.messageChannel);
            // this.nextHandler3 = new RemoveOfferHandler(this.messageChannel);
          
            // this.nextHandler4 = new SuspendOfferHandler(this.messageChannel);
            // this.nextHandler5 = new UnsuspendOfferHandler(this.messageChannel);
            // this.nextHandler6 = new ModifyOfferHandler(this.messageChannel);
            
            // this.nextHandler2.SetNext(this.nextHandler3);
            // this.nextHandler3.SetNext(this.nextHandler4);
            // this.nextHandler4.SetNext(this.nextHandler5);
            // this.nextHandler5.SetNext(this.nextHandler6);

        }
        public override void Handle(IMessage input)
        {
            if (this.nextHandler != null && (CanHandle(input)))
            {
                StringBuilder commandsStringBuilder = new StringBuilder($"Bienvenido Company.Name.\n Que desea hacer?:\n")
                                                                            .Append("/PublicarOferta\n")
                                                                            .Append("/RetirarOferta\n")
                                                                            .Append("/SuspenderOferta\n")
                                                                            .Append("/ReanudarOferta\n")
                                                                            .Append("/ModificarOferta\n")
                                                                            .Append("/BuscarOferta\n");
                this.messageChannel.SendMessage(commandsStringBuilder.ToString());
                //this.nextHandler.Handle(this.messageChannel.ReceiveMessage());
            }
            else
            {
                this.nextHandler.Handle(input);
            }
        }
    }
}