using System;
using System.Collections.Generic;
using System.Text;
using Ucu.Poo.Locations.Client;
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
                this.messageChannel.SendMessage("Ingrese el pais\n");
                string pais =  this.messageChannel.ReceiveMessage().Text;
                this.messageChannel.SendMessage("Ingrese el departamento\n");
                string departamento =  this.messageChannel.ReceiveMessage().Text;
                this.messageChannel.SendMessage("Ingrese la ciudad\n");
                string ciudad =  this.messageChannel.ReceiveMessage().Text;
                this.messageChannel.SendMessage("Ingrese la dirección\n");
                string direccion =  this.messageChannel.ReceiveMessage().Text;
                this.messageChannel.SendMessage("Ingrese los materiales producidos\n");
                string materials =  this.messageChannel.ReceiveMessage().Text;
                this.messageChannel.SendMessage("Ingrese su rubro\n");
                string headings = this.messageChannel.ReceiveMessage().Text;
                Location ubi = LocationServiceProvider.client.GetLocationAsync(pais, departamento, ciudad, direccion).Result;
                Company nuevaCompany = CompanyServiceProvider.CreateCompany(nombre, ubi, headings, materials);
                CompanyRegisterServiceProvider.AddCompanyToCompanyRegister(nuevaCompany);        //Comantado porque ubi es string y tiene que ser Location pero despues esta pronto
                TokenRegisterServiceProvider.AddCompanyToTokenRegister(nuevaCompany);
                 
            }
            else
            {
                this.nextHandler.Handle(input);
            }
            
        }
        
    }
}