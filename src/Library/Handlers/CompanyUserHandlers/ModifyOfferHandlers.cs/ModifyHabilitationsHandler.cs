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
        /// Estado para el handler de AddCompany.
        /// </summary>
        /// <value></value>
        public ModifyState State { get; set; }

        /// <summary>
        /// Guarda los prompts ingresados por el  usuario.
        /// </summary>
        /// <value></value>
        public ModifyOfferData Data {get;set;}
        private Company company;

        /// <summary>
        /// Constructor de objetos ModifyHabilitationsHandler.
        /// </summary>
        /// <param name="channel"></param>
        public ModifyHabilitationsHandler(IMessageChannel channel)
        {
            this.Command = "/modificarhabilitaciones";
            this.messageChannel = channel;
            this.State = ModifyState.Start;
            this.Data = new ModifyOfferData();
            this.company = null;
        }

        public override bool InternalHandle(IMessage input, out string response)
        {
            if(this.State == ModifyState.Start && this.CanHandle(input))
            {
                this.company = CompanyRegister.Instance.GetCompanyByUserId(input.Id);
                StringBuilder offers = new StringBuilder("Que oferta desea modificar?\n");
                if(this.company.OfferRegister != null)
                {
                    foreach(Offer x in this.company.OfferRegister)
                    {
                        offers.Append($"ID : {x.Id}\n")
                            .Append($"Material : {x.Material}\n")
                            .Append($"Cantidad: {x.QuantityMaterial}\n")
                            .Append($"Fecha de publicacion: {x.PublicationDate}\n")
                            .Append($"Precio: {x.TotalPrice}\n")
                            .Append($"\n-----------------------------------------------\n\n");
                    }   
                }            
                this.State = ModifyState.OfferList;
                response = offers.ToString();
                return true;
            }
            else if(this.State == ModifyState.OfferList)
            {
                this.Data.OfferId = Convert.ToInt32(input.Text);
                this.State = ModifyState.Modification;
                response = "Pase por aquí el link que lleva a sus habilitaciones\n";

            }
            else if (this.State == ModifyState.Modification)
            {
                this.Data.Result = this.company.OfferRegister.Find(offer => offer.Id == this.Data.OfferId);
                string habilitations = this.messageChannel.ReceiveMessage().Text;
                this.Data.Result.ChangeHabilitation(habilitations); 
                this.State = ModifyState.Start;
                response = "Las habilitaciones se han modificó";
                return true;
            }
            response = string.Empty;
            return false;
        }
    
        /// <summary>
        /// Indica los diferentes estados que tiene ModifyHabilitationsHandler.
        /// </summary>
        public enum ModifyState
        {

            /// <summary>
            /// El estado inicial del comando. Aquí pregunta por el ID de la oferta oferta que se quiere 
            /// modificar y le muestra una lista de las ofertas actuales de la empresa.
            /// </summary>
            Start,

            /// <summary>
            /// En este estado se obtiene el id y pregunta por el link de las habilitaciones.
            /// </summary>
            OfferList,

            /// <summary>
            /// En este estado se obtiene el link. delega el proceso de modificacion y le informa al usuario.
            /// </summary>
            Modification
        }

        /// <summary>
        /// Clase que representa los prompts recibidos en los diferentes estados.
        /// </summary>
        public class ModifyOfferData
        {
            /// <summary>
            /// La dirección que se ingresó en el estado ModifyStateOfferList.
            /// </summary>
            public int OfferId { get; set; }

            /// <summary>
            /// El resultado de la búsqueda de la oferta ingresada.
            /// </summary>
            public Offer Result { get; set; }
        }
    }      
}
