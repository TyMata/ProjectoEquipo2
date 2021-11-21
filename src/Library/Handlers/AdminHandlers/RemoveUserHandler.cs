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
        public RemoveUserState State {get; set;}

        public RemoveUserData Data {get; set;}
        
        /// <summary>
        /// Constructor de objetos RemoveUserHandler
        /// </summary>
        public RemoveUserHandler(IMessageChannel channel)
        {
            this.Command = "eliminarusuario";
            this.messageChannel = channel;
            this.State = RemoveUserState.Start;
            this.Data = new RemoveUserData();
        }
        /// <summary>
        /// Pregunta por el id del usuario que se quiere eliminar y si el usuario que se quiere eliminar esta registrado
        /// delega la accion de eliminarlo y lo informa por pantalla.
        /// De no ser asi lo informa por pantalla al usuario.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="response"></param>
        public override bool InternalHandle(IMessage input, out string response)
        {
            if(this.State == RemoveUserState.Start && this.CanHandle(input))
            {
                this.State = RemoveUserState.User;
                response = "¿Cual es el Id del usuario que quieres eliminar?";
                return true;
            }
            else if(this.State == RemoveUserState.User)
            {
                this.Data.User = Convert.ToInt32(this.messageChannel.ReceiveMessage().Text);
                this.Data.Result = UserRegister.Instance.GetUserById(this.Data.User);
                if (this.Data.Result != null)
                {
                    UserRegister.Instance.Remove(this.Data.Result);
                    this.State = RemoveUserState.Start;
                    response = $"El usuario de Id: {this.Data.User} ha sido eliminado";                    
                }
                else
                {
                    response = $"No se encontro ningun usuario con el {this.Data.User}.";
                }
                return true;
            }
            response = "";
            return false;
            
        }
        public enum RemoveUserState
        {
            Start,
            User,
        }

        public class RemoveUserData
        {
            public int User {get;set;}

            public Users Result {get; set;}
        }
    }
}