using System;
using System.Text;

namespace ClassLibrary
{
    /// <summary>
    /// Handler encargado de delegar la accion de Modificar una oferta.
    /// </summary>
    public class ModifyOfferHandler: AbstractHandler
    {  
        private Offer offer;
        /// <summary>
        /// Constructor de objetos ModifyOfferHandler.
        /// </summary>
        /// <param name="channel"></param>
        public ModifyOfferHandler(IMessageChannel channel)
        {
            this.messageChannel = channel;
            this.Command = "/modificaroferta";
            this.offer = null;
        }

        /// <summary>
        /// Metodo
        /// </summary>
        /// <param name="input"></param>
        public override bool InternalHandle(IMessage input)
        {
            if (CanHandle(input))
            {
                Company company = Singleton<CompanyRegister>.Instance.GetCompanyByUserId(input.Id);
                if(company.OfferRegister != null)
                {
                    StringBuilder offers = new StringBuilder("Que oferta desea modificar:\n");
                    foreach(Offer x in company.OfferRegister)
                    {
                        offers.Append($"Id : {x.Id}\n")
                            .Append($"Material : {x.Material}\n")
                            .Append($"Cantidad: {x.QuantityMaterial}\n")
                            .Append($"Fecha de publicacion: {x.PublicationDate}\n")
                            .Append($"Precio: {x.TotalPrice}\n")
                            .Append($"\n-----------------------------------------------\n\n");
                    }                       
                    this.messageChannel.SendMessage(offers.ToString());
                    int oferta = Convert.ToInt32(this.messageChannel.ReceiveMessage().Text);
                    this.offer = company.OfferRegister.Find(offer => offer.Id == oferta);
                    StringBuilder commandsStringBuilder = new StringBuilder($"Que desea modificar?\n")
                                                                                .Append("/modificarcantidad\n")
                                                                                .Append("/modificarprecio\n")
                                                                                .Append("/modificarhabilitaciones\n");
                    this.messageChannel.SendMessage(commandsStringBuilder.ToString()); 
                    input = this.messageChannel.ReceiveMessage();                                                     
                    return true;
                }   
                else 
                {
                    this.messageChannel.SendMessage("No hay ninguna oferta publicada bajo el nombre de esta empresa.");
                }
            }
            return false;
        }
    }
}