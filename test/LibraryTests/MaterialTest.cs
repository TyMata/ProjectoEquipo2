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
       private string name;
        private string type;
        private string classification;
        
         [SetUp]
        public void Setup()
        {
        //this.client = new LocationApiClient();
        this.name = "bastón";
        this.type = "madera";
        this.classification="organico";

       
        }
        [Test]
        public void CreateMaterialTest()
        {
            
            Material materialCreado =new Material(this.name, this.type,this.classification);
            Assert.AreEqual(this.name,materialCreado.Name);
            Assert.AreEqual(this.type,materialCreado.Type);
            Assert.AreEqual(this.classification,materialCreado.Classification);
        }

    }
}