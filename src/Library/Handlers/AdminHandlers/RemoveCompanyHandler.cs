using System;
using System.Collections.Generic;
using System.Text;
namespace ClassLibrary
{
    /// <summary>
    /// Marcamos el formato del resto de handlers
    /// </summary>
    public class RemoveCompanyHandler : AbstractHandler
    {
        
        /// <summary>
        /// Handler para los usuarios no registrados.
        /// </summary>
        public RemoveCompanyHandler(IMessageChannel channel)
        {
            this.Command = "/eliminarusuario";
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
                this.messageChannel.SendMessage("¿Cual es el Id del usuario que quieres eliminar?");
                string id = this.messageChannel.ReceiveMessage().Text;
                //if IsRegistredCompany(id)
                // {
                //     register.RemoveCompany(id);
                //     this.messageChannel.SendMessage($"La empresa de Id: {id} ha sido eliminada");
                // }
                // else 
                // {
                //     this.messageChannel.SendMessage($"El usuario de Id: {id} no esta registrado");
                // }
            }
            else
            {
                this.nextHandler.Handle(input);
            }
            
        }
        
    }
}