using System;

namespace ClassLibrary
{
    /// <summary>
    /// Handler para publicar una nueva oferta
    /// </summary>
    public class PublishOfferHandler : AbstractHandler , IHandler
    {
        private IHandler NextHandler;

        public PublishOfferHandler(IMessageChannel channel)
        {
            this.messageChannel = channel;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public override void Handle(IMessage input)
        {
            if (input.Text.ToLower().Trim() == "/Publicar Oferta")
            {
                

                this.messageChannel.SendMessage("¿Qué material desea vender?");
                string material = this.messageChannel.ReceiveMessage().Text;
                this.messageChannel.SendMessage("Cantidad de material:");
                string cantidad= this.messageChannel.ReceiveMessage().Text;
                this.messageChannel.SendMessage("En que ubicación se encuentran los materiales?");
                string ubicacion = this.messageChannel.ReceiveMessage().Text;
                this.messageChannel.SendMessage("¿Que habilitaciones son necesarias para poder manipular este material?");
                string habilitaciones = this.messageChannel.ReceiveMessage().Text;
                this.messageChannel.SendMessage("Insertar palabras claves para facilitar la  búsqueda:");

                
            }
            else if (NextHandler != null)
            {
                NextHandler.Handle(input);
            }

        }
        
        
    }
}