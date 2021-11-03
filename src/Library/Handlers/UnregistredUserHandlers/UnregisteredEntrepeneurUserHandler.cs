using System;
using System.Collections.Generic;
using System.Text;
namespace ClassLibrary
{
    /// <summary>
    /// Marcamos el formato del resto de handlers
    /// </summary>
    public class UnregisteredEntrepeneurUserHandler : AbstractHandler
    {
        
        /// <summary>
        /// Handler para los usuarios no registrados.
        /// </summary>
        public UnregisteredEntrepeneurUserHandler(IMessageChannel channel)
        {
            this.Command = "emprendedor";
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
               
            }
            else
            {
                this.nextHandler.Handle(input);
            }
            
        }
        
    }
}