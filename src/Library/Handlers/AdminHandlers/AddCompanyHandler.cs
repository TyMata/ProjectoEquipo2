using System;
using System.Collections.Generic;
using System.Text;
namespace ClassLibrary
{
    /// <summary>
    /// Marcamos el formato del resto de handlers
    /// </summary>
    public class AddCompanyHandler : AbstractHandler
    {
        
        /// <summary>
        /// Handler para los usuarios no registrados.
        /// </summary>
        public AddCompanyHandler(IMessageChannel channel)
        {
            this.Command = "/unirempresa";
            this.messageChannel = channel;
        }
        /// <summary>
        /// Verifica si el usuario que emite el mensaje esta registrado
        /// y de no ser asi lo ayuda a registrarse
        /// </summary>
        /// <param name="input"></param>
        public override void Handle(IMessage input)
        {
            if (this.nextHandler != null && (CanHandle(input)))
            {
                StringBuilder datos = new StringBuilder("Para poder registrar una empresa vamos a necesitar algunos datos de esta\n")
                                                .Append("Ingrese el nombre de la empresa\n");
                this.messageChannel.SendMessage(datos.ToString());
                string nombre = this.messageChannel.ReceiveMessage().Text;
                this.messageChannel.SendMessage("Ingrese la ubicacion\n");
                string ubi =  this.messageChannel.ReceiveMessage().Text;
                this.messageChannel.SendMessage("Ingrese los materiales producidos\n");
                string materials =  this.messageChannel.ReceiveMessage().Text;
                this.messageChannel.SendMessage("Ingrese su rubro\n");
                string headings = this.messageChannel.ReceiveMessage().Text;
                // Company nuevaCompany = CompanyServiceProvider.CreateCompany(nombre, ubi, headings, materials);
                // CompanyRegisterServiceProvider.AddCompanyToCompanyRegister(nuevaCompany);        //Comantado porque ubi es string y tiene que ser Location pero despues esta pronto
                // TokenRegisterServiceProvider.AddCompanyToTokenRegister(nuevaCompany);
                 
            }
            else
            {
                this.nextHandler.Handle(input);
            }
            
        }
        
    }
}