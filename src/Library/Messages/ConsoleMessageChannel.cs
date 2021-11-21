using System;

namespace ClassLibrary
{
    /// <summary>
    /// Esta clase representa el canal de mensajes por Consola.
    /// </summary>
    public class ConsoleMessageChannel : IMessageChannel
    {
        /// <summary>
        /// Recibe un mensaje por consola y lo devuelve en un objeto IMessage.
        /// </summary>
        /// <returns></returns>
        public IMessage ReceiveMessage()
        {
            string mensaje = Console.ReadLine();
            IMessage mensajeConsola = new ConsoleMessage(mensaje);
            return mensajeConsola;
        }

        public void SendMessage(string mensaje)
        {
            Console.WriteLine(mensaje);
        }
    }
}