using System;

namespace ClassLibrary
{
    public class ModifyQuantityHandler : IHandler
    {
        protected IHandler NextHandler;
        public override object  Handle(object request)
        {
            if(request == "/Modificar cantidad")
            {
                Console.WriteLine("Escriba la nueva cantidad de material:");
                Offer.Cuantity = Console.ReadLine();
               
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