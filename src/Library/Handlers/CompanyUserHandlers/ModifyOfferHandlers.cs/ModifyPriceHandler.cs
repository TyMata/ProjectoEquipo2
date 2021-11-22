using System;
using System.Text;

namespace ClassLibrary
{
    /// <summary>
    /// Handler para que el usuario empresa pueda modificar el precio de una determinada oferta.
    /// </summary>
    public class ModifyPriceHandler : AbstractHandler
    {
        public ModifyState State { get; set; }
        public ModifyOfferData Data {get;set;}
        private Company company;

        /// <summary>
        /// Constructor de objetos ModifyPriceHandler
        /// </summary>
        /// <param name="channel"></param>
        public ModifyPriceHandler()
        {
            this.Command = "/modificarprecio";
            this.State = ModifyState.Start;
            this.Data = new ModifyOfferData();
            this.company = null;
        }
        
        /// <summary>
        ///  Handle
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public override bool InternalHandle(IMessage input, out string response)
        {
            if(this.State == ModifyState.Start && this.CanHandle(input))
            {
                this.company = CompanyRegister.Instance.GetCompanyByUserId(input.Id);
                StringBuilder offers = new StringBuilder("Estas son todas sus ofertas:\n");
                if(this.company.OfferRegister != null)
                {
                    foreach(Offer x in this.company.OfferRegister)
                    {
                        offers.Append($"Id : {x.Id}\n")
                            .Append($"Material : {x.Material}\n")
                            .Append($"Cantidad: {x.QuantityMaterial}\n")
                            .Append($"Fecha de publicacion: {x.PublicationDate}\n")
                            .Append($"Precio: {x.TotalPrice}\n")
                            .Append($"\n-----------------------------------------------\n\n")
                            .Append("Cual quiere modificar?\n\n Ingrese el Id de esta:\n");
                    }                       
                    this.State = ModifyState.OfferList;
                    response = offers.ToString();
                    return true;
                }
            }
            else if(this.State == ModifyState.OfferList)
            {
                this.Data.Offer = Convert.ToInt32(input.Text);
                this.State = ModifyState.Modification;
                response = "Ingrese el nuevo precio de la oferta:\n";
                return true;
            }
            else if(this.State == ModifyState.Modification)
            {
                int price = Convert.ToInt32(input.Text);
                this.Data.Result = this.company.OfferRegister.Find(offer => offer.Id == this.Data.Offer);
                this.Data.Result.ChangePrice(price);
                this.State = ModifyState.Start;
                response = "El precio se ha modificado";
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