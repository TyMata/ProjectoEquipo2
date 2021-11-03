using System;
using System.Collections.Generic;
using System.Text;
namespace ClassLibrary
{
    /// <summary>
    /// Marcamos el formato del resto de handlers
    /// </summary>
    public class RemoveUserHandler : AbstractHandler
    {
        
        /// <summary>
        /// Handler para los usuarios no registrados.
        /// </summary>
        public RemoveUserHandler(IMessageChannel channel)
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
                int id = Convert.ToInt32(this.messageChannel.ReceiveMessage().Text);                               //COMO PASAMOS DE STRING A INT
                if (UserRegisterServiceProvider.IsRegistredUser(id))
                {
                    UserRegisterServiceProvider.RemoveUser(id);
                    this.messageChannel.SendMessage($"El usuario de Id: {id} ha sido eliminado");
                }
                else
                {
                    this.messageChannel.SendMessage($"El usuario de Id: {id} no esta registrado");
                }
            }
            else
            {
                this.nextHandler.Handle(input);
            }
            
        }
        
    }
}