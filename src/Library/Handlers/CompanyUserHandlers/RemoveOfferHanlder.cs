using System;

namespace ClassLibrary
{
    public class RemoveOfferHandler:AbstractHandler, IHandler
    {
          protected IHandler NextHandler;
        public override object Handle(object request)
        {
            if(request == "/Remover Oferta")
            {
                if(Company.ActualOffers != null)
                {
                    Console.WriteLine($"Estas son tus ofertas actuales: {Company.ActualOffers}./n ¿Cual es el Id de la que quiere retirar?");
                    Console.WriteLine($"La oferta {Oferta} se retiro del mercado");
                }
                else 
                {
                    Console.WriteLine("No hay ninguna oferta publicada bajo el nombre de esta empresa");
                }

            }
             else if (NextHandler != null)
            {
                NextHandler.Handle(request);
            }
        }
        
        public override  void SetNextHandler(IHandler nextHandler)
        {
            NextHandler = nextHandler;
        }
    }
}