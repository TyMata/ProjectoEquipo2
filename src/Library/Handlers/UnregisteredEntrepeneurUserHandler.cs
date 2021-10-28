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
            this.messageChannel = channel;
        }
        /*private IHandler nextHandler;*/
        /// <summary>
        /// Verifica si el usuario que emite el mensaje esta registrado
        /// y de no ser asi lo ayuda a registrarse
        /// </summary>
        /// <param name="input"></param>
        public override void Handle(IMessage input)
        {
            if (/*this.nextHandler != null &&*/ (input.Text.ToLower().Trim() == "emprendedor") )
            {
                StringBuilder datos = new StringBuilder("Asi que eres un Emprendedor!")
                                                .Append("Para poder registrarte vamos a necesitar algunos datos personales")
                                                .Append("Ingrese su nombre y apellido");
                this.messageChannel.SendMessage(datos.ToString());
                string nombre = this.messageChannel.ReceiveMessage().Text;
                this.messageChannel.SendMessage("Ingrese su ubicacion");
                string ubi =  this.messageChannel.ReceiveMessage().Text;
                this.messageChannel.SendMessage("Ingrese sus habilitaciones");
                string habilitaciones =  this.messageChannel.ReceiveMessage().Text;
                this.messageChannel.SendMessage("Ingrese su rubro");
                string rubro = this.messageChannel.ReceiveMessage().Text;
                /*CreateEntrepeneurUser(nombre, ubi, habilitaciones,rubro);*/
               
            }
            else
            {
                this.nextHandler.Handle(input);
            }
        }
    }
}