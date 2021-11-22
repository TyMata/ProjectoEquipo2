using System;
using System.Collections.Generic;
using System.Text;
namespace ClassLibrary
{
    /// <summary>
    /// HAndler encargado de darle la bienvenida a un usuario no registrado.
    /// </summary>
    public class UnregisteredUserHandler : AbstractHandler
    {
        private UnregisteredUserData Data {get;set;}
        private UnregisteredUserState State {get;set;}
        /// <summary>
        /// Constructor de objetos UnregistredUserHandler.
        /// </summary>
        public UnregisteredUserHandler()
        {
            this.Data =new UnregisteredUserData();
            this.State = UnregisteredUserState.Start;
            this.Command = "/menu";
        }
        /// <summary>
        /// Se encarga de darle la bienvenida al usuario no registrado y preguntarle
        /// si es un emprendedor o ubn usuario empresa
        /// </summary>
        /// <param name="input"></param>
        /// <param name="response"></param>
        public override bool InternalHandle(IMessage input, out string response)
        {
            if (this.State == UnregisteredUserState.Start && this.nextHandler != null)
            {
                this.State = UnregisteredUserState.NotFirstTime;
                StringBuilder bienvenida = new StringBuilder("Bienvenido a el bot del Equipo 2\n")
                                                    .Append("Por lo que veo no estas registrado\n")
                                                    .Append("¿Eres usuario de una empresa o eres emprendedor?\n");
                response = bienvenida.ToString();
               // this.nextHandler.Handle(input, out response );
                return true;
            }
            // else if(this.State == UnregisteredUserState.Command && CanHandle(input))
            // {
            //     this.Data.Command = input.Text;
            //     this.State = UnregisteredUserState.NotFirstTime;
            //     response = "";
            //     return true;
            // }
            else if (this.State == UnregisteredUserState.NotFirstTime && CanHandle(input) )
            {
                this.State = UnregisteredUserState.NotFirstTime;
                response = "¿Eres usuario de una empresa o eres emprendedor?\n";
                return true;
            }
            response = string.Empty;
            return false;

        }

        public enum UnregisteredUserState
        {
            
            Start,
            NotFirstTime,
            Command // TODO ver si esta mal o no
        }

        public class UnregisteredUserData
        {
            public string Command {get;set;}
        }
    }
}