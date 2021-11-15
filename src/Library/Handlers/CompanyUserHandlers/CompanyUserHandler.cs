using System;
using System.Text;
namespace ClassLibrary
{
    /// <summary>
    /// Handler encargado de dar a conocer los comandos disponibles para un usuario empresa
    /// </summary>
    public class CompanyUserHandler : AbstractHandler
    {
        /// <summary>
        /// Constructor de objetos CompanyUserHandler
        /// </summary>
        /// <param name="channel"></param>
        public CompanyUserHandler(IMessageChannel channel)
        {
            this.Command = "/empresa";
            this.messageChannel = channel;
        }
        /// <summary>
        /// Le da la bienvenida al usuario empresa y le pasa por pantalla los comandos disponibles.
        /// </summary>
        /// <param name="input"></param>
        public override bool InternalHandle(IMessage input)
        {
            if(CanHandle(input))
            {
                StringBuilder commandsStringBuilder = new StringBuilder($"Bienvenido \n Que desea hacer?:\n")
                                                                            .Append("/publicaroferta\n")
                                                                            .Append("/retiraroferta\n")
                                                                            .Append("/suspenderoferta\n")
                                                                            .Append("/reanudaroferta\n")
                                                                            .Append("/modificaroferta\n")
                                                                            .Append("/buscaroferta\n");
                this.messageChannel.SendMessage(commandsStringBuilder.ToString());
                return true;
               
                
            }
            return false;
        }
    }
}