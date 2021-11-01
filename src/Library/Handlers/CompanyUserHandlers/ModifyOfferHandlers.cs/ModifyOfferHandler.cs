using System;
using System.Text;

namespace ClassLibrary
{
    public class ModifyOfferHandler: AbstractHandler, IHandler
    {
        private IHandler NextHandler;
        private IHandler nextHandler2;
        private IHandler nextHandler3;
        private IHandler nextHandler4;
        private string Command;
        public ModifyOfferHandler(IMessageChannel channel, IHandler next)
        {
            this.messageChannel = channel;
            this.NextHandler =  next;
            this.Command = "/Modificar oferta";
            // this.nextHandler2 = new ModifyQuantityHandler(this.messageChannel);
            // this.nextHandler3 = new ModifyUnitPriceHandler(this.messageChannel);
            // this.nextHandler4 = new ModifyHabilitationsHandler(this.messageChannel);

            // this.nextHandler2.SetNext(nextHandler3);
            // this.nextHandler3.SetNext(nextHandler4);


        }
        public override void Handle(IMessage input)
        {
            
            if(input.Text.ToLower().Trim() == this.Command)
            {
                 if("Company.ActualOffers" != null)
                { 
                    StringBuilder commandsStringBuilder = new StringBuilder($"Que desea modificar?\n")
                                                                                .Append("/Modificar cantidad\n")
                                                                                .Append("/Modificar Precio\n")
                                                                                .Append("/Modificar habilitaciones\n")
                                                                                .Append("/Modificar tiempo\n");
                    this.messageChannel.SendMessage(commandsStringBuilder.ToString());                                                      
                    input = this.messageChannel.ReceiveMessage();
                    this.nextHandler2.Handle(input);
                }
                else 
                {
                    this.messageChannel.SendMessage("No hay ninguna oferta publicada bajo el nombre de esta empresa.");
                }

            }
             else if (NextHandler != null)
            {
                NextHandler.Handle(input);
            }

           

        }
        
    }
}