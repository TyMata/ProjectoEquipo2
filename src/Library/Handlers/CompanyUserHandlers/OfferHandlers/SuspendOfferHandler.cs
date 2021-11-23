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
        /// Estado para este handler.
        /// </summary>
        /// <value></value>
        public SuspendOfferState State {get; private set;}

        /// <summary>
        /// Clase para guardar la informacion que envia el usuario por el chat.
        /// </summary>
        /// <returns></returns>
        public SuspendOfferData Data {get; private set;} = new SuspendOfferData();

        private Company company;

        /// <summary>
        /// Constructor de objetos SuspendOfferHandler.
        /// </summary>
        public SuspendOfferHandler()
        {
            this.Command = "/suspenderoferta";
            this.State = SuspendOfferState.Start;
            this.company = null;
        }

        /// <summary>
        /// Se encarga de pasar por pantalla la lista de ofertas actuales y luego de recibir un Id
        /// de una oferta delega la accion de suspenderla.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="response"></param>
        /// <returns></returns>
        public override bool InternalHandle(IMessage input, out string response)
        {
            if((State == SuspendOfferState.Start) && this.CanHandle(input))
            {
                this.State = SuspendOfferState.ActiveOfferIdState;
                this.company = CompanyRegister.Instance.GetCompanyByUserId(input.Id);
                StringBuilder offers = new StringBuilder("Estas son tus ofertas actuales:\n");
                foreach (Offer item in Market.Instance.ActualOfferList)
                {
                    offers.Append($"Id de la oferta: {item.Id}\n")
                            .Append($"Material de la oferta: {item.Material}\n")
                            .Append($"Cantidad: {item.QuantityMaterial}\n")
                            .Append($"Fecha de publicacion: {item.PublicationDate}\n")
                            .Append($"\n-----------------------------------------------\n\n");
                }
                offers.Append("¿Cual es el Id de la que quiere suspender?");
                response = offers.ToString();
                return true;
            }
            else if (State == SuspendOfferState.ActiveOfferIdState)
            {
                this.Data.Id = Convert.ToInt32(input.Text);
                if (this.company.OfferRegister.Exists(Offer => Offer.Id == this.Data.Id))
                {
                    Market.Instance.SuspendOffer(this.Data.Id);
                    this.State = SuspendOfferState.Start;
                    response = "La oferta ha sido suspendida.\n";  
                }
                else
                {
                    response = "No hay ninguna oferta publicada bajo el nombre de esta empresa.\n";
                    this.State = SuspendOfferState.Start;
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
            this.State = SuspendOfferState.Start;
            this.Data = new SuspendOfferData();
        }

        /// <summary>
        /// Enumerado de los estados de este handler.
        /// </summary>
        public enum SuspendOfferState
        {
            /// <summary>
            /// Estado con el que comienza. Se le muestra todas las ofertas actuales de su empresa y se le pregunta por el Id de la que quiere Suspender.
            /// </summary>
            Start,
            /// <summary>
            /// Estado en donde se guarda el Id que envio el usuario por el chat, se busca en las ofertas actuales de la empresa si esta existe y si es asi se la suspende en el mercado.
            /// </summary>
            ActiveOfferIdState,
           
        }

        /// <summary>
        /// Clase para guardar la información que envia el usuario por el chat.
        /// </summary>
        public class SuspendOfferData
        {
            /// <summary>
            /// Se guarda el Id de la oferta  a suspender.
            /// </summary>
            /// <value></value>
            public int Id {get; set;}
        }
    }
}
