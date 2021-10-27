using System;

namespace ClassLibrary
{
    public class ModifyOfferHAndler : IHandler
    {
        protected IHandler NextHandler;
        public override object Handle(object request)
        {
            if(request == "/Modificar Habilitaciones")
            {
                

            }
             else if (NextHandler != null)
            {
                NextHandler.Handle(request);
            }

        }
        
        public IHandler SetNextHandler(IHandler nextHandler)
        {
            NextHandler = nextHandler;

        }
    }
}