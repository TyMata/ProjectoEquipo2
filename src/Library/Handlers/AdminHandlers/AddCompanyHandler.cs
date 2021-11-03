using System;
using System.Collections.Generic;
using System.Text;
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
                //Company nuevaCompany = CompanyServiceProvider.CreateCompany(nombre, ubi, headings, materials);
                
            }
            else
            {
                this.nextHandler.Handle(input);
            }
            
        }
        
    }
}