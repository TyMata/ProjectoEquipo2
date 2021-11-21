using System;
using System.Collections.Generic;
using System.Text;
using Ucu.Poo.Locations.Client;

namespace ClassLibrary
{
    /// <summary>
    /// Handler encargado de crear un usuario emprendedor
    /// </summary>
    public class UnregisteredEntrepeneurUserHandler : AbstractHandler
    {
        public UnregisteredEntrepeneurUserState State { get; set; }

        public UnregisteredEntrepeneurUserData Data{ get; set; }

        /// <summary>
        /// Constructor de objetos UnregistredEntrepreneurUserHandler
        /// </summary>
        public UnregisteredEntrepeneurUserHandler(IMessageChannel channel)
        {
            this.Command = "emprendedor";
            this.messageChannel = channel;
            this.State = UnregisteredEntrepeneurUserState.Start;
            this.Data = new UnregisteredEntrepeneurUserData();
        }
        /// <summary>
        /// Pregunta por los datos del emprendedor y delega la tarea de crear un usuario emprendedor
        /// </summary>
        /// <param name="input"></param>
        /// <param name="response"></param>
        public override bool InternalHandle(IMessage input, out string response)
        {
            if ((this.State == UnregisteredEntrepeneurUserState.Start) && (CanHandle(input)))
            {
                StringBuilder datos = new StringBuilder("Asi que eres un Emprendedor!\n")
                                                .Append("Para poder registrarte vamos a necesitar algunos datos personales\n")
                                                .Append("Ingrese su nombre completo\n");
                this.State = UnregisteredEntrepeneurUserState.Name;
                response = datos.ToString();
                return true;
            }
            else if(this.State == UnregisteredEntrepeneurUserState.Name)
            {
                this.Data.Name =  this.messageChannel.ReceiveMessage().Text;
                this.State = UnregisteredEntrepeneurUserState.Location;
                response = "Ingrese su ubicacion\n";
                return true;
            }
            else if (this.State == UnregisteredEntrepeneurUserState.Location)
            {
                this.Data.Location =  this.messageChannel.ReceiveMessage().Text;
                this.Data.LocationResult = null; //TODO Buscar la dirreccion en la api para que devuelva location
                this.State = UnregisteredEntrepeneurUserState.Habilitations;
                response = "Ingrese sus habilitaciones\n";
                return true;
            }
            else if (this.State == UnregisteredEntrepeneurUserState.Habilitations)
            {
                string habilitaciones =  this.messageChannel.ReceiveMessage().Text;
                response = "Ingrese su rubro\n";
                return true;
               
            }
            else if (this.State == UnregisteredEntrepeneurUserState.Headings)
            {
                string rubro = this.messageChannel.ReceiveMessage().Text;
                this.State = UnregisteredEntrepeneurUserState.Start;
                response = "Gracias por sus datos, se esta creando su usuario\n";
                //TODO se tiene que crear el usuario
                return true;

            }
            else
            {
                response = "";
                return false;
            } 
        }

        public enum UnregisteredEntrepeneurUserState
        {
            Start,
            Name,
            Location,
            Habilitations,
            Headings
        }

        public class UnregisteredEntrepeneurUserData
        {
            /// <summary>
            /// El nombre que se ingresó en el estado UnregisteredCompanyUserState.Name.
            /// </summary>
            public string Name { get; set; }

            /// <summary>
            /// El nombre que se ingresó en el estado UnregisteredCompanyUserState.Location.
            /// </summary>
            public string Location { get; set; }

            /// <summary>
            /// El resultado de la búsqueda de la dirección ingresada.
            /// </summary>
            public Location LocationResult { get; set; }
            
            /// <summary>
            /// El link a las habilitaciones que se ingresó en el estado UnregisteredCompanyUserState.Habilitations.
            /// </summary>
            public string Habilitations { get; set; }

            /// <summary>
            /// El rubro que se ingresó en el estado UnregisteredCompanyUserState.Headings.
            /// </summary>
            public string Headings { get; set; }
        }
    }
}