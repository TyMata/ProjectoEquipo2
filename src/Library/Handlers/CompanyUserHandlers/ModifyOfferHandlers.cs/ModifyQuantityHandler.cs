using System;

namespace ClassLibrary
{
    /// <summary>
    /// Handler para que el usuario empresa pueda modificar la cantidad de material en una determinada oferta
    /// </summary>
    public class ModifyQuantityHandler : AbstractHandler, IHandler
    {
        private IHandler NextHandler;
        private string Command;
        public ModifyQuantityHandler(IMessageChannel channel,IHandler next)
        {
            this.messageChannel = channel;
            this.NextHandler = next;
            this.Command = "/Modificar cantidad";
        }
        public override void Handle(IMessage input)
        {
            if(input.Text.ToLower().Trim()  == this.Command)
            {
                this.messageChannel.SendMessage("Escriba la nueva cantidad de material:");
                string quantity = this.messageChannel.ReceiveMessage().Text;

               
            }
             else if (NextHandler != null)
            {
                NextHandler.Handle(input);
            }
        }
        
    }
}