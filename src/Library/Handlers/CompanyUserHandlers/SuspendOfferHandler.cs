using System;

namespace ClassLibrary
{
    public class SuspendOfferHandler : AbstractHandler , IHandler
    {
        
        protected IHandler NextHandler;
        public override object Handle(object request)
        {
             if(request == "/Suspender Oferta")
            {
                 if(Company.ActualOffers != null)
                {
                    Console.WriteLine($"Estas son tus ofertas actuales: {Company.ActualOffers}./n ¿Cual es el Id de la que quiere pausar?");
                    Console.ReadLine();
                    Console.WriteLine($"La oferta {Oferta} ha sido pausada.");
                }
                else 
                {
                    Console.WriteLine("No hay ninguna oferta publicada bajo el nombre de esta empresa.");
                }

            }
             else if (NextHandler != null)
            {
                NextHandler.Handle(request);
            }

        }
        
        public override void SetNextHandler(IHandler nextHandler)
        {
            NextHandler = nextHandler;
        }
    }
}