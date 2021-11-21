using System;
using System.Text;

namespace ClassLibrary
{   
    /// <summary>
    /// Handler encargado de delegar la accion de suspender una oferta.
    /// </summary>
    public class SuspendOfferHandler : AbstractHandler
    {
        /// <summary>
        /// Constructor de objetos SuspendOfferHandler.
        /// </summary>
        /// <param name="channel"></param>
        public SuspendOfferHandler(IMessageChannel channel)
        {
            this.Command = "/suspenderoferta";
            this.messageChannel = channel;
        }
        /// <summary>
        /// Se encarga de pasarle por pantalla la lista de ofertas actuales y luego de recibir un Id
        /// de una oferta delega la accion de suspenderla.
        /// </summary>
        /// <param name="input"></param>
        public override bool InternalHandle(IMessage input)
        {
            if(this.CanHandle(input))
            {
                Company company =CompanyRegister.Instance.GetCompanyByUserId(input.Id);
                if(company.OfferRegister != null)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append($"Estas son tus ofertas actuales:\n");
                    foreach (Offer item in company.OfferRegister) 
                    {
                        sb.Append($"Id de la oferta: {item.Id}\n")
                            .Append($"Material de la oferta: {item.Material}\n")
                            .Append($"Cantidad: {item.QuantityMaterial}\n")
                            .Append($"Fecha de publicacion: {item.PublicationDate}\n")
                            .Append($"\n-----------------------------------------------\n\n");
                    }
                    sb.Append("¿Cual es el Id de la que quiere suspender?");
                    this.messageChannel.SendMessage(sb.ToString());
                    int id = Convert.ToInt32(this.messageChannel.ReceiveMessage().Text);
                    Market.Instance.SuspendOffer(id);
                    this.messageChannel.SendMessage($"La oferta Oferta se suspendio del mercado");
                    return true;
                }
                else 
                {
                    this.messageChannel.SendMessage("No hay ninguna oferta publicada bajo el nombre de esta empresa");
                }
            }
            return false;
        }
    }
}