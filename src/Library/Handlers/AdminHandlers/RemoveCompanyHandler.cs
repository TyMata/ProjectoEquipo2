using System;
using System.Collections.Generic;
using System.Text;
namespace ClassLibrary
{
    /// <summary>
    /// Marcamos el formato del resto de handlers
    /// </summary>
    public class RemoveCompanyHandler : AbstractHandler
    {
        
        /// <summary>
        /// Handler para los usuarios no registrados.
        /// </summary>
        public RemoveCompanyHandler(IMessageChannel channel)
        {
            this.Command = "/eliminarusuario";
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
                this.messageChannel.SendMessage("¿Cual es el nombre de la empresa que quieres eliminar?");
                string companyName = this.messageChannel.ReceiveMessage().Text;
                bool response;
                Company company = CompanyRegisterServiceProvider.SearchCompany(companyName, out response);
                if (response)
                {
                    CompanyRegisterServiceProvider.RemoveCompany(company.Id);
                    this.messageChannel.SendMessage($"La empresa {companyName} ha sido eliminada");
                }
                else 
                {
                    this.messageChannel.SendMessage($"La empresa {companyName} no esta registrada");
                }
            }
            else
            {
                this.nextHandler.Handle(input);
            }
            
        }
        
    }
}