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
        /// Estado para el handler de UnregisteredCompanyUserState .
        /// </summary>
        /// <value></value>
        public UnregisteredCompanyUserState State { get; set; }
        /// <summary>
        /// Guarda la información que pasa el usuario por el chat cuando se utiliza el comando UnregisteredCompanyUserHandler.
        /// </summary>
        /// <value></value>
        public UnregisteredCompanyUserData Data{ get; set; }
        /// <summary>
        /// Constructor de objetos UnregistredCompanyUserHandler.
        /// </summary>
        public UnregisteredCompanyUserHandler()
        {
            this.Command = "/usuarioempresanoregistrado";
            this.State = UnregisteredCompanyUserState.Start;
            this.Data = new UnregisteredCompanyUserData();
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
            if (this.State == UnregisteredCompanyUserState.Start && CanHandle(input))
            {
                StringBuilder datos = new StringBuilder("¡Así que eres usuario de una empresa!\n")
                                                .Append("Para poder registrarte vamos a necesitar el código de invitación.\n")
                                                .Append("Ingrese el código de invitación.");
                this.State = UnregisteredCompanyUserState.Token;
                response = datos.ToString();
                return true;
            }
            else if(this.State == UnregisteredCompanyUserState.Token)
            {
                this.Data.Token = input.Text.Trim();
                if (TokenRegister.Instance.IsValid(this.Data.Token))
                {
                    this.Data.unregistered = TokenRegister.Instance.GetCompany(this.Data.Token);
                    this.Data.unregistered.AddUser(input.Id);
                    this.State = UnregisteredCompanyUserState.Start;
                    response = $"Se verificó el Token ingresado y se ha creado su usuario perteneciente a la empresa {this.Data.unregistered.Name}.\nIngrese /menu para ver los comandos nuevamente.";
                    return true;
                }
                else
                {
                    this.State = UnregisteredCompanyUserState.NotFirstTime;
                    response = "No se pudo verificar el Token ingresado, ingrese nuevamente el token de invitación.";
                    return true;
                }
            }
            else if(this.State == UnregisteredCompanyUserState.NotFirstTime)
            {
                this.Data.Token = input.Text;
                this.State = UnregisteredCompanyUserState.Token;
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

            public Company unregistered { get; set; }
        }
    }
}