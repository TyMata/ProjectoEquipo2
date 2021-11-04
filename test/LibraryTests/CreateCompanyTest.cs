using ClassLibrary;
using NUnit.Framework;
using System.Collections.Generic;
// using Ucu.Poo.Locations.Client;

// namespace Tests
// {
//     /// <summary>
//     /// Prueba de la clase <see cref="GetLocation"/>.
//     /// </summary>
//     [TestFixture]
//     public class CompanyServiceProviderTest
//     {
//         public int id;

//         public static int Id{get; private set;}

//         public string Name{get; private set;}

//         public List<Location> Locations{get; private set;}

//         private List<User> companyUsers = new List<User>();

//         public List<User> CompanyUsers {get; private set;}

//         private string headings;

//         public string Headings {get; private set;}
//         private List<Offer> offerRegister = new List<Offer>();

//         public List<Offer> OfferRegister {get; private set;}

//         private List<string> producedMaterials = new List<string>();

//         public List<string> producedMaterials = new List<string>();

//         public List<string> ProducedMaterials {get; private set;}
        
//         static Company()
//         {
//             Id = 0;
//         }

//         public Company()
//         {
//             Id++;
//         }

//         public Company(string name, Location ubi, string headings, string materials)
//         {
//             this.name = name;
//             this.Locations.Add(ubi);
//             id = Id;
//             this.Headings = headings;
//             this.ProducedMaterials.Add(materials);
//         }

//         [SetUp]
//         public void Setup()
//         {
//             this.id = Id;
//             this.Name = "Ancap";
//             this.Locations = new List<Location>();
//             this.companyUsers = new List<User>();
//             this.headings = "Agricultura";
//             this.offerRegister = new List<Offer>();
//             this.producedMaterials = new List<string>();
//         }
//         [Test]

//         public void GetLocationTest()
//         {
            
//         }
//     }
// }