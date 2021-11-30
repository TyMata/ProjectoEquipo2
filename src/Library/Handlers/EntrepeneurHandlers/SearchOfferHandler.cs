using System;
using System.Text;
using System.Collections.Generic;

namespace ClassLibrary
{
    /// <summary>
    /// Handler encargado de delegar la accion de buscar ofertas por keywords.
    /// </summary>
    public class SearchOfferHandler : AbstractHandler
    {  
        /// <summary>
        /// Estado para el handler de SearchOfferHandler.
        /// </summary>
        /// <value></value>
        public SearchOfferState State {get; private set;}
        /// <summary>
        /// Guarda la información que pasa el usuario por el chat cuando se utiliza el comando SearchOfferHandler.
        /// </summary>
        /// <value></value>
        public SearchOfferData Data {get; private set;}

        /// <summary>
        /// Constructor de objetos SearchOfferByKeyWordsHandler.
        /// </summary>
        
        public SearchOfferHandler()
        {
            this.Command = "/buscaroferta";
            this.State = SearchOfferState.Start;
            this.Data = new SearchOfferData();
        }

        /// <summary>
        /// /// Se encarga de mostrar la lista de ofertas asociadas a las keywords.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="response"></param>
        /// <returns></returns>
        public override bool InternalHandle(IMessage input, out string response)
        {
            try
            {
                if ((State == SearchOfferState.Start) && this.CanHandle(input))
                {
                    this.State = SearchOfferState.ShowActiveState;
                    response = "Escriba la palabra clave de las ofertas a buscar";  //TODO: Como hacer lo de las SearchOffer.
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
                    StringBuilder offers = new StringBuilder("Estas son las ofertas encontradas con esa palabra clave:\n");
                    foreach (Offer item in this.Data.Offers)
                    {
                        offers.Append($"Id de la oferta: {item.Id}.\n")
                                .Append($"Material de la oferta: {item.Material.Name} de {item.Material.Type}.\n")
                                .Append($"Cantidad: {item.QuantityMaterial}.\n")
                                .Append($"Fecha de publicación: {item.PublicationDate}.\n")
                                .Append($"Precio: {item.TotalPrice}.\n")
                                .Append($"\n-----------------------------------------------\n\n");
                    }
                    response = offers.Append($"Si desea comprar alguna de las ofertas disponibles, por favor escriba su Id.\n")
                                    .Append($"De lo contrario escriba /menu para volver al menú principal.").ToString();
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
                    this.State = SearchOfferState.BuyOfferState;
                    int Id = Convert.ToInt32(input.Text);
                    this.Data.OfferToBuy = Market.Instance.GetOfferById(Id);
                    this.Data.Seller = this.Data.OfferToBuy.Company;
                    if (this.Data.OfferToBuy != null)
                    {
                        StringBuilder searchResult = new StringBuilder("¿Es esta la oferta que quiere comprar?\n");
                            searchResult.Append($"Id de la oferta: {this.Data.OfferToBuy.Id}.\n")
                                        .Append($"Material de la oferta: {this.Data.OfferToBuy.Material.Name}.\n")
                                        .Append($"Cantidad: {this.Data.OfferToBuy.QuantityMaterial}.\n")
                                        .Append($"Fecha de publicación: {this.Data.OfferToBuy.PublicationDate}.\n")
                                        .Append($"\n-----------------------------------------------\n\n")
                                        .Append($"Ingrese \"si\" si es la correcta, o \"no\" en caso contrario.");
                        response = searchResult.ToString();
                    }
                    else
                    {
                        response = $"No se encontró ninguna oferta con el Id \"{this.Data.Offers}\".";
                    }
                    return true;
                }
                else if (this.State == SearchOfferState.BuyOfferState)
                {
                    if(input.Text.ToLower().Trim() == "no")
                    {
                        this.State = SearchOfferState.AskActiveOfferIdState;
                        response = "Compra cancelada, ingrese nuevamente el Id correcto o /menu si desea volver.";
                        return true;
                    }
                    if(input.Text.ToLower().Trim() == "si")
                    {
                        Market.Instance.BuyOffer(this.Data.OfferToBuy.Id, UserRegister.Instance.GetUserById(input.Id));
                        this.State = SearchOfferState.Start;
                        StringBuilder sb = new StringBuilder("Datos de la empresa:\n");
                        sb.Append($"Nombre: {this.Data.Seller.Name}.\n")
                            .Append($"Email: {this.Data.Seller.Email}.\n")
                            .Append($"Número de teléfono: {this.Data.Seller.PhoneNumber}.");
                        response = sb.ToString();
                        return true;
                    }
                }
                response = string.Empty;
                return false;
            }
            catch(Exception e)
            {
                InternalCancel();
                response = e.Message;
                return true;
            }
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
            /// Luego de pedir el Id de la oferta a comprar. En este estado el comando devuelve la oferta seleccionada y pregunta si es esa la que el usuario quiere comprar.
            /// </summary>
            AskActiveOfferIdState,

            /// <summary>
            /// Luego de pedir la confirmacion de la oferta a comprar. En este estado se realiza la compra de la oferta seleccionada y se le pasa 
            /// a el usuario por pantalla la informacion de contacto de la empresa.
            /// </summary>
            BuyOfferState
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
            public List<Offer> Offers { get; set; }

            /// <summary>
            /// La oferta que se mostrará al emprendedor una vez haya enviado el Id de la oferta a comprar.
            /// </summary>
            /// <value></value>
            public Offer OfferToBuy { get; set; }

            /// <summary>
            /// La empresa la cual le pasaremos la informacion de contacto al usuario.
            /// </summary>
            /// <value></value>
            public Company Seller { get; set; }
        }
    }
}