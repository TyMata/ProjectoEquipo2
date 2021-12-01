using System;
using System.Text;
using ClassLibrary;
using NUnit.Framework;

namespace Tests
{
    /// <summary>
    /// Prueba la clase <see cref="SearchOfferHandler"/>
    /// </summary>
    public class SearchOfferHandlerTests
    {
        private IMessage message;
        private LocationAdapter location;
        private Offer oferta;
        private Material material;
        private Company company;
        private SearchOfferHandler handler;
        private DateTime dateTime;

        [SetUp]
        public void SetUp()
        {
            this.message = new TelegramBotMessage(1234, "/buscaroferta");
            location = new LocationAdapter("Comandante Braga 2715", "Montevideo", "Montevideo");
            material = new Material("Pallet", "Plastico", "Residuo");
            company =  CompanyRegister.Instance.CreateCompany("Nombre de la empresa", location, "headings", "company@gmail.com", "091919191");
            UserRegister.Instance.CreateEntrepreneurUser(1234,"098098098","Emprendedor",location,"rubro","link");
            this.oferta = new Offer(1234567, material, "link", location, "kg", 3, "pesos", 3000, company, true,new DateTime(),"continua");
            company.AddOffer(oferta);
            Market.Instance.PublishOffer(oferta);
            this.handler = new SearchOfferHandler();
        }

        /// <summary>
        /// Prueba que el mensaje sea procesado y que pida una palabra clave para buscar ofertas.
        /// </summary>
        [Test]
        public void HandleStartTest()
        {
            string response;
            bool result = this.handler.InternalHandle(this.message, out response);
            Assert.IsTrue(result);
            Assert.That(response, Is.EqualTo("Escriba la palabra clave de las ofertas a buscar")); 
            Assert.That(this.handler.State, Is.EqualTo(SearchOfferHandler.SearchOfferState.ShowActiveState));
        }

        /// <summary>
        /// Prueba que se procese el mensaje y que muestre las ofertas con la palabra clave que ingresó.
        /// </summary>
        [Test]
        public void HandleShowActiveTest()
        {
            string response;
            bool result = this.handler.InternalHandle(this.message, out response);
            this.message.Text = "Pallet";
            result = this.handler.InternalHandle(this.message, out response);
            StringBuilder offers = new StringBuilder("Estas son las ofertas encontradas con esa palabra clave:\n");
            foreach (Offer item in this.handler.Data.Offers)
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
            Assert.IsTrue(result);
            Assert.That(response, Is.EqualTo(offers.ToString())); 
            Assert.That(this.handler.State, Is.EqualTo(SearchOfferHandler.SearchOfferState.AskActiveOfferIdState));
        }

        /// <summary>
        /// Prueba que se procese el mensaje, le muestre la oferta a comprar  y que ingrese si quiere realizar la compra.
        /// </summary>
        [Test]
        public void HandleAskActiveOfferIdTest()
        {
            string response;
            bool result = this.handler.InternalHandle(this.message, out response);
            this.message.Text = "Pallet";
            result = this.handler.InternalHandle(this.message, out response);
            this.message.Text = oferta.Id.ToString();
            result = this.handler.InternalHandle(this.message, out response);
            StringBuilder searchResult = new StringBuilder("¿Es esta la oferta que quiere comprar?\n");
                        searchResult.Append($"Id de la oferta: {this.handler.Data.OfferToBuy.Id}.\n")
                                    .Append($"Material de la oferta: {this.handler.Data.OfferToBuy.Material.Name}.\n")
                                    .Append($"Cantidad: {this.handler.Data.OfferToBuy.QuantityMaterial}.\n")
                                    .Append($"Fecha de publicación: {this.handler.Data.OfferToBuy.PublicationDate}.\n")
                                    .Append($"\n-----------------------------------------------\n\n")
                                    .Append($"Ingrese \"si\" si es la correcta, o \"no\" en caso contrario.");
            response = searchResult.ToString();
            Assert.IsTrue(result);
            Assert.That(response, Is.EqualTo(searchResult.ToString())); 
            Assert.That(this.handler.State, Is.EqualTo(SearchOfferHandler.SearchOfferState.BuyOfferState));
        }

        /// <summary>
        /// Prueba que se procese el mensaje y que la compra fue relizada, además que se le envie al comprado los datos del vendedor.
        /// </summary>
        [Test]
        public void HandleBuyOfferTest()
        {
            string response;
            bool result = this.handler.InternalHandle(this.message, out response);
            this.message.Text = "Pallet";
            result = this.handler.InternalHandle(this.message, out response);
            this.message.Text = oferta.Id.ToString();
            result = this.handler.InternalHandle(this.message, out response);
            this.message.Text = "si";
            result = this.handler.InternalHandle(this.message, out response);
            StringBuilder sb = new StringBuilder("Datos de la empresa:\n");
                    sb.Append($"Nombre: {this.handler.Data.Seller.Name}.\n")
                      .Append($"Email: {this.handler.Data.Seller.Email}.\n")
                      .Append($"Número de teléfono: {this.handler.Data.Seller.PhoneNumber}.");
            Assert.IsTrue(result);
            Assert.That(response, Is.EqualTo(sb.ToString())); 
            Assert.That(this.handler.State, Is.EqualTo(SearchOfferHandler.SearchOfferState.Start));
        }

        /// <summary>
        /// Prueba que no se procese el mensaje ya que no es el correcto.
        /// </summary>
        [Test]
        public void InternalNotHandleTest()
        {
            string response;
            IHandler result = this.handler.Handle(new ConsoleMessage("/menus"), out response);
            Assert.IsNull(result);
            Assert.IsEmpty(response);
        }
    }
}