//--------------------------------------------------------------------------------
// <copyright file="Program.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//--------------------------------------------------------------------------------

using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using ClassLibrary;
using Ucu.Poo.Locations.Client;

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
                //92018-40659-95124

            //TODO: Preguntar sobre los estados, si tenemos que hacer uno para la CoR y otro para cada handler.
            //TODO: Preguntar sobre las intancias del bot.
            //TODO: Preguntar como hacer que no mande siempre el menu.
           TelegramBot tb = TelegramBot.Instance;
           tb.StartCommunication();
           Console.ReadLine();
            // IHandler handler = new AddCompanyHandler(mc);
            // handler.SetNext(new RemoveUserHandler(mc)
            //         .SetNext(new RemoveCompanyHandler(mc)
            //         .SetNext(/*new RemoveCompanyHandler(mc)
            //         .SetNext(*/new EndHandler(mc, null))))/*)*/;
            // mc.SendMessage("Bienvenido Admin!\n");
            // while(true)
            // {
            // StringBuilder bienvenida = new StringBuilder("Que quieres hacer?\n")
            //                                     .Append("/RegistrarEmpresa\n")
            //                                     .Append("/EliminarUsuario\n")
            //                                     .Append("/EliminarEmpresa\n");
            // mc.SendMessage(bienvenida.ToString());
            // // if (handler.Handle(mc.ReceiveMessage()) != null)
            // // {
            // //     return;
            // // }

            // string jsonD = File.ReadAllText(@"data.json");

            JsonSerializerOptions options = new()
            {
                ReferenceHandler = MyReferenceHandler.Instance,
                WriteIndented = true
            };

            Console.WriteLine(CompanyRegister.Instance.ConvertToJson(options));
            Console.WriteLine(Market.Instance.ConvertToJson(options));

            // Market.Instance.Initialize();
            // CompanyRegister.Instance.Initialize();
            
            string jsonOffer = Market.Instance.ConvertToJson(options);
            string jsonCompany = CompanyRegister.Instance.ConvertToJson(options);
            Console.WriteLine(jsonOffer);
            File.WriteAllText(@"data.json", jsonOffer);
            Console.WriteLine(jsonCompany);
            File.WriteAllText(@"data.json", jsonCompany);
        }
    }
}

