using System;
using System.Text;

namespace ClassLibrary
{
    /// <summary>
    /// Handler para que el usuario empresa pueda modificar la cantidad de material en una determinada oferta
    /// </summary>
    public class ModifyQuantityHandler : AbstractHandler
    {
         public ModifyState State { get; set; }
        public ModifyOfferData Data {get;set;}
        private Company company;

        /// <summary>
        /// Constructor de objetos ModifyQuantityHandler
        /// </summary>
        /// <param name="channel"></param>
        public ModifyQuantityHandler(IMessageChannel channel)
        {
            this.Command = "/modificarcantidad";
            this.messageChannel = channel;
            this.State = ModifyState.Start;
            this.Data = new ModifyOfferData();
            this.company = null;
        }

        /// <summary>
        /// Se encarga de mostrar la lista de ofertas de la empresa y modificar la cantidad de la oferta determinada
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public override bool InternalHandle(IMessage input, out string response)
        {
            if (this.State == ModifyState.Start && CanHandle(input))
            {
                Company company = CompanyRegister.Instance.GetCompanyByUserId(input.Id);
                StringBuilder offers = new StringBuilder("Que oferta desea modificar:\n");
                if(company.OfferRegister != null)
                {
                    foreach(Offer x in company.OfferRegister)
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
                response = "Ingrese la nueva cantidad de la oferta:\n";
                return true;
            }
            else if(this.State == ModifyState.Modification)
            {
                int quantity = Convert.ToInt32(input.Text);
                this.Data.Result = this.company.OfferRegister.Find(offer => offer.Id == this.Data.Offer);
                this.Data.Result.ChangeQuantity(quantity);
                this.State = ModifyState.Start;
                response = "La cantidad se ha modificado";
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
