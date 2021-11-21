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
            // IMessageChannel mc = new ConsoleMessageChannel();
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
            // if (handler.Handle(mc.ReceiveMessage()) != null)
            // {
            //     return;
            // }

            // }

            if (!File.Exists(@"data.json"))
            {
                CompanyRegister.Instance.CreateCompany("empresa", new Location(), "rubro");
                Company company = CompanyRegister.Instance.GetCompanyByUserId(1234567);
                Offer createOffer = Market.Instance.CreateOffer(1234567, new Material(), "habilitación", company.Locations, 25, 10000, company, true);

                string json = createOffer.ConvertToJson();
                Console.WriteLine(json);
                File.WriteAllText(@"data.json", json);
            }
            else
            {
                Market.Instance.Initialize();

                string json = File.ReadAllText(@"data.json");

                JsonSerializerOptions options = new()
                {
                    ReferenceHandler = MyReferenceHandler.Instance,
                    WriteIndented = true
                };

                Console.WriteLine(Market.Instance.ConvertToJson());
            }
        }
    }
}