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
                    User user = Singleton<UserRegister>.Instance.GetUserById(input.Id);
                    Company company = User.Company;

                    StringBuilder offers = new StringBuilder("Que oferta desea modificar:\n");
                    foreach(Offer x in Company.OfferRegister)
                    {
                        offers.Append($"Id : {x.Id}\n")
                            .Append($"Material : {x.Material}\n")
                            .Append($"Cantidad: {x.QuantityMaterial}\n")
                            .Append($"Fecha de publicacion: {x.PublicationDate}\n")
                            .Append($"Precio: {x.TotalPrice}\n")
                            .Append($"\n-----------------------------------------------\n\n");
                    }                       
                    this.messageChannel.SendMessage(offers.ToString());
                    string oferta = this.messageChannel.ReceiveMessage().Text;
                    StringBuilder commandsStringBuilder = new StringBuilder($"Que desea modificar?\n")
                                                                                .Append("/modificarcantidad\n")
                                                                                .Append("/modificarprecio\n")
                                                                                .Append("/modificarhabilitaciones\n");
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