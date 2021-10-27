using System;

namespace ClassLibrary
{
    public class UnsuspendOfferHanlder : AbstractHandler, IHandler
    {
        protected IHandler NextHandler;
        public override object Handle(object request)
        {
             if(request == "/Anular Suspencion Oferta")
            {
                 if(Company.ActualOffers != null)
                {
                    Console.WriteLine($"Estas son tus ofertas pausadas: {Company.PausedOffers}./n ¿Cual es el Id de la que quiere despausar?");
                    Console.ReadLine();
                    Console.WriteLine($"La oferta {Oferta} ha sido despausada.");
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