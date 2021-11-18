//--------------------------------------------------------------------------------
// <copyright file="Program.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//--------------------------------------------------------------------------------

using System;
using Ucu.Poo.Locations.Client;
using System.Text;
using ClassLibrary;

namespace ConsoleApplication
{
    /// <summary>
    /// Clase encargada de iniciar el programa.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// Punto de entrada al programa principal.
        /// </summary>
        public static void Main()
        {
            IMessageChannel mc = new ConsoleMessageChannel();
            IHandler handler = new AddCompanyHandler(mc);
            handler.SetNext(new RemoveUserHandler(mc)
                    .SetNext(new RemoveCompanyHandler(mc)
                    .SetNext(/*new RemoveCompanyHandler(mc)
                    .SetNext(*/new EndHandler(mc, null))))/*)*/;
            mc.SendMessage("Bienvenido Admin!\n");
            while(true)
            {
            StringBuilder bienvenida = new StringBuilder("Que quieres hacer?\n")
                                                .Append("/RegistrarEmpresa\n")
                                                .Append("/EliminarUsuario\n")
                                                .Append("/EliminarEmpresa\n");
            mc.SendMessage(bienvenida.ToString());
            if (handler.Handle(mc.ReceiveMessage()) != null)
            {
                return;
            }

            }
        }
    }
}