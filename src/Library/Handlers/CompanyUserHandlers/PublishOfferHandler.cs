using System;

namespace ClassLibrary
{
    public class PublishOfferHandler : AbstractHandler , IHandler
    {
        protected IHandler NextHandler;
        public override object Handle(object request)
        {
            if (request == "/Publicar Oferta")
            {
                Console.WriteLine("¿Qué material desea vender?");

                Console.WriteLine("Cantidad de material:");

                Console.WriteLine("En que ubicación se encuentran los materiales?");

                Console.WriteLine("¿Que habilitaciones son necesarias para poder manipular este material?");

                Console.WriteLine("Insertar palabras claves para facilitar la  búsqueda:");
            }
            else if (NextHandler != null)
            {
                NextHandler.Handle(request);
            }

        }
        
        public void SetNextHandler(IHandler nextHandler)
        {
            NextHandler = nextHandler;
        }
    }
}