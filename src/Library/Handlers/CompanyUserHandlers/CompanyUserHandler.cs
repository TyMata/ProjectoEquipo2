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
        public override void Handle(IMessage input)
        {
            if (this.nextHandler != null && (CanHandle(input)))
            {
                StringBuilder commandsStringBuilder = new StringBuilder($"Bienvenido Company.Name.\n Que desea hacer?:\n")
                                                                            .Append("/PublicarOferta\n")
                                                                            .Append("/RetirarOferta\n")
                                                                            .Append("/SuspenderOferta\n")
                                                                            .Append("/ReanudarOferta\n")
                                                                            .Append("/ModificarOferta\n")
                                                                            .Append("/BuscarOferta\n");
                this.messageChannel.SendMessage(commandsStringBuilder.ToString());
                //this.nextHandler.Handle(this.messageChannel.ReceiveMessage());
            }
            else
            {
                this.nextHandler.Handle(input);
            }
        }
    }
}