using System;
using System.Text;

namespace ClassLibrary
{
    /// <summary>
    /// Handler encargado de delegar la accion de retirar una oferta.
    /// </summary>
    public class RemoveOfferHandlerCopy: AbstractHandler
    {   
        public RemoveOfferState State {get; private set;}

        public RemoveOfferData Data {get; private set;} = new RemoveOfferData();

        private Company company;

        /// <summary>
        /// Constructor de objetos RemoveOfferHandler.
        /// </summary>
        /// <param name="channel"></param>
        public RemoveOfferHandlerCopy(IMessageChannel channel)
        {
            this.Command = "/retiraroferta";            
            this.messageChannel = channel ;
            this.State = RemoveOfferState.Start;
            this.company = null;
        }
        /// <summary>
        /// Se encarga de pasar por pantalla la lista de ofertas actuales y luego de recibir un Id
        /// de una oferta delega la accion de eliminarla.
        /// De no existir la oferta le avisa por pantalla al usuario.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="response"></param>
        public override bool InternalHandle(IMessage input, out string response)
        {
            if((State == RemoveOfferState.Start) && this.CanHandle(input))
            {
                this.State = RemoveOfferState.ShowActiveState;
                this.company = CompanyRegister.Instance.GetCompanyByUserId(input.Id);
                response = "Estas son tus ofertas actuales:\n";
                return true;
            }
            else if (State == RemoveOfferState.ShowActiveState)
            {   
                StringBuilder offers = new StringBuilder();
                foreach (Offer item in Market.Instance.ActualOfferList)
                {
                    this.State = RemoveOfferState.AskActiveOfferIdState;
                    offers.Append($"Id de la oferta: {item.Id}\n")
                            .Append($"Material de la oferta: {item.Material}\n")
                            .Append($"Cantidad: {item.QuantityMaterial}\n")
                            .Append($"Fecha de publicacion: {item.PublicationDate}\n")
                            .Append($"\n-----------------------------------------------\n\n");
                }
                response = offers.ToString();
                return true;
            }
            else if (State == RemoveOfferState.AskActiveOfferIdState)
            {
                this.State = RemoveOfferState.BoolIdAnswerState;
                response = "¿Cual es el Id de la oferta a retirar?";
                return true;
            }
            else if (State == RemoveOfferState.BoolIdAnswerState)
            {
                this.Data.id = Convert.ToInt32(input.Text);
                if (this.company.CompanyUsers.Exists(user => user.Id == this.Data.id))
                {
                    Market.Instance.SuspendOffer(this.Data.id);
                    this.State = RemoveOfferState.Start;
                    response = "La oferta ha sido retirada del mercado.";  
                }
                else
                {
                    response = "No hay ninguna oferta publicada bajo el nombre de esta empresa.";
                    this.State = RemoveOfferState.Start;
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
            this.State = RemoveOfferState.Start;
            this.Data = new RemoveOfferData();
        }

        public enum RemoveOfferState
        {
            Start,
            ShowActiveState,
            AskActiveOfferIdState,
            BoolIdAnswerState
        }

        public class RemoveOfferData
        {
        public int id {get; set;}
        }
    }
}