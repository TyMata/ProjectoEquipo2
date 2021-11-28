using System;
using System.Text;

namespace ClassLibrary
{
    /// <summary>
    /// Handler encargado de delegar la accion de volver a activar una oferta suspendida.
    /// </summary>
    public class ResumeOfferHandler : AbstractHandler
    {
        /// <summary>
        /// Estado para este handler.
        /// </summary>
        /// <value></value>
        public ResumeOfferState State {get; private set;}

        /// <summary>
        /// Clase para guardar la informacion que envia el usuario por el chat cuando se le pregunta
        /// </summary>
        /// <returns></returns>
        public ResumeOfferData Data {get; private set;} = new ResumeOfferData();

        private Company company;

        /// <summary>
        /// Constructor de objetos ResumeOfferHandler.
        /// </summary>
        public ResumeOfferHandler()
        {
            this.Command = "/reanudaroferta";
            this.State = ResumeOfferState.Start;
            this.company = null;
        }

        /// <summary>
        /// Se encarga de pasar por pantalla la lista de ofertas actuales y luego de recibir un Id
        /// de una oferta delega la accion de volver a activarla.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="response"></param>
        /// <returns></returns>
        public override bool InternalHandle(IMessage input, out string response)
        {
            if((State == ResumeOfferState.Start) && this.CanHandle(input))
            {
                this.company = CompanyRegister.Instance.GetCompanyByUserId(input.Id);
                StringBuilder offers = new StringBuilder();
                if(this.company != null)
                { 
                    foreach (Offer item in this.company.OfferRegister)
                    {
                        if(!item.Availability)
                        {
                            offers.Append($"Id de la oferta: {item.Id}.\n")
                                .Append($"Material de la oferta: {item.Material.Name} de {item.Material.Type}.\n")
                                .Append($"Cantidad: {item.QuantityMaterial}.\n")
                                .Append($"Precio: {item.TotalPrice}.\n")
                                .Append($"Fecha de publicación: {item.PublicationDate}.\n")
                                .Append($"\n-----------------------------------------------\n\n");
                        }
                    }
                    this.State = ResumeOfferState.SuspendedOfferIdState;
                    offers.Append("¿Cuál es el Id de la oferta que quiere activar?\n");
                    response = offers.ToString();
                    return true;
                }
                else
                {
                    offers.Append($"No se encontró ninguna empresa a la que usted pertenezca.\n")
                        .Append($"Ingrese /menu si quiere volver a ver los comandos disponibles.");
                    response = offers.ToString();
                    return true;
                }
            }
            else if (State == ResumeOfferState.SuspendedOfferIdState)
            {  
                this.Data.Id = Convert.ToInt32(input.Text);
                if (this.company.OfferRegister.Exists(offer => offer.Id == this.Data.Id))
                {
                    Market.Instance.ResumeOffer(this.Data.Id);
                    this.State = ResumeOfferState.Start;
                    response = "La oferta se ha activado nuevamente.";  
                }
                else
                {
                    response = "No hay ninguna oferta publicada bajo el nombre de esta empresa.";
                    this.State = ResumeOfferState.Start;
                }
                return true;
            }
            else
            {
                response = string.Empty;
                return false;
            }
        }

        protected override void InternalCancel()
        {
            this.State = ResumeOfferState.Start;
            this.Data = new ResumeOfferData();
        }

        /// <summary>
        /// Enumerado de los estado de este handler
        /// </summary>
        public enum ResumeOfferState
        {
            /// <summary>
            /// Estado con el que comienza el handler, en este se le muestra sus ofertas actuales y pide el id de la que quiere reanudar.
            /// </summary>
            Start,
            
            /// <summary>
            /// Estado en donde se guarda la Id que envio el usuario y se reanuda la oferta en el mercado.
            /// </summary>
            SuspendedOfferIdState,
            
        }

        /// <summary>
        /// Guarda la id que envia el usuario por el chat para luego ser usada.
        /// </summary>
        public class ResumeOfferData
        {
            /// <summary>
            /// Se guarada la Id.
            /// </summary>
            /// <value></value>
            public int Id {get; set;}
        }
    }
}
