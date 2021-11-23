//--------------------------------------------------------------------------------
// <copyright file="Program.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//--------------------------------------------------------------------------------

using System;
using System.IO;
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

            if (!File.Exists(@"data.json"))
            {
                LocationAdapter location = new LocationAdapter("address", "city", "department");
                CompanyRegister.Instance.CreateCompany("empresa", location, "rubro");
                Company company = CompanyRegister.Instance.GetCompanyByUserId(1234567);
                Offer oferta = new Offer(123, new Material(), "habilitación", location, 25, 10000, company, true, new DateTime());                
                string json = oferta.ConvertToJson();
                Console.WriteLine(json);
                File.WriteAllText(@"data.json", json);
            }

            // else
            // {
            //     Market.Instance.Initialize();

            //     string json = File.ReadAllText(@"data.json");

            //     JsonSerializerOptions options = new()
            //     {
            //         ReferenceHandler = MyReferenceHandler.Instance,
            //         WriteIndented = true
            //     };

            //     Offer offer = JsonSerializer.Deserialize<Offer>(json, options);
            //     Console.WriteLine(offer.ConvertToJson());
            // }
        }
    
    }
}

