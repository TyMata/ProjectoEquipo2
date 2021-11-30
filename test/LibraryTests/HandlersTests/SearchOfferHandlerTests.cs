using System;
using System.Text;
using ClassLibrary;
using NUnit.Framework;

namespace Tests
{
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
            message = new TelegramBotMessage(1234, "/buscaroferta");
            location = new LocationAdapter("Comandante Braga 2715", "Montevideo", "Montevideo");
            material = new Material("Pallet", "Plastico", "Residuo");
            company =  CompanyRegister.Instance.CreateCompany("Nombre de la empresa", location, "headings", "company@gmail.com", "091919191");
            company.AddUser(1234);
            oferta = Market.Instance.CreateOffer(material, "link", location, 3, 3000, company, true,"continua");
            handler = new SearchOfferHandler();
        }

        [Test]
        public void HandleStartTest()
        {
            string response;
            bool result = handler.InternalHandle(message, out response);
            Assert.IsTrue(result);
            Assert.That(response, Is.EqualTo("Escriba la palabra clave de las ofertas a buscar")); 
            Assert.That(handler.State, Is.EqualTo(SearchOfferHandler.SearchOfferState.ShowActiveState));
        }

        [Test]
        public void HandleShowActiveTest()
        {
            string response;
            bool result = handler.InternalHandle(message, out response);
            message.Text = "Pallet";
            result = handler.InternalHandle(message, out response);
            StringBuilder offers = new StringBuilder("Estas son las ofertas encontradas con esa palabra clave:\n");
            foreach (Offer item in handler.Data.Offers)
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
            Assert.That(handler.State, Is.EqualTo(SearchOfferHandler.SearchOfferState.AskActiveOfferIdState));
        }

        [Test]
        public void HandleAskActiveOfferIdTest()
        {
            string response;
            bool result = handler.InternalHandle(message, out response);
            message.Text = "Pallet";
            result = handler.InternalHandle(message, out response);
            message.Text = oferta.Id.ToString();
            result = handler.InternalHandle(message, out response);
            StringBuilder searchResult = new StringBuilder("¿Es esta la oferta que quiere comprar?\n");
                        searchResult.Append($"Id de la oferta: {handler.Data.OfferToBuy.Id}.\n")
                                    .Append($"Material de la oferta: {handler.Data.OfferToBuy.Material.Name}.\n")
                                    .Append($"Cantidad: {handler.Data.OfferToBuy.QuantityMaterial}.\n")
                                    .Append($"Fecha de publicación: {handler.Data.OfferToBuy.PublicationDate}.\n")
                                    .Append($"\n-----------------------------------------------\n\n")
                                    .Append($"Ingrese \"si\" si es la correcta, o \"no\" en caso contrario.");
            response = searchResult.ToString();
            Assert.IsTrue(result);
            Assert.That(response, Is.EqualTo(searchResult.ToString())); 
            Assert.That(handler.State, Is.EqualTo(SearchOfferHandler.SearchOfferState.BuyOfferState));
        }

        [Test]
        public void HandleBuyOfferTest()
        {
            string response;
            bool result = handler.InternalHandle(message, out response);
            message.Text = "Pallet";
            result = handler.InternalHandle(message, out response);
            message.Text = oferta.Id.ToString();
            result = handler.InternalHandle(message, out response);
            message.Text = "si";
            result = handler.InternalHandle(message, out response);
            StringBuilder sb = new StringBuilder("Datos de la empresa:\n");
                    sb.Append($"Nombre: {handler.Data.Seller.Name}.\n")
                      .Append($"Email: {handler.Data.Seller.Email}.\n")
                      .Append($"Número de teléfono: {handler.Data.Seller.PhoneNumber}.");
            Assert.IsTrue(result);
            Assert.That(response, Is.EqualTo(sb.ToString())); 
            Assert.That(handler.State, Is.EqualTo(SearchOfferHandler.SearchOfferState.Start));
        }

        [Test]
        public void InternalNotHandleTest()
        {
            string response;
            IHandler result = handler.Handle(new ConsoleMessage("/menus"), out response);
            Assert.IsNull(result);
            Assert.IsEmpty(response);
        }
    }
}