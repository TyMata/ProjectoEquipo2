using System;
using System.Text;

namespace ClassLibrary
{
    /// <summary>
    /// Handler para que el usuario empresa pueda modificar las habilitaciones de una determinada oferta.
    /// /// </summary>
    public class ModifyHabilitationsHandler : AbstractHandler
    {

        public ModifyState State { get; set; }
        public ModifyOfferData Data {get;set;}
        private Company company;

        /// <summary>
        /// Constructor de objetos ModifyHabilitationsHandler
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="response"></param>
        /// <returns></returns>
        public override bool InternalHandle(IMessage input, out string response)
        {
            if(this.State == ModifyState.Start && this.CanHandle(input))
            {
                this.company = CompanyRegister.Instance.GetCompanyByUserId(input.Id);
                StringBuilder offers = new StringBuilder("Que oferta desea modificar:\n");
                if(this.company.OfferRegister != null)
                {
                    foreach(Offer x in this.company.OfferRegister)
                    {
                        offers.Append($"Id : {x.Id}\n")
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
                this.Data.Offer = Convert.ToInt32(input.Text);
                this.State = ModifyState.Modification;
                response = "Pase por aquí el link que lleva a sus habilitaciones\n";

            }
            else if (this.State == ModifyState.Modification)
            {
                this.Data.Result = this.company.OfferRegister.Find(offer => offer.Id == this.Data.Offer);
                string habilitations = this.messageChannel.ReceiveMessage().Text;
                this.Data.Result.ChangeHabilitation(habilitations); 
                this.State = ModifyState.Start;
                response = "Las habilitaciones se han modificó";
                return true;
            }
            response = string.Empty;
            return false;
            
        }
    

        public enum ModifyState
        {
            Start,
            OfferList,
            Modification,

            
        }

        public class ModifyOfferData
        {
            /// <summary>
            /// La dirección que se ingresó en el estado AddressState.AddressPrompt.
            /// </summary>
            public int Offer { get; set; }

            /// <summary>
            /// El resultado de la búsqueda de la dirección ingresada.
            /// </summary>
            public Offer Result { get; set; }
        }
    }      
}
