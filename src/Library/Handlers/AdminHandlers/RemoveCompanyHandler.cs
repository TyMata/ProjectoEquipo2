using System;
using System.Collections.Generic;
using System.Text;
namespace ClassLibrary
{
    /// <summary>
    /// Handler encargado de delegar la accion de eliminar una empresa del registro.
    /// </summary>
    public class RemoveCompanyHandler : AbstractHandler
    {
        
        /// <summary>
        /// Constructor de objetos RemoveCompanyHandler
        /// </summary>
        public RemoveCompanyHandler(IMessageChannel channel)
        {
            this.Command = "/eliminarempresa";
            this.messageChannel = channel;
        }
        /// <summary>
        /// Pregunta por el nombre de la empresa la cual se quiere eliminar y luego de 
        /// verificar que ya esta registrada, la elimina.
        /// De no estar registrada le avisa al usuario de esto.
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