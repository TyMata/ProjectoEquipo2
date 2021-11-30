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
        /// Estado para el handler de RemoveCompany.
        /// </summary>
        /// <value></value>
        public RemoveCompanyState State { get; set; }

        /// <summary>
        /// Guarda la información que pasa el usuario por el chat cuando se utiliza el comando RemoveCompany.
        /// </summary>
        /// <value></value>

        public RemoveCompanyData Data {get; set;}

        /// <summary>
        /// Constructor de objetos RemoveCompanyHandler.
        /// </summary>
        public RemoveCompanyHandler()
        {
            this.Command = "/eliminarempresa";
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
        public override bool InternalHandle(IMessage input, out string response)  //TODO: mandar lista de companies.
        {
            try
            {
                if(this.State == RemoveCompanyState.Start && this.CanHandle(input))
                {
                    this.State = RemoveCompanyState.Company;
                    response = "¿Cuál es el nombre de la empresa que quieres eliminar?";
                    return true;
                }
                else if(this.State == RemoveCompanyState.Company)
                {
                    this.Data.CompanyName = input.Text;
                    if (input.Text == "/menu")
                    {
                        this.State = RemoveCompanyState.Start;
                        response = "Volviendo al menú...";
                        return true;
                    }
                    this.State = RemoveCompanyState.Start;
                    this.Data.Result = CompanyRegister.Instance.GetCompanyByName(this.Data.CompanyName); // sepide el Id de la company pero se usa el id del user para buscrla
                    if (this.Data.Result != null)
                    {
                        CompanyRegister.Instance.Remove(this.Data.Result);
                        response = $"La empresa {this.Data.CompanyName} ha sido eliminada";
                        return true;
                    }
                    else
                    {
                        response = $"La empresa bajo el nombre de \"{this.Data.CompanyName}\" no está registrada";
                        return true;
                    }
                }
                response = string.Empty;   
                return false;
            }
            catch(Exception e)
            {
                InternalCancel();
                response = e.Message;
                return true;
            }
        }

        /// <summary>
        /// Retorna este handler al estado inicial.
        /// </summary>
        protected override void InternalCancel()
        {
            this.State = RemoveCompanyState.Start;
            this.Data = new RemoveCompanyData();
        }

        /// <summary>
        /// Indica los diferentes estados que tiene AddCompanyHandler.
        /// </summary>
        public enum RemoveCompanyState
        {
            /// <summary>
            /// El estado inicial del comando. Aquí pide el Id de la empresa a eliminar y pasa al siguiente estado.
            /// </summary>
            Start,
            /// <summary>
            /// Luego de pedir el Id de la empresa. En este estado el comando elimina la empresa si existe y vuelve al estado Start.
            /// </summary>
            Company
        }

        /// <summary>
        /// Representa los datos que va obteniendo el comando RemoveCompanyHandler en los diferentes estados.
        /// </summary>
        public class RemoveCompanyData
        {
            /// <summary>
            /// El Id de la empresa que se ingresó en el estado RemoveCompanyState.Company.
            /// </summary>
            /// <value></value>
            public string CompanyName {get; set;}
            /// <summary>
            /// El resultado de la búsqueda de la empresa por medio del Id.
            /// </summary>
            /// <value></value>
            public Company Result { get; set; }
        }
    }
}