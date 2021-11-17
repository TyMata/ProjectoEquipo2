using System;
using System.Collections.Generic;
using System.Text;
namespace ClassLibrary
{
    /// <summary>
    /// Handler encargado de crear un usuario emprendedor
    /// </summary>
    public class UnregisteredEntrepeneurUserHandler : AbstractHandler
    {
        
        /// <summary>
        /// Constructor de objetos UnregistredEntrepreneurUserHandler
        /// </summary>
        public UnregisteredEntrepeneurUserHandler(IMessageChannel channel)
        {
            this.Command = "emprendedor";
            this.messageChannel = channel;
        }
        /// <summary>
        /// Pregunta por los datos del emprendedor y delega la tarea de crear un usuario emprendedor
        /// </summary>
        /// <param name="input"></param>
        public override bool InternalHandle(IMessage input)
        {
            if (CanHandle(input))
            {
                StringBuilder datos = new StringBuilder("Asi que eres un Emprendedor!\n")
                                                .Append("Para poder registrarte vamos a necesitar algunos datos personales\n")
                                                .Append("Ingrese su nombre y apellido\n");
                this.messageChannel.SendMessage(datos.ToString());
                string nombre = this.messageChannel.ReceiveMessage().Text;
                this.messageChannel.SendMessage("Ingrese su ubicacion\n");
                string ubi =  this.messageChannel.ReceiveMessage().Text;
                this.messageChannel.SendMessage("Ingrese sus habilitaciones\n");
                string habilitaciones =  this.messageChannel.ReceiveMessage().Text;
                this.messageChannel.SendMessage("Ingrese su rubro\n");
                string rubro = this.messageChannel.ReceiveMessage().Text;
                /*CreateEntrepeneurUser(input, nombre, ubi, habilitaciones,rubro); FALTA CREAR */
                return true;
               
            }
            else
            {
                return false;
            } 
        } 
    }
}