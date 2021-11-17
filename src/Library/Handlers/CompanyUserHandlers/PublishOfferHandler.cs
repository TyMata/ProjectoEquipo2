using System;
using Ucu.Poo.Locations.Client;

namespace ClassLibrary
{
    /// <summary>
    /// Handler encargado de delegar la accion de crear y publicar una oferta en el registro
    /// </summary>
    public class PublishOfferHandler : AbstractHandler
    {
        /// <summary>
        /// Constructor de objetos PublishOfferHandler
        /// </summary>
        /// <param name="channel"></param>
        public PublishOfferHandler(IMessageChannel channel)
        {
            this.Command = "/publicaroferta";
            this.messageChannel = channel;
        }
        /// <summary>
        /// Pregunta por los datos de la oferta a crear y delega la accion de crearla y publicarla
        /// </summary>
        /// <param name="input"></param>
        public override bool InternalHandle(IMessage input)
        {
            if (CanHandle(input))
            {
                this.messageChannel.SendMessage("¿Qué material desea vender?");
                string material = this.messageChannel.ReceiveMessage().Text;
                this.messageChannel.SendMessage("¿De parte de que empresa esta publicando los materiales?");
                Company company = Singleton<CompanyRegister>.Instance.GetCompanyByUserId(input.Id);
                this.messageChannel.SendMessage("Cantidad de material:");
                int quantity= Convert.ToInt32(this.messageChannel.ReceiveMessage().Text);
                this.messageChannel.SendMessage("¿Cuál va a ser el precio total?");
                double totalPrice = Convert.ToDouble(this.messageChannel.ReceiveMessage().Text);
                this.messageChannel.SendMessage("¿Que habilitaciones son necesarias para poder manipular este material?");
                string habilitations = this.messageChannel.ReceiveMessage().Text;
                this.messageChannel.SendMessage("Insertar palabras claves para facilitar la  búsqueda, separadas por una coma ( , ):");
                string keywords = this.messageChannel.ReceiveMessage().Text;
                User usuario = Singleton<UserRegister>.Instance.GetUserById(input.Id);
                Singleton<Market>.Instance.CreateOffer(input.Id, material, habilitations, company.Locations, quantity, totalPrice,  company, keywords,true);
                return true;
            }
            return false;
            
        }
    }
}