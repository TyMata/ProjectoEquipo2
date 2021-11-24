using System;
using System.Text;
namespace ClassLibrary
{
    /// <summary>
    /// Handler encargado de dar a conocer los comandos disponibles para un usuario empresa.
    /// </summary>
    public class CompanyUserHandler : AbstractHandler
    {
        public CompanyUserState State {get; private set;}

        public CompanyUserData Data {get; private set;} = new CompanyUserData();

        private Company company;

        /// <summary>
        /// Constructor de objetos CompanyUserHandler.
        /// </summary>
        /// <param name="channel"></param>
        public CompanyUserHandler()
        {
            this.Command = "/empresa";
            this.State = CompanyUserState.Start;//TODO ver de eliminar este handler
            this.company = null;
        }
        /// <summary>
        /// Le da la bienvenida al usuario empresa y le pasa por pantalla los comandos disponibles.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="response"></param>
        public override bool InternalHandle(IMessage input, out string response)
        {
            if((State == CompanyUserState.Start) && this.CanHandle(input))
            {
                StringBuilder commandsStringBuilder = new StringBuilder($"Bienvenido \n Que desea hacer?:\n")
                                                                            .Append("/publicaroferta\n")
                                                                            .Append("/retiraroferta\n")
                                                                            .Append("/suspenderoferta\n")
                                                                            .Append("/reanudaroferta\n")
                                                                            .Append("/modificaroferta\n")
                                                                            .Append("/buscaroferta\n"); //TODO fijarse si los comandos estan bien
                response = commandsStringBuilder.ToString();
                return true;
            }
            response = string.Empty;
            return false;
        }

        protected override void InternalCancel()
        {
            this.State = CompanyUserState.Start;
            this.Data = new CompanyUserData();
        }

        public enum CompanyUserState
        {
            Start,
        }

        public class CompanyUserData
        {
            public int id {get; set;}
        }
    }
}