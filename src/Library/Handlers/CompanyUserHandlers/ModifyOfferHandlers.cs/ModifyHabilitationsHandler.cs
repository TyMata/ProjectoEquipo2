using System;

namespace ClassLibrary
{
    /// <summary>
    /// Handler para que el usuario empresa pueda modificar las habilitaciones de una determinada oferta.
    /// </summary>
    public class ModifyHabilitationsHandler : AbstractHandler
    {
        /// <summary>
        /// Constructor de objetos ModifyHabilitationsHandler
        /// </summary>
        /// <param name="channel"></param>
        public ModifyHabilitationsHandler(IMessageChannel channel)
        {
            this.Command = "/modificarhabilitaciones";
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