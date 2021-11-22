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

        private Company company;

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
            else if( this.State == SearchOfferState.ShowActiveState)
            {
                string keyword = input.Text;
                this.Data.Offers = Market.Instance.SearchOffers(keyword);
                StringBuilder offers = new StringBuilder("Estas son las ofertas encontras con esa palabra clave:\n");
                foreach (Offer item in this.Data.Offers)
                {
                    offers.Append($"Id de la oferta: {item.Id}\n")
                            .Append($"Material de la oferta: {item.Material}\n")
                            .Append($"Cantidad: {item.QuantityMaterial}\n")
                            .Append($"Fecha de publicacion: {item.PublicationDate}\n")
                            .Append($"\n-----------------------------------------------\n\n");
                }
                response = offers.ToString();
                return true;
            }
            response = string.Empty;
            return false;
        }

        protected override void InternalCancel()
        {
            this.State = SearchOfferState.Start;
            this.Data = new SearchOfferData();
        }

        public enum SearchOfferState
        {
            Start,
            ShowActiveState,
            AskActiveOfferIdState,
            BoolIdAnswerState
        }

        public class SearchOfferData
        {
            public int Id {get; set;}
            public List<Offer> Offers {get;set;}
        }
    }
}