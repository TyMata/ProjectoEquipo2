using System;
using System.Collections.Generic;
using System.Text;
using Ucu.Poo.Locations.Client;
namespace ClassLibrary
{
    /// <summary>
    /// Handler encargado de delegar la accion de añadir una empresa nueva al registro
    /// </summary>
    public class AddMaterialHandler : AbstractHandler
    {
        /// <summary>
        /// Estado para el handler de AddCompany.
        /// </summary>
        /// <value></value>
        public AddMaterialState State { get; set; }
        
        /// <summary>
        /// Guarda la información que pasa el usuario por el chat cuando se utiliza el comando AddCompanyHandler.
        /// </summary>
        /// <value></value>
        public AddMaterialData Data { get; set; }

        /// <summary>
        /// Constructor de los objetos AddCompanyHandler.
        /// </summary>
        public AddMaterialHandler()
        {
            this.Command ="/agregarmaterial";
            this.State = AddMaterialState.Start;
            this.Data = new AddMaterialData();
        }
        /// <summary>
        /// Pide algunos datos de la empresa que se quiere registrar la crea.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="response"></param>
        public override bool InternalHandle(IMessage input, out string response)
        {
            try
            {
                if (this.State == AddMaterialState.Start && this.CanHandle(input))  //TODO: Verificar que sea el rol correcto.
                {
                    this.State = AddMaterialState.Name;
                    response = "Ingrese el nombre del material a añadir. Ej: Pallet, cáscara, etc.\n";
                    return true;
                }
                else if(this.State == AddMaterialState.Name)
                {
                    if (input.Text == "/menu")
                    {
                        this.State = AddMaterialState.Start;
                        response = "Volviendo al menú...";
                        return true;
                    }
                    this.Data.Name = input.Text;
                    this.State = AddMaterialState.Type;
                    response = "Ingrese el tipo:\n";
                    return true;
                }
                else if(this.State == AddMaterialState.Type)
                {
                    if (input.Text == "/menu")
                    {
                        this.State = AddMaterialState.Start;
                        response = "Volviendo al menú...";
                        return true;
                    }
                    this.Data.Type = input.Text;
                    this.State = AddMaterialState.Classification;
                    response = "Ingrese la clasificación:\n";
                    return true;
                }
                else if(this.State == AddMaterialState.Classification)
                {
                    if (input.Text == "/menu")
                    {
                        this.State = AddMaterialState.Start;
                        response = "Volviendo al menú...";
                        return true;
                    }
                    this.Data.Classification = input.Text;
                    this.State = AddMaterialState.Start;
                    Company temp = CompanyRegister.Instance.GetCompanyByUserId(input.Id);
                    temp.AddMaterial(this.Data.Name, this.Data.Type, this.Data.Classification);
                    response = "Se añadió el material";
                    return true;
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
            this.State = AddMaterialState.Start;
            this.Data = new AddMaterialData();
        }

        /// <summary>
        /// Indica los diferentes estados que puede tener el comando AddCompanyHandler.
        /// </summary>
        public enum AddMaterialState
        {
            /// <summary>
            /// El estado inicial del comando. Aquí pide el nombre del material a registrar y pasa al siguiente estado.
            /// </summary>
            Start,

            /// <summary>
            /// Luego de pedir el nombre del material. En este estado el comando pide el tipo del material y pasa al siguiente estado.
            /// </summary>
            Name,

            /// <summary>
            /// Luego de pedir el tipo del material. En este estado el comando pide las habilitaciones necesarias
            /// para manejar el material y pasa al siguiente estado.
            /// </summary>
            Type,

            /// <summary>
            /// Luego de pedir las habilitaciones necesarias. En este estado el comando añade el material y vuelve al estado Start.
            /// </summary>
            Classification
        }

        /// <summary>
        /// Representa los datos que va obteniendo el comando AddCompanyHandler en los diferentes estados.
        /// </summary>
        public class AddMaterialData
        {
            /// <summary>
            /// El nombre del objeto que se ingresó en el estado AddMaterial.Name.
            /// </summary>
            public string Name { get; set; }

            /// <summary>
            /// El tipo del material que se ingresó en el estado AddMaterial.Type.
            /// </summary>
            public string Type { get; set; }

            /// <summary>
            /// La clasificacion necesaria para manejar el material que se ingresó en el estado AddMaterial.Classification.
            /// </summary>
            public string Classification { get; set; }
        }
    }
}