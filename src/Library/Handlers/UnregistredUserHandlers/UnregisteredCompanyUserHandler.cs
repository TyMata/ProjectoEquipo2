using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary
{
    /// <summary>
    /// Handler encargado de delegar la accion de registrar un usuario empresa.
    /// </summary>
    public class UnregisteredCompanyUserHandler : AbstractHandler
    {
        /// <summary>
        /// Estado para el handler de UnregisteredCompanyUserHandler.
        /// </summary>
        /// <value></value>
        private UnregisteredCompanyUserState state { get; set; }

        /// <summary>
        /// Guarda la información que pasa el usuario por el chat cuando se utiliza el comando UnregisteredCompanyUserHandler.
        /// </summary>
        /// <value></value>
        private UnregisteredCompanyUserData data{ get; set; }
        /// <summary>
        /// Constructor de objetos UnregistredCompanyUserHandler.
        /// </summary>
        public UnregisteredCompanyUserHandler()
        {
            this.Command = "/empresanoregistrada";
            this.state = UnregisteredCompanyUserState.Start;
            this.data = new UnregisteredCompanyUserData();
        }
        /// <summary>
        /// Pregunta por el codigo de invitacion y delega la tarea de verificar si el token es valido 
        /// y la creacion de el usuario empresa.
        /// De no ser asi se le avisa al usuario.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="response"></param>
        public override bool InternalHandle(IMessage input, out string response)
        {
            if (this.state == UnregisteredCompanyUserState.Start && CanHandle(input))
            {
                StringBuilder datos = new StringBuilder("Asi que eres usuario de una empresa!\n")
                                                .Append("Para poder registrarte vamos a necesitar el codigo de invitacion\n")
                                                .Append("Ingrese el codigo de invitacion\n");
                this.state = UnregisteredCompanyUserState.Token;
                response = datos.ToString();
                return true;
            }
            else if(this.state == UnregisteredCompanyUserState.Token)
            {
                this.data.Token = input.Text.Trim();
                if (TokenRegister.Instance.IsValid(this.data.Token))
                {
                    Company temp = TokenRegister.Instance.GetCompany(this.data.Token);
                    temp.AddUser(input.Id);
                    this.state = UnregisteredCompanyUserState.Start;
                    response = $"Se verifico el Token ingresado y se esta creando su usario perteneciente a la empresa {temp.Name}";
                    return true;
                }
                else
                {
                    this.state = UnregisteredCompanyUserState.NotFirstTime;
                    response = "No se pudo verificar el Token ingresado, ingrese nuevamente el token de invitacion";
                    return true;
                }
            }
            else if(this.state == UnregisteredCompanyUserState.NotFirstTime)
            {
                this.data.Token = input.Text;
                this.state = UnregisteredCompanyUserState.Token;
                response = "";
                return true;
            }
            else
            {
                response = "";
                return false;
            }
        }
        
        /// <summary>
        /// Indica los diferentes estados que tiene UnregisteredCompanyUserHandler.
        /// </summary>
        public enum UnregisteredCompanyUserState
        {
            /// <summary>
            /// El estado inicial del comando. Aquí pregunta por el token de invitacion necesario para 
            /// registrar a un usuario empresa.
            /// </summary>
            Start,
            /// <summary>
            /// Estado que vuelve a recibir un token en caso de que el ingresado anteriormente tenga
            /// errores o no se encuentre en el registro.
            /// </summary>
            NotFirstTime,
            /// <summary>
            /// Estado en el cual se verifica si el token ingresado es valido o no y se informa al usuario.
            /// </summary>
            Token
        }

        /// <summary>
        /// Representa los datos que va obteniendo el comando UnregistredCompanyUserHandler en los diferentes estados.
        /// </summary>
        public class UnregisteredCompanyUserData
        {
            /// <summary>
            /// El Token que se ingresó en el estado UnregisteredCompanyUserState.Token.
            /// </summary>
            public string Token { get; set; }
        }
    }
}