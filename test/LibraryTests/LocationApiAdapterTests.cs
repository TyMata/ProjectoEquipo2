using System;
using Ucu.Poo.Locations.Client;
using ClassLibrary;
using NUnit.Framework;

namespace Tests
{
    /// <summary>
    /// Prueba la clase <see cref="LocationApiAdapter"/>
    /// </summary>
    [TestFixture]
    public class LocationApiAdapterTests
    {
        private string address;
        private string city;
        private string department;
        private string address2;
    
        [SetUp]
        public void SetUp()
        {
            address = "Av. 8 de Octubre 2738";
            city = "Montevideo";
            department = "Montevideo";
            address2 = "Comandante Braga 2715";

        }

        [Test]
        public void GetLocationAdapterTest()
        {
            LocationAdapter location = new LocationAdapter(address,city,department);
            Assert.IsNotNull(location);
        }

        // /// <summary>
        // /// Prueba que se devuelva la distancia correcta
        // /// </summary>
        // [Test]
        // public void GetDistanceTest()
        // {
        //     IDistanceResult result = LocationApiAdapter.Instance.GetDistance(address,address2);
        //     Assert.AreEqual(0.608,result);
        // }
    }
}