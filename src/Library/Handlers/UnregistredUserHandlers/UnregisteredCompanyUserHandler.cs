using System;
using System.Collections.Generic;
using System.Text;
namespace ClassLibrary
{
    /// <summary>
    /// Handler encargado de delegar la accion de registrar un usuario empresa
    /// </summary>
    public class UnregisteredCompanyUserHandler : AbstractHandler
    {
        
        /// <summary>
        /// Constructor de objetos UnregistredCompanyUserHandler
        /// </summary>
        public UnregisteredCompanyUserHandler(IMessageChannel channel)
        {
            this.Command = "empresa";
            this.messageChannel = channel;
        }
        /// <summary>
        /// Pregunta por el codigo de invitacion y delega la tarea de verificar si el token es valido 
        /// y la creacion de el usuario empresa.
        /// De no ser asi se le avisa al usuario.
        /// </summary>
        /// <param name="input"></param>
        public override void Handle(IMessage input)
        {
            if (this.nextHandler != null && (CanHandle(input)) )
            {
                StringBuilder datos = new StringBuilder("Asi que eres usuario de una empresa!\n")
                                                .Append("Para poder registrarte vamos a necesitar el codigo de invitacion\n")
                                                .Append("Ingrese el codigo de invitacion\n");
                this.messageChannel.SendMessage(datos.ToString());
                string codigo = this.messageChannel.ReceiveMessage().Text;
                Company response;

                if (TokenRegisterServiceProvider.IsValidToken(codigo, out response))
                {
                    CreateUserServiceProvider.CreateCompanyUser(input, response );
                }
                // else
                // {
                //    Excepcion?????? 
                // }
                else
                {
                    this.messageChannel.SendMessage("No se pudo verificar el Token ingresado, intente nuevamente");
                }
               
            }
            else
            {
                this.nextHandler.Handle(input);
            }
        }
    }
}