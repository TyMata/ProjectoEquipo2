using System;
using System.Collections.Generic;
using System.Text;
namespace ClassLibrary
{
    /// <summary>
    /// Marcamos el formato del resto de handlers
    /// </summary>
    public class UnregisteredUserHandler : AbstractHandler
    {

        /// <summary>
        /// Handler para los usuarios no registrados.
        /// </summary>
        public UnregisteredUserHandler(IMessageChannel channel)
        {
            this.messageChannel = channel;
            this.nextHandler2 = new UnregisteredEntrepeneurUserHandler(this.messageChannel);
            this.nextHandler2.SetNext(new UnregisteredCompanyUserHandler(this.messageChannel));
        }
        /*private IHandler nextHandler;*/
        private IHandler nextHandler2;
        /// <summary>
        /// Verifica si el usuario que emite el mensaje esta registrado
        /// y de no ser asi lo ayuda a registrarse
        /// </summary>
        /// <param name="input"></param>
        public override void Handle(IMessage input)
        {
            if (true/*this.nextHandler != null && !EstaRegistrado(input.Id)*/)
            {
                StringBuilder bienvenida = new StringBuilder("Bienvenido a el bot del Equipo 2")
                                                    .Append("Por lo que veo no estas registrado")
                                                    .Append("¿Eres usuario de una empresa o eres emprendedor?");
                this.messageChannel.SendMessage(bienvenida.ToString());
                IMessage respuesta = this.messageChannel.ReceiveMessage();
                if (respuesta.Text.ToLower().Trim() == "emprendedor" || respuesta.Text.ToLower().Trim() =="empresa" )
                    this.nextHandler2.Handle(respuesta);
                else
                {
                    /*Excepcion????*/
                }
               
            }
            else
            {
                this.nextHandler.Handle(input);
            }
        }
    }
}