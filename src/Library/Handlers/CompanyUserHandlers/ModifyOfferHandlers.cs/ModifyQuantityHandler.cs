using System;

namespace ClassLibrary
{
    /// <summary>
    /// Handler para que el usuario empresa pueda modificar la cantidad de material en una determinada oferta
    /// </summary>
    public class ModifyQuantityHandler : AbstractHandler
    {
        public ModifyQuantityHandler(IMessageChannel channel)
        {
            this.Command = "/ModificarCantidad";
            this.messageChannel = channel;
        }
        public override void Handle(IMessage input)
        {
            if(this.nextHandler != null && (CanHandle(input)))
            {
                this.messageChannel.SendMessage("Escriba la nueva cantidad de material");
                string quantity = this.messageChannel.ReceiveMessage().Text;
            }
             else
            {
                this.nextHandler.Handle(input);
            }
        }
        
    }
}