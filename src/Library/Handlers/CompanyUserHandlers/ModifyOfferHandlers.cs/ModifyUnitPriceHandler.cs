using System;

namespace ClassLibrary
{
    /// <summary>
    /// Handler para qu eel usario empresa pueda modifcar el precio por unidad de un determinado material en una oferta
    /// </summary>
    public class ModifyUnitPriceHandler : AbstractHandler ,IHandler
    {
        private IHandler NextHandler;
        private string Command;
        public ModifyUnitPriceHandler(IMessageChannel channel, IHandler next)
        {
            this.messageChannel = channel;
            this.NextHandler = next;
            this.Command = "/modificar precio";
        }

        public override void Handle(IMessage input)
        {
            if(input.Text.ToLower().Trim() == this.Command)
            {
                this.messageChannel.SendMessage("Escribe el nueva precio: ");
                string price = this.messageChannel.ReceiveMessage().Text;
            }
             else if (NextHandler != null)
            {
                NextHandler.Handle(input);
            }

        }
        
    }
}