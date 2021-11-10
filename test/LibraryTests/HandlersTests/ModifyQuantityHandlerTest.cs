using System;
using NUnit.Framework;
using ClassLibrary;


namespace Tests
{
    [TestFixture]
    public class ModifyQuantityHandlerTest
    {
        private Offer oferta;
        private Material material;
        private Company company;
        private DateTime dateTime;
        private IHandler handler;

        [SetUp]
        public void Setup()
        {
        //this.client = new LocationApiClient();
            //Company company = new Company("compania",location, "rubro", "materiales");
            DateTime dateTime = new DateTime();
            //this.oferta = new Offer(1234567,"material", "habilitation", location,3,300.0,company,"keywords",true,dateTime);
            this.material = new Material("material","type","clasificacion");
            IMessageChannel messageChannel = new ConsoleMessageChannel();
            this.handler = new ModifyQuantityHandler(messageChannel);
        }
        [Test]

        public void HandleTest()
        {
            handler.Handle(new ConsoleMessage("/modificarcantidad"));
            
        }

    }
}