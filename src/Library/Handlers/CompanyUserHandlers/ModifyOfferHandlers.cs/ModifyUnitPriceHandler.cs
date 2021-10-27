using System;

namespace ClassLibrary
{
    public class ModifyUnitPriceHandler : AbstractHandler ,IHandler
    {
        protected IHandler NextHandler;
        public override object Handle(object request)
        {
            if(request == "/Modificar precio")
            {
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