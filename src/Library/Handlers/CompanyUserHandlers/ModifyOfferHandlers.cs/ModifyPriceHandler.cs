namespace ClassLibrary
{
    /// <summary>
    /// Handler para que el usuario empresa pueda modificar el precio de una determinada oferta.
    /// </summary>
    public class ModifyPriceHandler : AbstractHandler
    {
        /// <summary>
        /// Constructor de objetos ModifyPriceHandler
        /// </summary>
        /// <param name="channel"></param>
        public ModifyPriceHandler(IMessageChannel channel)
        {
            this.Command = "/modificarprecio";
            this.messageChannel = channel;
        }
        public override void Handle(IMessage input)
        {
            if(this.nextHandler != null && (CanHandle(input)))
            {
                this.messageChannel.SendMessage("Inserte el nuevo precio de la oferta:\n");
                string precio = this.messageChannel.ReceiveMessage().Text;
            }
            else
            {
                this.nextHandler.Handle(input);
            }
        }
    }
}