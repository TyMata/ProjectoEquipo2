using System;
using System.Collections.Generic;
using System.Text;
namespace ClassLibrary
{
    /// <summary>
    /// Marcamos el formato del resto de handlers
    /// </summary>
    public class InviteTokenGeneratorHandler : AbstractHandler
    {
        
        /// <summary>
        /// Handler para los usuarios no registrados.
        /// </summary>
        public InviteTokenGeneratorHandler(IMessageChannel channel)
        {
            this.Command = "/generartokendeinvitacion";
            this.messageChannel = channel;
        }
        /// <summary>
        /// Verifica si el usuario que emite el mensaje esta registrado
        /// y de no ser asi lo ayuda a registrarse
        /// </summary>
        /// <param name="input"></param>
        public override void Handle(IMessage input)
        {
            if (this.nextHandler != null && (CanHandle(input)) )
            {
                this.messageChannel.SendMessage("Ingrese el nombre de la empresa a la que pertenece el token");
                bool response;
                string companyName = this.messageChannel.ReceiveMessage().Text;
                CompanyRegisterServiceProvider.SearchCompany(companyName,out response);
                if (response)
                {
                    this.messageChannel.SendMessage("Se esta generando un nuevo Token");
                    string token = TokenRegisterServiceProvider.GenerateToken(companyName);
                    this.messageChannel.SendMessage("El nuevo Token es este:");
                    this.messageChannel.SendMessage(token); // creado en TokenRegister??????)
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