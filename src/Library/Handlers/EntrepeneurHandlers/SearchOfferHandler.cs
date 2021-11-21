using System.Text;

namespace ClassLibrary
{
    /// <summary>
    /// Handler encargado de delegar la accion de buscar ofertas por keywords
    /// </summary>
    public class SearchOfferHandler : AbstractHandler
    {   
        public SearchOfferState State {get; private set;}

        public SearchOfferData Data {get; private set;} = new SearchOfferData();

        private Company company;

        /// <summary>
        /// Constructor de objetos SearchOfferByKeyWordsHandler.
        /// </summary>
        /// <param name="channel"></param>
        public SearchOfferHandler(IMessageChannel channel)
        {
            this.Command = "/buscaroferta";
            this.messageChannel = channel;
            this.State = SearchOfferState.Start;
        }

        
        public override bool InternalHandle(IMessage input, out string response)
        {
            if ((State == SearchOfferState.Start) && this.CanHandle(input))
            {
                response = "Escriba las palabras claves de la oferta a buscar";  //TODO: Como hacer lo de las SearchOffer.
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
        public int id {get; set;}
        }
    }
}