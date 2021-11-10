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
    /// Prueba de la clase <see cref="Material"/>.
    /// </summary>
    [TestFixture]
    public class MaterialTest
    {
       
        private string type;
        private string classification;
        private string name;
        
         [SetUp]
        public void Setup()
        {
            //this.client = new LocationApiClient();
            this.type = "madera";
            this.classification="organico";
            this.name = "nombre";

       
        }
        [Test]

        public void CreateMaterialTest()
        {
            
            Material materialCreado =new Material(this.name,this.type,this.classification);
            Assert.AreEqual(this.type,materialCreado.Type);
            Assert.AreEqual(this.classification,materialCreado.Classification);
        }

    }
}