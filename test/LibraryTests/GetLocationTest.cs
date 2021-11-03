//--------------------------------------------------------------------------------
// <copyright file="TrainTests.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//--------------------------------------------------------------------------------

using ClassLibrary;
using NUnit.Framework;

namespace Tests
{
    /// <summary>
    /// Prueba de la clase <see cref="GetLocation"/>.
    /// </summary>
    [TestFixture]
    public class LocationApiAdapterTest
    {
        //public LocationApiClient client;

        public LocationApiAdapter adapter;
        [SetUp]
        public void Setup()
        {
        //this.client = new LocationApiClient();
           this.adapter = new LocationApiAdapter();
        }
        [Test]

        public void GetLocationTest()
        {
            

        }

    }
}