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
        
        /// <summary>
        ///  Handle
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public override bool InternalHandle(IMessage input)
        {
            if(CanHandle(input))
            {
                this.messageChannel.SendMessage("Inserte el nuevo precio de la oferta:\n");
                string precio = this.messageChannel.ReceiveMessage().Text;
                return true;
            }
            return false;
        }
    }
}