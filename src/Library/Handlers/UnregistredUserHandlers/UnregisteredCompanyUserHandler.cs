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
        public UnregisteredCompanyUserState State { get; set; }

        public UnregisteredCompanyUserData Data{ get; set; }
        /// <summary>
        /// Constructor de objetos UnregistredCompanyUserHandler.
        /// </summary>
        public UnregisteredCompanyUserHandler()
        {
            this.Command = "empresa";
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
                StringBuilder datos = new StringBuilder("Asi que eres usuario de una empresa!\n")
                                                .Append("Para poder registrarte vamos a necesitar el codigo de invitacion\n")
                                                .Append("Ingrese el codigo de invitacion\n");
                this.State = UnregisteredCompanyUserState.Token;
                response = datos.ToString();
                return true;
            }
            else if(this.State == UnregisteredCompanyUserState.NotFirstTime)
            {
                this.Data.Token = input.Text;
                this.State = UnregisteredCompanyUserState.Token;
                response = "";
                return true;
            }
            else if(this.State == UnregisteredCompanyUserState.Token)
            {
                this.Data.Token = input.Text;
                if (TokenRegister.Instance.IsValid(this.Data.Token))
                {
                    Company temp = TokenRegister.Instance.GetCompany(this.Data.Token);
                    temp.AddUser(input.Id);
                    this.State = UnregisteredCompanyUserState.Start;
                    response = $"Se verifico el Token ingresado y se esta creando su usario perteneciente a la empresa {temp.Name}";
                    return true;
                }
                else
                {
                    this.State = UnregisteredCompanyUserState.NotFirstTime;
                    response = "No se pudo verificar el Token ingresado, ingrese nuevamente el token de invitacion";
                    return true;
                }
            }
            else
            {
                response = "";
                return false;
            }
        }

        public enum UnregisteredCompanyUserState
        {
            Start,
            NotFirstTime,
            Token
        }

        public class UnregisteredCompanyUserData
        {
            /// <summary>
            /// El Token que se ingresó en el estado UnregisteredCompanyUserState.Token.
            /// </summary>
            public string Token { get; set; }
        }
    }
}