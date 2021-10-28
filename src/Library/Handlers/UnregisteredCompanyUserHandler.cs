using System;
using System.Collections.Generic;
using System.Text;
namespace ClassLibrary
{
    /// <summary>
    /// Marcamos el formato del resto de handlers
    /// </summary>
    public class UnregisteredCompanyUserHandler : AbstractHandler
    {
        
        /// <summary>
        /// Handler para los usuarios no registrados.
        /// </summary>
        public UnregisteredCompanyUserHandler(IMessageChannel channel)
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
            if (/*this.nextHandler != null && */(input.Text.ToLower().Trim() == "empresa") )
            {
                StringBuilder datos = new StringBuilder("Asi que eres usuario de una empresa!")
                                                .Append("Para poder registrarte vamos a necesitar el codigo de invitacion y algunos datos personales")
                                                .Append("Ingrese el codigo de invitacion");
                /*if (CodigoEsValido(this.messageChannel.ReceiveMessage().Text))
                {*/
                    this.messageChannel.SendMessage(datos.ToString());
                    string nombre = this.messageChannel.ReceiveMessage().Text;
                    this.messageChannel.SendMessage("Ingrese su ubicacion");
                    string ubi =  this.messageChannel.ReceiveMessage().Text;
                    this.messageChannel.SendMessage("Ingrese sus habilitaciones");
                    string habilitaciones =  this.messageChannel.ReceiveMessage().Text;
                    this.messageChannel.SendMessage("Ingrese su rubro");
                    string rubro = this.messageChannel.ReceiveMessage().Text;
                    /*CreateCompanyUser(nombre, ubi, habilitaciones,rubro);
                }
                else
                {
                   Excepcion?????? 
                }*/
               
            }
            else
            {
                this.nextHandler.Handle(input);
            }
        }
    }
}