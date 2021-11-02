using System;

namespace ClassLibrary
{
    public class SetUp
    {
        private Register Registro;
        public SetUp(Register registro)
        {
            this.Registro = registro;
        }
        

        /*public IHandler Start(IMessageChannel channel, IMessage input)
        {
            if(!this.Registro.IsRegistered(input.Id) )
            {
                //Chain of Responsability de unregistered
                IHandler respuesta =  new UnregisteredUserHandler(channel);
                respuesta.SetNext(new UnregisteredCompanyUserHandler(channel)
                        .SetNext(new UnregisteredEntrepeneurUserHandler(channel)
                        .SetNext(new EndHandler(channel, respuesta))));
                return respuesta;
            }
            else if(this.Registro.IsAdmin(input.Id))
            {
                
            }
            else if (this.Registro.IsCompany(input.Id))
            {

            }
            else
            {
                
            }
        }*/



    }
}
