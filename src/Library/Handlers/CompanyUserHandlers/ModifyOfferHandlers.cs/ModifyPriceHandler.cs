namespace ClassLibrary
{
    /// <summary>
    /// Handler para que el usuario empresa pueda modificar las habilitaciones de una determinada oferta.
    /// </summary>
    public class ModifyPriceHandler : AbstractHandler
    {
        /// <summary>
        /// Constructor de objetos ModifyPriceHanlder
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
                this.messageChannel.SendMessage("Pase por aquí el link que lleva a sus habilitaciones\n");
            }
            else
            {
                this.nextHandler.Handle(input);
            }
        }
    }
}