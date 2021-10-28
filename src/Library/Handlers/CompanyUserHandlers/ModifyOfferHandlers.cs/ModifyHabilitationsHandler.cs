using System;

namespace ClassLibrary
{
    /// <summary>
    /// Handler para que el usuario empresa pueda modificar las habilitaciones de una determinada oferta.
    /// </summary>
    public class ModifyHabilitationsHandler : AbstractHandler, IHandler
    {
        private IHandler NextHandler;
        public ModifyHabilitationsHandler(IMessageChannel channel)
        {
            this.messageChannel = channel;
        }
        public override void Handle(IMessage input)
        {
            if(input.Text.ToLower().Trim() == "/Modificar Habilitaciones")
            {
                

            }
             else if (NextHandler != null)
            {
                NextHandler.Handle(input);
            }

        }
        
      
    }
}