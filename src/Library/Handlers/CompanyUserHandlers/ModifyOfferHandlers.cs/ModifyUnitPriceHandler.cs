using System;

namespace ClassLibrary
{
    /// <summary>
    /// Handler para qu eel usario empresa pueda modifcar el precio por unidad de un determinado material en una oferta
    /// </summary>
    public class ModifyUnitPriceHandler : AbstractHandler ,IHandler
    {
        private IHandler NextHandler;
        public ModifyUnitPriceHandler(IMessageChannel channel)
        {
            this.messageChannel = channel;
        }

        public override void Handle(IMessage input)
        {
            if(input.Text.ToLower().Trim() == "/Modificar precio")
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