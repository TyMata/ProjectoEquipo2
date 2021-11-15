using System;
using System.Text;

namespace ClassLibrary
{
    /// <summary>
    /// Handler encargado de delegar la accion de volver a activar una oferta suspendida
    /// </summary>
    public class ResumeOfferHandler : AbstractHandler
    {
        /// <summary>
        /// Constructor de objetos ResumeOfferHandler
        /// </summary>
        /// <param name="channel"></param>
        public ResumeOfferHandler(IMessageChannel channel)
        {
            this.Command = "/reanudaroferta";
            this.messageChannel = channel;
        }
        /// <summary>
        /// Se encarga de pasarle por pantalla la lista de ofertas actuales y luego de recibir un Id
        /// de una oferta delega la accion de volver a activarla.
        /// </summary>
        /// <param name="input"></param>
        public override void Handle(IMessage input)
        {
             if(this.CanHandle(input))
            {
                Company company = Singleton<CompanyRegister>.Instance.GetCompanyByUserId(input.Id);

                if(company.OfferRegister != null)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append($"Estas son tus ofertas suspendids actuales:\n");
                    foreach (Offer item in Singleton<Market>.Instance.ActualOfferList)
                    {
                        sb.Append($"Id de la oferta: {item.Id}\n")
                            .Append($"Material de la oferta: {item.Material}\n")
                            .Append($"Cantidad: {item.QuantityMaterial}\n")
                            .Append($"Fecha de publicacion: {item.PublicationDate}\n")
                            .Append($"\n-----------------------------------------------\n\n");
                    }
                    sb.Append("¿Cual es el Id de la que quiere activar?");
                    this.messageChannel.SendMessage(sb.ToString());
                    int id = Convert.ToInt32(this.messageChannel.ReceiveMessage().Text);
                    Singleton<Market>.Instance.SuspendOffer(id);
                    this.messageChannel.SendMessage($"La oferta Oferta se volvio a activar");
                }
                else 
                {
                    this.messageChannel.SendMessage("No hay ninguna oferta publicada bajo el nombre de esta empresa");
                }
            }     
        }
    }
}
