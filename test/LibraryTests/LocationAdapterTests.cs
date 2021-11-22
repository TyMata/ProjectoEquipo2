// using System;
// using ClassLibrary;
// using NUnit.Framework;
// using Ucu.Poo.Locations.Client;

// namespace Tests
// {
//     /// <summary>
//     /// Prueba la clase <see cref="LocationAdapter"/>
//     /// </summary>
//     [TestFixture]
//     public class LocationAdapterTests
//     {
//         private string address;
//         private string city;
//         private string department;
//         private LocationApiClient client;

//         /// <summary>
//         /// Se crean variable de la api y para la ubicación.
//         /// </summary>
//         [SetUp]
//         public void Setup()
//         {
//             client = new LocationApiClient();
//             address = "Av. 8 de Octubre 2738";
//             city = "Montevideo";
//             department = "Montevideo";
//         }

//         /// <summary>
//         /// Prueba si la ubicacion que devuelve el método GetLocation de LocationApiAdapter es igual a la ubicacion de la Api.
//         /// </summary>
//         [Test]
//         public void LocationTest()
//         {
//             Location expected = client.GetLocation(address, city, department);
//             LocationAdapter location = new LocationAdapter(address,city,department);
//             Assert.AreEqual(expected.Latitude,location.Latitude);
//             Assert.AreEqual(expected.Longitude,location.Longitude);
//         }
//     }
// }