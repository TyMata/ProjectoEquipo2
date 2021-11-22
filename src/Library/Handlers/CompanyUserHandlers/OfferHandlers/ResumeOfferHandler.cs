using System;
using System.Text;

namespace ClassLibrary
{
    /// <summary>
    /// Handler encargado de delegar la accion de volver a activar una oferta suspendida.
    /// </summary>
    public class ResumeOfferHandler : AbstractHandler
    {
        public ResumeOfferState State {get; private set;}

        public ResumeOfferData Data {get; private set;} = new ResumeOfferData();

        private Company company;
        /// <summary>
        /// Constructor de objetos ResumeOfferHandler.
        /// </summary>
        /// <param name="channel"></param>
        public ResumeOfferHandler()
        {
            this.Command = "/reanudaroferta";
            this.State = ResumeOfferState.Start;
            this.company = null;
        }
        /// <summary>
        /// Se encarga de pasarle por pantalla la lista de ofertas actuales y luego de recibir un Id
        /// de una oferta delega la accion de volver a activarla.
        /// </summary>
        /// <param name="input"></param>
        public override bool InternalHandle(IMessage input, out string response)
        {
            if((State == ResumeOfferState.Start) && this.CanHandle(input))
            {
                this.State = ResumeOfferState.ShowSuspendedState;
                this.company = CompanyRegister.Instance.GetCompanyByUserId(input.Id);
                response = "Estas son tus ofertas suspendidas actuales:\n";
                return true;
            }
            else if (State == ResumeOfferState.ShowSuspendedState)
            {   
                StringBuilder offers = new StringBuilder();
                foreach (Offer item in Market.Instance.ActualOfferList)
                {
                    this.State = ResumeOfferState.AskSuspendedOfferIdState;
                    offers.Append($"Id de la oferta: {item.Id}\n")
                            .Append($"Material de la oferta: {item.Material}\n")
                            .Append($"Cantidad: {item.QuantityMaterial}\n")
                            .Append($"Fecha de publicacion: {item.PublicationDate}\n")
                            .Append($"\n-----------------------------------------------\n\n");
                }
                response = offers.ToString();
                return true;
            }
            else if (State == ResumeOfferState.AskSuspendedOfferIdState)
            {
                this.State = ResumeOfferState.BoolIdAnswerState;
                response = "¿Cual es el Id de la que quiere activar?";
                return true;
            }
            else if (State == ResumeOfferState.BoolIdAnswerState)
            {
                this.Data.id = Convert.ToInt32(input.Text);
                if (this.company.CompanyUsers.Exists(user => user.Id == this.Data.id))
                {
                    Market.Instance.SuspendOffer(this.Data.id);
                    this.State = ResumeOfferState.Start;
                    response = "La oferta se ha activado nuevamente";  
                }
                else
                {
                    response = "No hay ninguna oferta publicada bajo el nombre de esta empresa";
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

        public enum ResumeOfferState
        {
            Start,
            ShowSuspendedState,
            AskSuspendedOfferIdState,
            BoolIdAnswerState
        }

        public class ResumeOfferData
        {
            public int id {get; set;}
        }
    }
}
