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
        /// Estado para el handler de RemoveUser.
        /// </summary>
        /// <value></value>
        public RemoveUserState State {get; set;}
        /// <summary>
        /// Guarda la información que pasa el usuario por el chat cuando se utiliza el comando RemoveUserHandler.
        /// </summary>
        /// <value></value>
        public RemoveUserData Data {get; set;}
        
        /// <summary>
        /// Constructor de objetos RemoveUserHandler.
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
                this.State = RemoveUserState.Start;
                this.Data.UserId = Convert.ToInt32(this.messageChannel.ReceiveMessage().Text);
                this.Data.Result = UserRegister.Instance.GetUserById(this.Data.UserId);
                if (this.Data.Result != null)
                {
                    UserRegister.Instance.Remove(this.Data.Result);
                    this.State = RemoveUserState.Start;
                    response = $"El usuario de Id: {this.Data.UserId} ha sido eliminado";                    
                }
                else
                {
                    response = $"No se encontro ningun usuario con el {this.Data.UserId}.";
                }
                return true;
            }
            response = "";
            return false;   
        }

        /// <summary>
        /// Indica los diferentes estados que tiene RemoveUserHandler.
        /// </summary>
        public enum RemoveUserState
        {
            /// <summary>
            /// El estado inicial del comando. Aquí pide el Id del usuario a eliminar y pasa al siguiente estado.
            /// </summary>
            Start,
            /// <summary>
            /// Luego de pedir el Id del usuario. En este estado el comando elimina el usuario si existe y vuelve al estado Start.
            /// </summary>
            User,
        }

        /// <summary>
        /// Representa los datos que va obteniendo el comando RemoveUserHandler en los diferentes estados.
        /// </summary>
        public class RemoveUserData
        {
            /// <summary>
            /// El Id del usuario que se ingresó en el estado RemoveUserState.User.
            /// </summary>
            /// <value></value>
            public int UserId {get;set;}
            /// <summary>
            /// El resultado de la búsqueda del usuario por medio del Id.
            /// </summary>
            /// <value></value>
            public Users Result {get; set;}
        }
    }
}