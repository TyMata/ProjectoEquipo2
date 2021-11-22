using System;
using System.Text;

namespace ClassLibrary
{
    /// <summary>
    /// Handler encargado de delegar la accion de retirar una oferta.
    /// </summary>
    public class RemoveOfferHandler: AbstractHandler
    {   
        /// <summary>
        /// Estado para el handler de RemoveOfferHandler.
        /// </summary>
        /// <value></value>
        public RemoveOfferState State {get; private set;}

        /// <summary>
        /// Guarda la información que pasa el usuario por el chat cuando se utiliza el comando ModifyQuantityHandler.
        /// </summary>
        /// <value></value>
        public RemoveOfferData Data {get; private set;} = new RemoveOfferData();

        private Company company;

        /// <summary>
        /// Constructor de objetos RemoveOfferHandler.
        /// </summary>
        public RemoveOfferHandler()
        {
            this.Command = "/retiraroferta";           
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
            else if (State == RemoveOfferState.AskActiveOfferIdState)           //TODO Todo en un mismo if??
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

        /// <summary>
        /// Indica los diferentes estados que tiene RemoveOfferHandler.
        /// </summary>
        public enum RemoveOfferState
        {
            /// <summary>
            /// El estado inicial del comando. Aquí pregunta por el ID de la oferta oferta que se quiere 
            /// modificar y le muestra una lista de las ofertas actuales de la empresa.
            /// </summary>
            Start,
            ShowActiveState,
            AskActiveOfferIdState,
            BoolIdAnswerState
        }

        /// <summary>
        /// Representa los datos que va obteniendo el comando RemoveOfferHandler en los diferentes estados.
        /// </summary>
        public class RemoveOfferData
        {
            /// <summary>
            /// El ID que se ingresó en el estado RemoveOfferHandler.OfferList.
            /// </summary>
            public int id {get; set;}
        }
    }
}