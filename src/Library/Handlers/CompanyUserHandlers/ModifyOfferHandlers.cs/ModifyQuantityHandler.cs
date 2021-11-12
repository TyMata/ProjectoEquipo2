using System;

namespace ClassLibrary
{
    /// <summary>
    /// Handler para que el usuario empresa pueda modificar la cantidad de material en una determinada oferta
    /// </summary>
    public class ModifyQuantityHandler : AbstractHandler
    {
        /// <summary>
        /// Constructor de objetos ModifyQuantityHandler
        /// </summary>
        /// <param name="channel"></param>
        public ModifyQuantityHandler(IMessageChannel channel)
        {
            this.Command = "/modificarcantidad";
            this.messageChannel = channel;
        }
        public override void Handle(IMessage input)
        {
            if(this.nextHandler != null && (CanHandle(input)))
            {
                this.messageChannel.SendMessage("Ingrese la nueva cantidad de la oferta:\n");
                string quantity = this.messageChannel.ReceiveMessage().ToString(); 

            }
            else
            {
                this.nextHandler.Handle(input);
            }
        }    
    } 
}
