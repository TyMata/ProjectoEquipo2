using System;

namespace ClassLibrary
{   
    /// <summary>
    /// Esta clase solo se encarga de crear la chain of responsability correspondiente para cada usuario
    /// por lo tanto cumple con el patron SRP. Tambien cumple con el patron Creator porque utiliza, necesariamente, ya que es la encargada
    /// de construir los Handler para crear la CoR.
    /// </summary>
    public class Setup
    {

        public IHandler Start( IMessage input)
        {
            if(UserRegister.Instance.GetUserById(input.Id) == null )
            {
                //Chain of Responsability de unregistered
                IHandler respuesta =  new UnregisteredUserHandler();
                respuesta.SetNext(new UnregisteredCompanyUserHandler()
                        .SetNext(new UnregisteredEntrepeneurUserHandler()
                        .SetNext(new EndHandler (respuesta))));
                return respuesta;
            }
            else if(UserRegister.Instance.GetUserById(input.Id).IsAdminUser())
            {
                IHandler respuesta =  new AdminStartHandler();
                respuesta.SetNext(new AddCompanyHandler()
                        .SetNext(new RemoveUserHandler()
                        .SetNext(new RemoveCompanyHandler()
                        .SetNext(new EndHandler(respuesta)))));
                return respuesta;
            }
            else if (UserRegister.Instance.GetUserById(input.Id).IsCompanyUser())
            {
                IHandler respuesta = new CompanyUserHandler();
                respuesta.SetNext(new PublishOfferHandler()
                            .SetNext(new RemoveOfferHandler()
                            .SetNext(new SuspendOfferHandler()
                            .SetNext(new ResumeOfferHandler()
                            .SetNext(new ModifyHabilitationsHandler()
                            .SetNext(new ModifyPriceHandler()
                            .SetNext(new ModifyQuantityHandler()
                            .SetNext(new ShowCompanyOffersHandler()
                            .SetNext(new EndHandler( null))))))))));
                return respuesta;
            }
            // else
            // {
            //     // IHandler respuesta = new EntrepreneurUserHandler(channel);
            //     // respuesta.SetNext(new ActiveOfferHandler(channel)
            //     //             .SetNext(new SearchOfferHandler(channel)));
            // }
            return null;
        }
    }
}