using System;
using System.Collections.Generic;
using System.Text;
using Ucu.Poo.Locations.Client;
namespace ClassLibrary
{
    /// <summary>
    /// Handler encargado de delegar la accion de añadir una empresa nueva al registro
    /// </summary>
    public class AddCompanyHandler : AbstractHandler
    {
        
        /// <summary>
        /// Constructor de los objetos AddCompanyHandler
        /// </summary>
        public AddCompanyHandler(IMessageChannel channel)
        {
            this.Command = "/unirempresa";
            this.messageChannel = channel;
        }
        /// <summary>
        /// Pide algunos datos de la empresa que se quiere registrar la crea
        /// </summary>
        /// <param name="input"></param>
        public override bool InternalHandle(IMessage input)
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
                return true;
                //Comantado porque ubi es string y tiene que ser Location pero despues esta pronto
                //TokenRegisterServiceProvider.AddCompanyToTokenRegister(nuevaCompany);         
            }
            return false;
        }  
    }
}