using System;
using System.Collections.Generic;
using System.Text;
namespace ClassLibrary
{
    /// <summary>
    /// Primer Handler de la CoR para los usuarios Admin.
    /// </summary>
    public class AdminStartHandler : AbstractHandler
    {
        public AdminStartState State { get; set; }

        /// <summary>
        /// Constructor de los objetos AdminStartHandler.
        /// </summary>
        public AdminStartHandler(IMessageChannel channel)
        {
            this.messageChannel = channel;
            this.State = AdminStartState.Start;
        }
        /// <summary>
        /// Le otorga por pantalla los comandos que puede utilizar el admin.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="response"></param>
        public override bool InternalHandle(IMessage input,out string response)
        {
            if (this.State == AdminStartState.Start && this.nextHandler != null) // TODO ver como hacer este handler o como mandlre el next handler
            {
                StringBuilder bienvenida = new StringBuilder("Bienvenido Admin!\n")
                                                .Append("Que quieres hacer?\n")
                                                .Append("/RegistrarEmpresa\n")
                                                .Append("/EliminarUsuario\n")
                                                .Append("/EliminarEmpresa\n");
                this.State = AdminStartState.NotFirstTime;
                response = bienvenida.ToString();
            }
            else if(this.State == AdminStartState.Command)
            {
                IMessage input2 = this.messageChannel.ReceiveMessage();
                response = "";
                return true;
            }
            response = "";
            return false;
        }

        public enum AdminStartState
        {
            Start,
            NotFirstTime,
            Command
        }

        public class RemoveUserData
        {
            public int User {get;set;}
        }
    }
}