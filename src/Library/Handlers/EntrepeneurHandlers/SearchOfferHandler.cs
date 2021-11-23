using System;
using System.Text;
using System.Collections.Generic;

namespace ClassLibrary
{
    /// <summary>
    /// Handler encargado de delegar la accion de buscar ofertas por keywords
    /// </summary>
    public class SearchOfferHandler : AbstractHandler
    {   
        public SearchOfferState State {get; private set;}

        public SearchOfferData Data {get; private set;}

        /// <summary>
        /// Constructor de objetos SearchOfferByKeyWordsHandler.
        /// </summary>
        /// <param name="channel"></param>
        public SearchOfferHandler()
        {
            this.Command = "/buscaroferta";
            this.State = SearchOfferState.Start;
            this.Data = new SearchOfferData();
        }

        
        public override bool InternalHandle(IMessage input, out string response)
        {
            if ((State == SearchOfferState.Start) && this.CanHandle(input))
            {
                this.State = SearchOfferState.ShowActiveState;
                response = "Escriba las palabras claves de la oferta a buscar";  //TODO: Como hacer lo de las SearchOffer.
                return true;
            }
            else if(State == SearchOfferState.ShowActiveState)
            {
                if (input.Text == "/menu")
                {
                    this.State = SearchOfferState.Start;
                    response = "Volviendo al menú...";
                    return true;
                }
                this.State = SearchOfferState.AskActiveOfferIdState;
                string keyword = input.Text;
                this.Data.Offers = Market.Instance.SearchOffers(keyword);
                StringBuilder offers = new StringBuilder("Estas son las ofertas encontras con esa palabra clave:\n");
                foreach (Offer item in this.Data.Offers)
                {
                    offers.Append($"Id de la oferta: {item.Id}\n")
                            .Append($"Material de la oferta: {item.Material}\n")
                            .Append($"Cantidad: {item.QuantityMaterial}\n")
                            .Append($"Fecha de publicacion: {item.PublicationDate}\n")
                            .Append($"\n-----------------------------------------------\n\n")
                            .Append($"Si desea seleccionar alguna de las ofertas disponibles, por favor escriba su Id\n\n")
                            .Append($"De lo contrario escriba /menu para volver al menú principal\n\n");
                }
                response = offers.ToString();
                return true;
            }
            else if (this.State == SearchOfferState.AskActiveOfferIdState)
            {
                if (input.Text == "/menu")
                {
                    this.State = SearchOfferState.Start;
                    response = "Volviendo al menú...";
                    return true;
                }
                this.State = SearchOfferState.Start;
                int Id = Convert.ToInt32(input.Text);
                this.Data.Offers = Market.Instance.SearchOffers(Id.ToString());
                if (this.Data.Offers != null)
                {
                    StringBuilder searchResult = new StringBuilder("Resultado de la búsqueda:\n");
                    foreach (Offer item in this.Data.Offers)
                    {
                        searchResult.Append($"Id de la oferta: {item.Id}\n")
                                    .Append($"Material de la oferta: {item.Material}\n")
                                    .Append($"Cantidad: {item.QuantityMaterial}\n")
                                    .Append($"Fecha de publicacion: {item.PublicationDate}\n")
                                    .Append($"\n-----------------------------------------------\n\n");
                    }
                    response = searchResult.ToString();
                }
                else
                {
                    response = $"No se encontró ninguna oferta con el Id {this.Data.Offers}";
                }
                return true;
            }
            response = string.Empty;
            return false;
        }

        /// <summary>
        /// Retorna este handler al estado inicial.
        /// </summary>
        protected override void InternalCancel()
        {
            this.State = SearchOfferState.Start;
            this.Data = new SearchOfferData();
        }

        /// <summary>
        /// Indica los diferentes estados que puede tener el comando SearchOfferHandler.
        /// </summary>
        public enum SearchOfferState
        {
            /// <summary>
            /// El estado inicial del comando. Aquí pide las palabras claves de la ofertas a buscar.
            /// </summary>
            Start,

            /// <summary>
            /// Luego de pedir las palabras claves de las ofertas. En este estado el comando devuelve todas las ofertas
            /// existentes, que cumplan con los requisitos. Además sugiere la posibilidad de seleccionar una oferta única por medio de una Id.
            /// </summary>
            ShowActiveState,
            /// <summary>
            /// 
            /// </summary>
            AskActiveOfferIdState,
        }

        /// <summary>
        /// Representa los datos que va obteniendo el comando SearchOfferHandler en los diferentes estados.
        /// </summary>
        public class SearchOfferData
        {
            /// <summary>
            /// La lista de ofertas que se mostrará al emprendedor una vez haya enviado las palabras claves.
            /// </summary>
            /// <value></value>
            public List<Offer> Offers {get;set;}
        }
    }
}