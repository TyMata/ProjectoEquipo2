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
        public RemoveCompanyState State {get;set;}

        public RemoveCompanyData Data {get; set;}

        /// <summary>
        /// Constructor de objetos RemoveCompanyHandler
        /// </summary>
        public RemoveCompanyHandler(IMessageChannel channel)
        {
            this.Command = "/eliminarempresa";
            this.messageChannel = channel;
            this.State = RemoveCompanyState.Start;
            this.Data = new RemoveCompanyData();
        }
        /// <summary>
        /// Pregunta por el nombre de la empresa la cual se quiere eliminar y luego de 
        /// verificar que ya esta registrada, la elimina.
        /// De no estar registrada le avisa al usuario de esto.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="response"></param>
        public override bool InternalHandle(IMessage input, out string response)
        {
            if(this.State == RemoveCompanyState.Start && this.CanHandle(input))
            {
                this.State = RemoveCompanyState.Company;
                response = "¿Cual es el Id de la empresa que quieres eliminar?";
                return true;
            }
            else if(this.State == RemoveCompanyState.Company)
            {
                this.Data.Company = Convert.ToInt32(input.Text);
                this.Data.Result = CompanyRegister.Instance.GetCompanyByUserId(this.Data.Company);
                if (this.Data.Result != null)
                {
                    CompanyRegister.Instance.Remove(this.Data.Result);
                    response = $"La empresa {this.Data.Result.Name} ha sido eliminada";
                    return true;
                }
                else 
                {
                    response = $"La empresa com el Id {this.Data.Company} no esta registrada";
                    return true;
                }
                
            }
            response = string.Empty;   
            return false;
            
        }
        
    }

    /// <summary>
    /// Guarda los estados de el handler para remover una empresa
    /// </summary>
    public enum RemoveCompanyState
    {
        Start,
        Company,
    }

    /// <summary>
    /// Guarda la informacion que envia el usuario
    /// </summary>
    public class RemoveCompanyData
    {
        public int Company {get; set;}

        public Company Result {get;set;}
    }
}