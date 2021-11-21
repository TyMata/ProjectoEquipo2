using System;
using System.Text;

namespace ClassLibrary
{
    /// <summary>
    /// Handler encargado de delegar la accion de suspender una oferta.
    /// </summary>
    public class SuspendOfferHandlerCopy : AbstractHandler
    {
        public SuspendOfferState State {get; private set;}

        public SuspendOfferData Data {get; private set;} = new SuspendOfferData();

        private Company company;

        /// <summary>
        /// Constructor de objetos SuspendOfferHandler.
        /// </summary>
        /// <param name="channel"></param>
        public SuspendOfferHandlerCopy(IMessageChannel channel)
        {
            this.Command = "/suspenderoferta";
            this.messageChannel = channel;
            this.State = SuspendOfferState.Start;
            this.company = null;
        }
        /// <summary>
        /// Se encarga de pasarle por pantalla la lista de ofertas actuales y luego de recibir un Id
        /// de una oferta delega la accion de suspenderla.
        /// </summary>
        /// <param name="input"></param>
        public override bool InternalHandle(IMessage input, out string response)
        {
            if((State == SuspendOfferState.Start) && this.CanHandle(input))
            {
                this.State = SuspendOfferState.ShowActiveState;
                this.company = CompanyRegister.Instance.GetCompanyByUserId(input.Id);
                response = "Estas son tus ofertas actuales:\n";
                return true;
            }
            else if (State == SuspendOfferState.ShowActiveState)
            {   
                StringBuilder offers = new StringBuilder();
                foreach (Offer item in Market.Instance.ActualOfferList)
                {
                    this.State = SuspendOfferState.AskActiveOfferIdState;
                    offers.Append($"Id de la oferta: {item.Id}\n")
                            .Append($"Material de la oferta: {item.Material}\n")
                            .Append($"Cantidad: {item.QuantityMaterial}\n")
                            .Append($"Fecha de publicacion: {item.PublicationDate}\n")
                            .Append($"\n-----------------------------------------------\n\n");
                }
                response = offers.ToString();
                return true;
            }
            else if (State == SuspendOfferState.AskActiveOfferIdState)
            {
                this.State = SuspendOfferState.BoolIdAnswerState;
                response = "¿Cual es el Id de la que quiere suspender?";
                return true;
            }
            else if (State == SuspendOfferState.BoolIdAnswerState)
            {
                this.Data.id = Convert.ToInt32(input.Text);
                if (this.company.CompanyUsers.Exists(user => user.Id == this.Data.id))
                {
                    Market.Instance.SuspendOffer(this.Data.id);
                    this.State = SuspendOfferState.Start;
                    response = "La oferta ha sido suspendida.";  
                }
                else
                {
                    response = "No hay ninguna oferta publicada bajo el nombre de esta empresa.";
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
        /// Indica los diferentes estados que puede tener el comando AddCompanyHandler.
        /// </summary>
        public enum SuspendOfferState
        {
            Start,
            ShowActiveState,
            AskActiveOfferIdState,
            BoolIdAnswerState
        }

        public class SuspendOfferData
        {
            public int id {get; set;}
        }
    }
}
