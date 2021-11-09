using System;
using System.Text;

namespace ClassLibrary
{
    /// <summary>
    /// Handler encargado de delegar la accion de Modificar una oferta
    /// </summary>
    public class ModifyOfferHandler: AbstractHandler
    {
        /// <summary>
        /// Constructor de objetos ModifyOfferHandler
        /// </summary>
        /// <param name="channel"></param>
        public ModifyOfferHandler(IMessageChannel channel)
        {
            this.messageChannel = channel;
            this.Command = "/modificaroferta";
            // this.nextHandler2 = new ModifyQuantityHandler(this.messageChannel);
            // this.nextHandler3 = new ModifyUnitPriceHandler(this.messageChannel);
            // this.nextHandler4 = new ModifyHabilitationsHandler(this.messageChannel);

            // this.nextHandler2.SetNext(nextHandler3);
            // this.nextHandler3.SetNext(nextHandler4);


        }
        public override void Handle(IMessage input)
        {
            if(this.nextHandler != null && (CanHandle(input)))
            {
                if("Company.ActualOffers" != null)
                { 
                    StringBuilder commandsStringBuilder = new StringBuilder($"Que desea modificar?\n")
                                                                                .Append("/ModificarCantidad\n")
                                                                                .Append("/ModificarPrecio\n")
                                                                                .Append("/ModificarHabilitaciones\n");
                    this.messageChannel.SendMessage(commandsStringBuilder.ToString());                                                      
                    this.nextHandler.Handle(input);
                }
                else 
                {
                    this.messageChannel.SendMessage("No hay ninguna oferta publicada bajo el nombre de esta empresa.");
                }
            }
            else
            {
                this.nextHandler.Handle(input);
            }
        }
    }
}