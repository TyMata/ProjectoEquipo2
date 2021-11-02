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
                this.messageChannel.SendMessage("¿Para que empresa es el token?");
                
                //if IsRegistredCompany(this.messageChannel.ReceiveMessage());
                    this.messageChannel.SendMessage("Se esta generando un nuevo Token");
                    this.messageChannel.SendMessage("El nuevo Token es este:");
                    //this.messageChannel.SendMessage    GenerateToken()  FALTA CREAR???? (creado en TokenRegister??????)
                //else
                    //this.messageChannel.SendMessage("No hay ninguna empresa registrada con ese nombre")
            }
            else
            {
                this.nextHandler.Handle(input);
            }
            
        }
        
    }
}