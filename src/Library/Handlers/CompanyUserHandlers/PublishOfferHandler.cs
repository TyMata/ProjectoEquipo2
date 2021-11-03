using System;

namespace ClassLibrary
{
    /// <summary>
    /// Handler para publicar una nueva oferta
    /// </summary>
    public class PublishOfferHandler : AbstractHandler
    {
        public PublishOfferHandler(IMessageChannel channel)
        {
            this.Command = "/PublicarOferta";
            this.messageChannel = channel;
        }
        public override void Handle(IMessage input)
        {
            if (this.nextHandler != null && (CanHandle(input)))
            {
                this.messageChannel.SendMessage("¿Qué material desea vender?");
                string material = this.messageChannel.ReceiveMessage().Text;
                this.messageChannel.SendMessage("Cantidad de material:");
                string cantidad= this.messageChannel.ReceiveMessage().Text;
                this.messageChannel.SendMessage("¿Cuál va a ser el precio total?");
                string precioTotal = this.messageChannel.ReceiveMessage().Text;
                this.messageChannel.SendMessage("En que ubicación se encuentran los materiales?");
                string ubicacion = this.messageChannel.ReceiveMessage().Text;
                this.messageChannel.SendMessage("¿Que habilitaciones son necesarias para poder manipular este material?");
                string habilitaciones = this.messageChannel.ReceiveMessage().Text;
                this.messageChannel.SendMessage("Insertar palabras claves para facilitar la  búsqueda:");
            }
            else
            {
                nextHandler.Handle(input);
            }
        }
    }
}