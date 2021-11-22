using System;

namespace ClassLibrary
{   
    /// <summary>
    /// Esta clase solo se encarga de crear la chain of responsability correspondiente para cada usuario
    /// por lo tanto cumple con el patron SRP. Tambien comple con el patron Creator porque utiliza, necesariamente, ya que es la encargada
    /// de construir los Handler para crear la CoR.
    /// </summary>
    public class Setup
    {

        public IHandler Start(IMessageChannel channel, IMessage input)
        {
            if(UserRegister.Instance.GetUserById(input.Id) == null )
            {
                //Chain of Responsability de unregistered
                IHandler respuesta =  new UnregisteredUserHandler(channel);
                respuesta.SetNext(new UnregisteredCompanyUserHandler(channel)
                        .SetNext(new UnregisteredEntrepeneurUserHandler(channel)
                        .SetNext(new EndHandler(channel, respuesta))));
                return respuesta;
            }
            else if(UserRegister.Instance.GetUserById(input.Id).IsAdminUser())
            {
                IHandler respuesta =  new AdminStartHandler(channel);
                respuesta.SetNext(new AddCompanyHandler(channel)
                        .SetNext(new RemoveUserHandler(channel)
                        .SetNext(new RemoveCompanyHandler(channel)
                        .SetNext(new EndHandler(channel, respuesta)))));
                return respuesta;
            }
            else if (UserRegister.Instance.GetUserById(input.Id).IsCompanyUser())
            {
                IHandler respuesta = new CompanyUserHandler(channel);
                respuesta.SetNext(new PublishOfferHandler(channel)
                            .SetNext(new RemoveOfferHandler(channel)
                            .SetNext(new SuspendOfferHandler(channel)
                            .SetNext(new ResumeOfferHandler(channel)
                            .SetNext(new ModifyHabilitationsHandler(channel)
                            .SetNext(new ModifyPriceHandler(channel)
                            .SetNext(new ModifyQuantityHandler(channel)
                            .SetNext(new ShowCompanyOffersHandler(channel)
                            .SetNext(new EndHandler(channel, null))))))))));
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
