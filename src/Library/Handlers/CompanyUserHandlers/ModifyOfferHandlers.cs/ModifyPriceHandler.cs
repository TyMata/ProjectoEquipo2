using System;
using System.Text;

namespace ClassLibrary
{
    /// <summary>
    /// Handler para que el usuario empresa pueda modificar el precio de una determinada oferta.
    /// </summary>
    public class ModifyPriceHandler : AbstractHandler
    {

        /// <summary>
        /// Estado para el handler de ModifyPriceHandler.
        /// </summary>
        /// <value></value>
        public ModifyState State { get; set; }

        /// <summary>
        /// Guarda la información que pasa el usuario por el chat cuando se utiliza el comando ModifyPriceHandler.
        /// </summary>
        /// <value></value>
        public ModifyOfferData Data { get; set; }
        private Company company;

        /// <summary>
        /// Constructor de objetos ModifyPriceHandler
        /// </summary>
        public ModifyPriceHandler()
        {
            this.Command = "/modificarprecio";
            this.State = ModifyState.Start;
            this.Data = new ModifyOfferData();
            this.company = null;
        }
        
        /// <summary>
        /// Se encarga de mostrar la lista de ofertas de la empresa y modificar el precio
        /// de la oferta indicada por el usuario.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="response"></param>
        /// <returns></returns>
        public override bool InternalHandle(IMessage input, out string response)
        {
            if(this.State == ModifyState.Start && this.CanHandle(input))
            {
                this.company = CompanyRegister.Instance.GetCompanyByUserId(input.Id);
                StringBuilder offers = new StringBuilder("¿Qué oferta desea modificar?\n");
                if(this.company != null && this.company.OfferRegister != null)
                {
                    foreach(Offer x in this.company.OfferRegister)
                    {
                        offers.Append($"Id: {x.Id}.\n")
                            .Append($"Material: {x.Material.Name} de {x.Material.Type}.\n")
                            .Append($"Unidad de medida: {x.UnitOfMeasure}.\n")
                            .Append($"Cantidad: {x.QuantityMaterial}.\n")
                            .Append($"Fecha de publicación: {x.PublicationDate}.\n")
                            .Append($"Divisa: {x.Currency}.\n")
                            .Append($"Precio: {x.TotalPrice}.\n")
                            .Append($"\n-----------------------------------------------\n\n");
                    }
                    offers.Append("Ingrese el Id de la oferta a modificar.\n");                       
                    this.State = ModifyState.OfferList;
                    response = offers.ToString();
                    return true;
                }
                else
                {
                    offers.Append($"No se encontró ninguna empresa a la que usted pertenezca.\n")
                        .Append($"Ingrese /menu si quiere volver a ver los comandos disponibles.");
                    response = offers.ToString() ;      
                    return true;
                }
            }
            else if(this.State == ModifyState.OfferList)
            {
                this.Data.OfferId = Convert.ToInt32(input.Text);
                this.State = ModifyState.Modification;
                response = "Ingrese el nuevo precio de la oferta.";
                return true;
            }
            else if(this.State == ModifyState.Modification)
            {
                // this.Data.Price = Convert.ToInt32(input.Text);
                this.Data.Result = this.company.OfferRegister.Find(offer => offer.Id == this.Data.OfferId);
                if(this.Data.Result != null)
                {
                    this.Data.Result.ChangePrice(Convert.ToInt32(input.Text));  
                    this.State = ModifyState.Start;
                    response = "El precio se ha modificado.";
                    return true; 
                }
                else
                {
                    response = "No se encontro ninguna oferta.";
                    return true;
                }
            }
            response = string.Empty;
            return false;
        }

        /// <summary>
        /// Metodo encargado de resetear el State y la Data del Handler.
        /// </summary>
        protected override void InternalCancel()
        {
            this.State = ModifyState.Start;
            this.Data = new ModifyOfferData();
        }

        /// <summary>
        /// Indica los diferentes estados que tiene ModifyPriceHandler.
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
        /// Representa los datos que va obteniendo el comando ModifyHabilitationsHandler en los diferentes estados.
        /// </summary>
        public class ModifyOfferData
        {
            /// <summary>
            /// La dirección que se ingresó en el estado ModifyState.OfferList.
            /// </summary>
            public int OfferId { get; set; }

            public int Price {get; set;}

            /// <summary>
            /// El resultado de la búsqueda de la oferta ingresada.
            /// </summary>
            public Offer Result { get; set; }
        }
    }
}