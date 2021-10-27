using System;
using System.Text;
namespace ClassLibrary
{
    public class CompanyUserHandler : AbstractHandler, IHandler
    {
        protected IHandler NextHandler1;
        protected IHandler NextHandler2;
        public override object Handle(object request)
        {
            if ( id in "/idempresa")
            {
                StringBuilder commandsStringBuilder = new StringBuilder($"Bienvendio{Company.Name}.\n Que desea hacer?:\n")
                                                                            .Append("/Publicar Oferta\n")
                                                                            .Append("/Retirar Oferta\n")
                                                                            .Append("/Suspender Oferta\n")
                                                                            .Append("/Anular Suspension Oferta\n")
                                                                            .Append("/Modificar Oferta\n")
                                                                            .Append("/Buscar Oferta\n");
                                                                            
                NextHandler2.Handle(request);
            }
            else if (NextHandler1 != null)
            {
                NextHandler1.Handle(request);
            }
        }
        
        public void SetNextHandler1(IHandler nextHandler)
        {
            NextHandler1 = nextHandler;
        }

         public void SetNextHandler2(IHandler nextHandler)
        {
            NextHandler2 = nextHandler;
        }


    }
}