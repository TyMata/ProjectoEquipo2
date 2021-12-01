using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary
{
    /// <summary>
    /// Handler encargado de crear un usuario emprendedor.
    /// </summary>
    public class UnregisteredEntrepreneurUserHandler : AbstractHandler
    {   
        /// <summary>
        /// Estado para el handler de UnregisteredEntrepreneurUserHandler.
        /// </summary>
        /// <value></value>
        public UnregisteredEntrepreneurUserState State { get; set; }
        /// <summary>
        /// Guarda la información que pasa el usuario por el chat cuando se utiliza el comando UnregisteredEntrepreneurUserHandler.
        /// </summary>
        /// <value></value>
        public UnregisteredEntrepreneurUserData Data{ get; set; }

        /// <summary>
        /// Constructor de objetos UnregistredEntrepreneurUserHandler.
        /// </summary>
        public UnregisteredEntrepreneurUserHandler()
        {
            this.Command = "/emprendedornoregistrado";
            this.State = UnregisteredEntrepreneurUserState.Start;
            this.Data = new UnregisteredEntrepreneurUserData();
        }
        /// <summary>
        /// Pregunta por los datos del emprendedor y delega la tarea de crear un usuario emprendedor.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="response"></param>
        public override bool InternalHandle(IMessage input, out string response)
        {
            try
            {
                if ((this.State == UnregisteredEntrepreneurUserState.Start) && (CanHandle(input)))
                {
                    StringBuilder datos = new StringBuilder("¡Así que eres un Emprendedor!\n")
                                                    .Append("Para poder registrarte vamos a necesitar algunos datos personales.\n")
                                                    .Append("Ingrese su nombre completo.");
                    this.State = UnregisteredEntrepreneurUserState.Name;
                    response = datos.ToString();
                    return true;
                }
                else if(this.State == UnregisteredEntrepreneurUserState.Name)
                {
                    this.Data.Name =  input.Text;
                    this.State = UnregisteredEntrepreneurUserState.Phone;
                    response = "Ingrese su numero de teléfono.";
                    return true;
                }
                else if(this.State == UnregisteredEntrepreneurUserState.Phone)
                {
                    this.Data.Phone =  input.Text;
                    this.State = UnregisteredEntrepreneurUserState.Address;
                    response = "Ingrese su dirección.";
                    return true;
                }
                else if (this.State == UnregisteredEntrepreneurUserState.Address)
                {
                    this.Data.Address =  input.Text;
                    this.State = UnregisteredEntrepreneurUserState.City;
                    response = "Ingrese la ciudad.";
                    return true;
                }
                else if(this.State == UnregisteredEntrepreneurUserState.City)
                {
                    this.Data.City = input.Text;
                    this.State = UnregisteredEntrepreneurUserState.Department;
                    response = "Ingrese el departamento.";
                    return true;
                }
                else if (this.State == UnregisteredEntrepreneurUserState.Department)
                {
                    this.Data.Department = input.Text;
                    this.State = UnregisteredEntrepreneurUserState.Habilitations;
                    this.Data.LocationResult = new LocationAdapter(this.Data.Address,this.Data.City,this.Data.Department);
                    response = "Ingrese sus habilitaciones.";
                    return true;
                }
                else if (this.State == UnregisteredEntrepreneurUserState.Habilitations)
                {
                    string habilitaciones =  input.Text;
                    this.State = UnregisteredEntrepreneurUserState.Headings;
                    response = "Ingrese su rubro.";
                    return true;
                }
                else if (this.State == UnregisteredEntrepreneurUserState.Headings)
                {
                    string rubro = input.Text;
                    this.State = UnregisteredEntrepreneurUserState.Start;
                    UserRegister.Instance.CreateEntrepreneurUser(input.Id, this.Data.Phone ,this.Data.Name, this.Data.LocationResult, this.Data.Headings,this.Data.Habilitations);
                    response = "Gracias por sus datos, se esta creando su usuario\n";
                    return true;

                }
                response = "";
                return false;
            }
            catch(Exception e)
            {
                response = e.Message;
                return true;
            }
        }

        /// <summary>
        /// Retorna este handler al estado inicial.
        /// </summary>
        protected override void InternalCancel()
        {
            this.State = UnregisteredEntrepreneurUserState.Start;
            this.Data = new UnregisteredEntrepreneurUserData();
        }

        /// <summary>
        /// Estados para el handler de un emprendedor no registrado
        /// </summary>
        public enum UnregisteredEntrepreneurUserState
        {
            /// <summary>
            /// El estado inicial del comando. Aquí pregunta por el token de invitacion necesario para 
            /// registrar a un usuario empresa.
            /// </summary>
            Start,
            /// <summary>
            /// Estado en donde se guarda el nombre que envio el usuario y se pregunta por el telefono de contacto.
            /// </summary>
            Name,
            /// <summary>
            /// Estado en donde se guarda el telefono de contacto que envio el usuario y se pregunta por la direccion.
            /// </summary>
            Phone,
            /// <summary>
            /// Estado en donde se guarda la direccion que envio el usuario y se pregunta por la ciudad.
            /// </summary>
            Address,
            /// <summary>
            /// Estado en donde se guarda la ciudad que envio el usuario y se pregunta por el departamento.
            /// </summary>
            City,
            /// <summary>
            /// Estado en donde se guarda el departamento que envio el usuario, se encuentra la ubicacion y se pregunta por las habilitaciones.
            /// </summary>
            Department,
            /// <summary>
            /// Estado en donde se guarda el link a las habilitaciones que envio el usuario y se pregunta por el rubro.
            /// </summary>
            Habilitations,
            /// <summary>
            /// Estado en donde se guarda el rubro que envio el usuario, se crea el usuario emprendedor y se le informa al usuario.
            /// </summary>
            Headings,
        }

        /// <summary>
        /// Representa los datos que va obteniendo el comando UnregistredEntrepeneurHandler en los diferentes estados.
        /// </summary>
        public class UnregisteredEntrepreneurUserData
        {
            /// <summary>
            /// El nombre que se ingresó en el estado UnregisteredCompanyUserState.Name.
            /// </summary>
            public string Name { get; set; }

            /// <summary>
            /// Se guarda el numero de telefono que se ingresó en el estado UnregisteredEntrepreneurUserState.Phone .
            /// </summary>
            public string Phone { get; set; }

            /// <summary>
            /// se guarda la dirección que se ingresó en el estado UnregisteredEntrepreneurUserState.Addres .
            /// </summary>
            public string Address { get; set; }

            /// <summary>
            /// Se guarda la ciudad que se ingresó en el estado UnregisteredEntrepreneurUserState.City .
            /// </summary>
            /// <value></value>
            public string City{ get; set; }

            /// <summary>
            /// Se guarda eL departamento que se ingresó en el estado UnregisteredEntrepreneurUserState.Department .
            /// </summary>
            /// <value></value>
            public string Department { get; set; }

            /// <summary>
            /// El resultado de la búsqueda de la dirección ingresada.
            /// </summary>
            public LocationAdapter LocationResult { get; set; }
            
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