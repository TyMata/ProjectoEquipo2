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
        

        // public   IHandler Start(IMessageChannel channel, IMessage input)
        // {
        //     if(!this.Registro.IsRegistered(input.Id) )
        //     {
        //         //Chain of Responsability de unregistered


        //     }
        //     else
        //     {
        //         //Chain de registered
        //     }

        // }



    }
}