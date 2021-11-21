using System;
using System.Text;

namespace ClassLibrary
{
    /// <summary>
    /// Handler para que el usuario empresa pueda modificar las habilitaciones de una determinada oferta.
    /// /// </summary>
    public class ModifyHabilitationsHandler : AbstractHandler
    {

        /// <summary>
        /// Constructor de objetos ModifyHabilitationsHandler.
        /// </summary>
        /// <param name="channel"></param>
        public ModifyHabilitationsHandler(IMessageChannel channel)
        {
            this.Command = "/modificarhabilitaciones";
            this.messageChannel = channel;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        public override  bool InternalHandle(IMessage input)
        {
            if(CanHandle(input))
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
                    this.messageChannel.SendMessage("Pase por aquí el link que lleva a sus habilitaciones\n");
                    string habilitations = this.messageChannel.ReceiveMessage().Text;
                    offer.ChangeHabilitation(habilitations);
                    return true;
                }
            }
            return false;
        }
    }
}