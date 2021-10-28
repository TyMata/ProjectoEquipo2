using System;

namespace ClassLibrary
{
    /// <summary>
    /// Handler para que el usuario empresa pueda modificar la cantidad de material en una determinada oferta
    /// </summary>
    public class ModifyQuantityHandler : AbstractHandler, IHandler
    {
        private IHandler NextHandler;
        public ModifyQuantityHandler(IMessageChannel channel)
        {
            this.messageChannel = channel;
        }
        public override void Handle(IMessage input)
        {
            if(input.Text.ToLower().Trim()  == "/Modificar cantidad")
            {
                this.messageChannel.SendMessage("Escriba la nueva cantidad de material:");
                string cuantity = this.messageChannel.ReceiveMessage().Text;

               
            }
             else if (NextHandler != null)
            {
                NextHandler.Handle(input);
            }
        }
        
    }
}