using System;
using System.Collections.Generic;
using System.Text;
namespace ClassLibrary
{
    /// <summary>
    /// Handler encargado de delegar la accion de eliminar un usario del registro
    /// </summary>
    public class RemoveUserHandler : AbstractHandler
    {
        
        /// <summary>
        /// Constructor de objetos RemoveUserHandler
        /// </summary>
        public RemoveUserHandler(IMessageChannel channel)
        {
            this.Command = "/eliminarusuario";
            this.messageChannel = channel;
        }
        /// <summary>
        /// Pregunta por el id del usuario que se quiere eliminar y si el usuario que se quiere eliminar esta registrado
        /// delega la accion de eliminarlo y lo informa por pantalla.
        /// De no ser asi lo informa por pantalla al usuario.
        /// </summary>
        /// <param name="input"></param>
        public override void Handle(IMessage input)
        {
            if (this.nextHandler != null && (CanHandle(input)) )
            {
                this.messageChannel.SendMessage("¿Cual es el Id del usuario que quieres eliminar?");
                int id = Convert.ToInt32(this.messageChannel.ReceiveMessage().Text);
                User user = Singleton<UserRegister>.Instance.GetUserById(id);
                if (user != null)
                {
                    Singleton<UserRegister>.Instance.Remove(user);
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