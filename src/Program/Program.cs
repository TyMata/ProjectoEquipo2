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
            //TODO: Preguntar sobre las intancias del bot.
            //TODO Agregar precondiciones y postcondiciones en TODO el bot
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

            JsonSerializerOptions options = new()
            {
                ReferenceHandler = MyReferenceHandler.Instance,
                WriteIndented = true
            };

            Console.WriteLine(CompanyRegister.Instance.ConvertToJson(options));
            Console.WriteLine(Market.Instance.ConvertToJson(options));

            foreach (Offer offer in Market.Instance.actualOfferList)
            {
                string temp = JsonSerializer.Serialize(Market.Instance.actualOfferList);
                Console.WriteLine(temp);
                File.WriteAllText(@"actualOfferListData.json", temp);
            }
            foreach (Company company in CompanyRegister.Instance.CompanyList)
            {
                // if (@"data.json" != string.Empty)
                // {
                //     string jsonD = File.ReadAllText(@"data.json");
                //     CompanyRegister companyDeserializer = JsonSerializer.Deserialize<CompanyRegister>(jsonD, options);
                // }
                // File.ReadAllText("@data.json");
                string temp2 = JsonSerializer.Serialize(CompanyRegister.Instance.CompanyList);
                Console.WriteLine(temp2);
                File.WriteAllText(@"companyData.json", temp2);
            }
            // string jsonOffer = Market.Instance.ConvertToJson(options);
            // string jsonCompany = CompanyRegister.Instance.ConvertToJson(options);
            // Console.WriteLine(jsonOffer);
            // File.WriteAllText(@"data.json", jsonOffer);
            // Console.WriteLine(jsonCompany);
            // File.WriteAllText(@"data.json", jsonCompany);
        }
    }
}

