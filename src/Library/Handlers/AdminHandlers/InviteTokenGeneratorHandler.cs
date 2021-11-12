using System;
using System.Collections.Generic;
using System.Text;
namespace ClassLibrary
{
    /// <summary>
    /// Handler encargado de delegar la creacion de un nuevo token de invitacion para un usuario empresa.
    /// </summary>
    public class InviteTokenGeneratorHandler : AbstractHandler
    {
        
        /// <summary>
        /// Constructor de los objetos InviteTokenGeneratorHandler.
        /// </summary>
        public InviteTokenGeneratorHandler(IMessageChannel channel)
        {
            this.Command = "/generartokendeinvitacion";
            this.messageChannel = channel;
        }
        /// <summary>
        /// Pregunta por el nombre de la empresa a la que se le quiere añadir un usuario, delega la creacion de un token de invitacion
        /// y lo muestra por pantalla para poder pasarselo al usuario por otro medio por fuera del bot.
        /// De no haber una empresa registrada con ese nombre le avisa al usuario de esto.
        /// </summary>
        /// <param name="input"></param>
        public override void Handle(IMessage input)
        {
            if (this.nextHandler != null && (CanHandle(input)) )
            {
                this.messageChannel.SendMessage("Ingrese el nombre de la empresa a la que pertenece el token");
                bool response;
                string companyName = this.messageChannel.ReceiveMessage().Text;
                Singleton<CompanyRegister>.Instance.GetCompanyByUserId(input.Id);
                if (response)
                {
                    this.messageChannel.SendMessage("Se esta generando un nuevo Token");
                    string token = TokenRegisterServiceProvider.GenerateToken(companyName);
                    this.messageChannel.SendMessage("El nuevo Token es este:");
                    this.messageChannel.SendMessage(token);
                }
                else
                    this.messageChannel.SendMessage("No hay ninguna empresa registrada con ese nombre");
            }
            else
            {
                this.nextHandler.Handle(input);
            }
            
        }
        
    }
}