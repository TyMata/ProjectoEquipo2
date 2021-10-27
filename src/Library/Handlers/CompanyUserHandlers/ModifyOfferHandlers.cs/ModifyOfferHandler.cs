using System;
using System.Text;

namespace ClassLibrary
{
    public class ModifyOfferHandler: IHandler
    {
        protected IHandler NextHandler1;
        protected IHandler NextHandler2;
        public override object Handle(object request)
        {
            
            if(request == "/Modificar Oferta")
            {
                 if(Company.ActualOffers != null)
                {
                   
                StringBuilder commandsStringBuilder = new StringBuilder($"Que desea modificar?\n")
                                                                            .Append("/Modificar cantidad\n")
                                                                            .Append("/Modificar Precio\n")
                                                                            .Append("/Modificar habilitaciones\n")
                                                                            .Append("/Modificar tiempo\n");
                                                                          
                                                                            
                NextHandler1.Handle(request);
                }
                else 
                {
                    Console.WriteLine("No hay ninguna oferta publicada bajo el nombre de esta empresa.");
                }

            }
             else if (NextHandler2 != null)
            {
                NextHandler2.Handle(request);
            }

           

        }
        
        public void SetNextHandler(IHandler nextHandler)
        {
             NextHandler1 = nextHandler;
        }
        public void SetNextHandler(IHandler nextHandler)
        {
             NextHandler2 = nextHandler;
        
    }
}