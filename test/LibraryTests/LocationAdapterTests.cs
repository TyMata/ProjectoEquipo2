using System;
using ClassLibrary;
using NUnit.Framework;
using Ucu.Poo.Locations.Client;

namespace Tests
{
    /// <summary>
    /// Prueba la clase <see cref="LocationAdapter"/>
    /// </summary>
    [TestFixture]
    public class LocationAdapterTests
    {
        private string address;
        private string address2;
        private string city;
        private string department;
        private LocationApiClient client;

        /// <summary>
        /// Se crean variable de la api y para la ubicación.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            client = new LocationApiClient();
            address = "Av. 8 de Octubre 2738";
            city = "Montevideo";
            department = "Montevideo";
            address2 = "Comandante Braga 2715";
        }

        /// <summary>
        /// Prueba si la ubicacion que devuelve el método GetLocation de LocationApiAdapter es igual a la ubicacion de la Api.
        /// </summary>
        [Test]
        public void SetLocationTest()
        {
            Location expected = client.GetLocation(address, city, department);
            LocationAdapter location = new LocationAdapter(address,city,department);
            Assert.AreEqual(expected.Latitude,location.Latitude);
            Assert.AreEqual(expected.Longitude,location.Longitude);
        }

        /// <summary>
        /// Prueba que el metodo GetDistance devuelva la distancia correcta y no nula.
        /// </summary>
        [Test]
        public void GetDistance()
        {
            LocationAdapter location = new LocationAdapter(address,city,department);
            double distance = location.GetDistance(address2,city,department);
            Location expected1 = client.GetLocation(address, city, department);
            Location expected = client.GetLocation(address2, city, department);
            double expected2 = client.GetDistance(expected1,expected).TravelDistance;
            Assert.AreEqual(expected2 ,distance );
            Assert.IsNotNull(distance);
        }

        /// <summary>
        /// Prueba que el metodo devuelva  que  la duracion entre direcciones sea correcta.
        /// </summary>
        [Test]
        public void GetDuration()
        {
            LocationAdapter location = new LocationAdapter(address,city,department);
            double distance = location.GetDuration(address2,city,department);
            Location expected1 = client.GetLocation(address, city, department);
            Location expected = client.GetLocation(address2, city, department);
            double expected2 = client.GetDistance(expected1,expected).TravelDuration;
            Assert.AreEqual(expected2 ,distance );
            Assert.IsNotNull(distance);

        }
    }
} 