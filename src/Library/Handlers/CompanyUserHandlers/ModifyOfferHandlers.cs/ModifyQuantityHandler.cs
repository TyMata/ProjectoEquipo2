using System;
using System.Text;

namespace ClassLibrary
{
    /// <summary>
    /// Handler para que el usuario empresa pueda modificar la cantidad de material en una determinada oferta.
    /// </summary>
    public class ModifyQuantityHandler : AbstractHandler
    {

        /// <summary>
        /// Estado para el handler de ModifyQuantityHandler.
        /// </summary>
        /// <value></value>
        public ModifyState State { get; set; }

        /// <summary>
        /// Guarda la información que pasa el usuario por el chat cuando se utiliza el comando ModifyQuantityHandler.
        /// </summary>
        /// <value></value>
        public ModifyOfferData Data {get;set;}
        private Company company;

        /// <summary>
        /// Constructor de objetos ModifyQuantityHandler.
        /// </summary>
        public ModifyQuantityHandler()
        {
            this.Command = "/modificarcantidad";
            this.State = ModifyState.Start;
            this.Data = new ModifyOfferData();
            this.company = null;
        }

        /// <summary>
        /// Se encarga de mostrar la lista de ofertas de la empresa y modificar la cantidad
        /// de materiales de la oferta indicada por el usuario.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="response"></param>
        /// <returns></returns>
        public override bool InternalHandle(IMessage input, out string response)
        {
            if (this.State == ModifyState.Start && CanHandle(input))
            {
                this.company = CompanyRegister.Instance.GetCompanyByUserId(input.Id);
                StringBuilder offers = new StringBuilder("Que oferta desea modificar?\n");
                if(this.company != null && this.company.OfferRegister != null)
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
                    offers.Append("Ingrese el Id de la oferta a modificar:\n"); 
                    this.State = ModifyState.OfferList;
                    response = offers.ToString();
                    return true; 
                }   
                
                else
                {
                    offers.Append($"No se encontro ninguna empresa a la que usted pertenezca.\n")
                        .Append($"Ingrese /menu si quiere volver a ver los comandos disponibles\n");
                    response = offers.ToString() ;      
                    return true;
                }
            }
            else if(this.State == ModifyState.OfferList)
            {
                this.Data.Offer = Convert.ToInt32(input.Text);
                this.State = ModifyState.Modification;
                response = "Ingrese la nueva cantidad de materiales de la oferta:\n";
                return true;
            }
            else if(this.State == ModifyState.Modification)
            {
                int quantity = Convert.ToInt32(input.Text);
                this.Data.Result = this.company.OfferRegister.Find(offer => offer.Id == this.Data.Offer);
                this.Data.Result.ChangeQuantity(quantity);
                this.State = ModifyState.Start;
                response = "La cantidad de materiales se ha modificado";
                return true;
            }
            response = string.Empty;
            return false;
        }
        
        protected override void InternalCancel()
        {
            this.State = ModifyState.Start;
            this.Data = new ModifyOfferData();
        }
        
        /// <summary>
        /// Indica los diferentes estados que tiene ModifyQuantityHandler.
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
        /// Representa los datos que va obteniendo el comando ModifyQuantityHandler en los diferentes estados.
        /// </summary>
        public class ModifyOfferData
        {
            /// <summary>
            /// El ID que se ingresó en el estado ModifyState.OfferList.
            /// </summary>
            public int Offer { get; set; }

            /// <summary>
            /// El resultado de la búsqueda de la oferta ingresada.
            /// </summary>
            public Offer Result { get; set; }
        }  
    } 
}