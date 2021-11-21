using System;
using System.Text;

namespace ClassLibrary
{
    /// <summary>
    /// Handler para que el usuario empresa pueda modificar la cantidad de material en una determinada oferta.
    /// </summary>
    public class ModifyQuantityHandler : AbstractHandler
    {
        /// <summary>
        /// Constructor de objetos ModifyQuantityHandler.
        /// </summary>
        /// <param name="channel"></param>
        public ModifyQuantityHandler(IMessageChannel channel)
        {
            this.Command = "/modificarcantidad";
            this.messageChannel = channel;
        }

        /// <summary>
        /// Se encarga de mostrar la lista de ofertas de la empresa y modificar la cantidad de la oferta determinada.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public override bool InternalHandle(IMessage input)
        {
            if (CanHandle(input))
            {
                Company company = CompanyRegister.Instance.GetCompanyByUserId(input.Id);
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
                    Offer offer = company.OfferRegister.Find(offer => offer.Id == oferta);
                    this.messageChannel.SendMessage("Ingrese la nueva cantidad de la oferta:\n");
                    int quantity = Convert.ToInt32(this.messageChannel.ReceiveMessage().Text);
                    offer.ChangeQuantity(quantity);
                    return true; 
                }
            }
            return false;
        }    
    } 
}
