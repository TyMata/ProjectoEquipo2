using System;

namespace ClassLibrary
{
    /// <summary>
    /// Handler para que el usuario empresa pueda modificar las habilitaciones de una determinada oferta.
    /// </summary>
    public class ModifyHabilitationsHandler : AbstractHandler, IHandler
    {
        private IHandler NextHandler;
        private string Command;
        public ModifyHabilitationsHandler(IMessageChannel channel, IHandler next)
        {
            this.messageChannel = channel;
            this.NextHandler = next;
            this.Command = "/modificar habilitaciones";
        }
        public override void Handle(IMessage input)
        {
            if(this.CanHandle(input))
            {
                

            }
             else if (this.NextHandler != null)
            {
                this.NextHandler.Handle(input);
            }

        }
        
      
    }
}