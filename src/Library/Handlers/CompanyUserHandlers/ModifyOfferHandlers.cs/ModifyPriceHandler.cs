using System;
using System.Text;

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
                Company company = CompanyRegister.Instance.GetCompanyByUserId(input.Id);
                if(company.OfferRegister != null)
                {
                    StringBuilder offers = new StringBuilder("Estas son todas sus ofertas:\n");
                    foreach(Offer x in company.OfferRegister)
                    {
                        offers.Append($"Id : {x.Id}\n")
                            .Append($"Material : {x.Material}\n")
                            .Append($"Cantidad: {x.QuantityMaterial}\n")
                            .Append($"Fecha de publicacion: {x.PublicationDate}\n")
                            .Append($"Precio: {x.TotalPrice}\n")
                            .Append($"\n-----------------------------------------------\n\n")
                            .Append("Cual quiere modificar?\n Ingrese el Id de esta:\n");
                    }                       
                    this.messageChannel.SendMessage(offers.ToString());
                    int oferta = Convert.ToInt32(this.messageChannel.ReceiveMessage().Text);
                    Offer offer = company.OfferRegister.Find(offer => offer.Id == oferta);
                    this.messageChannel.SendMessage("Inserte el nuevo precio de la oferta:\n");
                    int price = Convert.ToInt32(this.messageChannel.ReceiveMessage().Text);
                    offer.ChangePrice(price);
                    return true;
                }
            }
            return false;
        }
    }
}